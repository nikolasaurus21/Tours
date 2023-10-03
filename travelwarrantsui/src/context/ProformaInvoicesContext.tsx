import React, { createContext, useEffect, useState } from "react";
import PopUp from "../ui/PopUp";
import { IAddProformaInvoice, IProformaInvoices } from "../api/interfaces";
import { allProformaInvoices, newProformaInvoice } from "../api/api";
import axios, { AxiosError } from "axios";

export type ProformaInvoicesContextData = {
  proformaInvoices: IProformaInvoices[];
  currentPage: number;
  totalPages: number;
  setCurrentPage: React.Dispatch<React.SetStateAction<number>>;
  removeProformaInvoice: (id: number) => Promise<void>;
  addProformaInvoice: (data: IAddProformaInvoice) => Promise<void>;
};

export const ProformaInovicesContext =
  createContext<ProformaInvoicesContextData>({
    proformaInvoices: [],
    setCurrentPage: () => {},
    currentPage: 1,
    totalPages: 1,
    removeProformaInvoice: async (id: number) => {},
    addProformaInvoice: async (data: IAddProformaInvoice) => {},
  });

export const InovicesProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [isPopupOpen, setPopupOpen] = useState<boolean>(false);

  const [proformaInvoices, setProformaInvoices] = useState<IProformaInvoices[]>(
    []
  );
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(1);

  useEffect(() => {
    const fetchInovices = async () => {
      try {
        const proInvoiceData = await allProformaInvoices(currentPage);
        console.log(proInvoiceData);
        setProformaInvoices(proInvoiceData);
        setTotalPages(Math.ceil(proInvoiceData.length / 10));
      } catch (error) {
        console.error("Error fetching inovices:", error);
      }
    };

    fetchInovices();
  }, [currentPage]);

  const removeProformaInvoice = async (id: number) => {
    try {
      await axios.delete(
        `https://localhost:7206/api/Inovices/DeleteInovice/${id}`
      );
      setProformaInvoices(proformaInvoices.filter((inv) => inv.id !== id));
    } catch (error) {
      console.log(error);
    }
  };
  const addProformaInvoice = async (
    data: IAddProformaInvoice
  ): Promise<void> => {
    try {
      const addProformaInvoice = await newProformaInvoice(data);

      setProformaInvoices((prevInovices) => [
        addProformaInvoice,
        ...prevInovices,
      ]);
    } catch (error) {
      const axiosError = error as AxiosError;
      if (axiosError.response && axiosError.response.status === 400) {
        setPopupOpen(true);
      }
    }
  };
  return (
    <ProformaInovicesContext.Provider
      value={{
        proformaInvoices,
        totalPages,
        setCurrentPage,
        currentPage,
        removeProformaInvoice,
        addProformaInvoice,
      }}
    >
      {isPopupOpen && (
        <PopUp isOpen={isPopupOpen} onClose={() => setPopupOpen(false)} />
      )}
      {children}
    </ProformaInovicesContext.Provider>
  );
};