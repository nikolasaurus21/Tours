import React, { createContext, useEffect, useState } from "react";
import { IAddInvoice, IEditInvoice, Invoices } from "../api/interfaces";
import { editInvoice, getInvoices, newInvoice } from "../api/api";
import axios, { AxiosError } from "axios";
import PopUp from "../ui/PopUp";

export type InvoicesContextData = {
  invoices: Invoices[];
  currentPage: number;
  totalPages: number;
  setCurrentPage: React.Dispatch<React.SetStateAction<number>>;
  addInvoice: (data: IAddInvoice) => Promise<void>;
  removeInvoice: (id: number) => Promise<void>;
  updateInvoice: (id: number, data: IEditInvoice) => Promise<void>;
};

export const InvoicesContext = createContext<InvoicesContextData>({
  invoices: [],
  setCurrentPage: () => {},
  currentPage: 1,
  totalPages: 1,
  addInvoice: async (data: IAddInvoice) => {},
  removeInvoice: async (id: number) => {},
  updateInvoice: async (id: number, data: IAddInvoice) => {},
});
export const InvoicesProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [invoices, setInvoices] = useState<Invoices[]>([]);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [isPopupOpen, setPopupOpen] = useState<boolean>(false);

  useEffect(() => {
    const fetchInvoices = async () => {
      try {
        const invoicesData = await getInvoices(currentPage);
        setInvoices(invoicesData);
        setTotalPages(Math.ceil(invoicesData.length / 10));
      } catch (error) {
        console.error("Error fetching invoices:", error);
      }
    };

    fetchInvoices();
  }, [currentPage]);

  const addInvoice = async (data: IAddInvoice): Promise<void> => {
    try {
      const addInovice = await newInvoice(data);

      setInvoices((prevInovices) => [addInovice, ...prevInovices]);
    } catch (error) {
      const axiosError = error as AxiosError;
      if (axiosError.response && axiosError.response.status === 400) {
        setPopupOpen(true);
      }
    }
  };

  const removeInvoice = async (id: number) => {
    try {
      await axios.delete(
        `https://localhost:7206/api/Invoices/DeleteInvoice/${id}`
      );
      setInvoices(invoices.filter((inv) => inv.id !== id));
    } catch (error) {
      console.log(error);
    }
  };

  const updateInvoice = async (
    id: number,
    data: IEditInvoice
  ): Promise<void> => {
    try {
      const updateInovice = await editInvoice(id, data);

      setInvoices((x) => {
        return x.map((i) => (i.id === id ? updateInovice : i));
      });
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <InvoicesContext.Provider
      value={{
        invoices,
        setCurrentPage,
        currentPage,
        totalPages,
        addInvoice,
        removeInvoice,
        updateInvoice,
      }}
    >
      {isPopupOpen && (
        <PopUp isOpen={isPopupOpen} onClose={() => setPopupOpen(false)} />
      )}
      {children}
    </InvoicesContext.Provider>
  );
};
