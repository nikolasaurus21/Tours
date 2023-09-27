import React, { createContext, useEffect, useState } from "react";
import { IAddInovice, IEditInovice, Inovices } from "../api/interfaces";
import { editInovice, getInovices, newInovice } from "../api/api";
import axios, { AxiosError } from "axios";
import PopUp from "../ui/PopUp";

export type InovicesContextData = {
  inovices: Inovices[];
  currentPage: number;
  totalPages: number;
  setCurrentPage: React.Dispatch<React.SetStateAction<number>>;
  addInovice: (data: IAddInovice) => Promise<void>;
  removeInovice: (id: number) => Promise<void>;
  updateInovice: (id: number, data: IEditInovice) => Promise<void>;
};

export const InovicesContext = createContext<InovicesContextData>({
  inovices: [],
  setCurrentPage: () => {},
  currentPage: 1,
  totalPages: 1,
  addInovice: async (data: IAddInovice) => {},
  removeInovice: async (id: number) => {},
  updateInovice: async (id: number, data: IAddInovice) => {},
});
export const InovicesProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [inovices, setInovices] = useState<Inovices[]>([]);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(0);
  const [isPopupOpen, setPopupOpen] = useState<boolean>(false);

  useEffect(() => {
    const fetchInovices = async () => {
      try {
        const inovicesData = await getInovices(currentPage);
        setInovices(inovicesData);
        setTotalPages(Math.ceil(inovicesData.length / 10));
      } catch (error) {
        console.error("Error fetching inovices:", error);
      }
    };

    fetchInovices();
  }, [currentPage]);

  const addInovice = async (data: IAddInovice): Promise<void> => {
    try {
      const addInovice = await newInovice(data);

      setInovices((prevInovices) => [addInovice, ...prevInovices]);
    } catch (error) {
      const axiosError = error as AxiosError;
      if (axiosError.response && axiosError.response.status === 400) {
        setPopupOpen(true);
      }
    }
  };

  const removeInovice = async (id: number) => {
    try {
      await axios.delete(
        `https://localhost:7206/api/Inovices/DeleteInovice/${id}`
      );
      setInovices(inovices.filter((inv) => inv.id !== id));
    } catch (error) {
      console.log(error);
    }
  };

  const updateInovice = async (
    id: number,
    data: IEditInovice
  ): Promise<void> => {
    try {
      const updateInovice = await editInovice(id, data);

      setInovices((x) => {
        return x.map((i) => (i.id === id ? updateInovice : i));
      });
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <InovicesContext.Provider
      value={{
        inovices,
        setCurrentPage,
        currentPage,
        totalPages,
        addInovice,
        removeInovice,
        updateInovice,
      }}
    >
      {isPopupOpen && (
        <PopUp isOpen={isPopupOpen} onClose={() => setPopupOpen(false)} />
      )}
      {children}
    </InovicesContext.Provider>
  );
};
