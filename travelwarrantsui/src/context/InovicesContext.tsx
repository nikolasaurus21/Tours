import React, { createContext, useEffect, useState } from "react";
import { IAddInovice, Inovices } from "../api/interfaces";
import { getInovices, newInovice } from "../api/api";
import { AxiosError } from "axios";
import PopUp from "../ui/PopUp";

export type InovicesContextData = {
  inovices: Inovices[];
  currentPage: number;
  totalPages: number;
  setCurrentPage: React.Dispatch<React.SetStateAction<number>>;
  addInovice: (data: IAddInovice) => Promise<void>;
};

export const InovicesContext = createContext<InovicesContextData>({
  inovices: [],
  setCurrentPage: () => {},
  currentPage: 1,
  totalPages: 1,
  addInovice: async (data: IAddInovice) => {},
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
  return (
    <InovicesContext.Provider
      value={{ inovices, setCurrentPage, currentPage, totalPages, addInovice }}
    >
      {isPopupOpen && (
        <PopUp isOpen={isPopupOpen} onClose={() => setPopupOpen(false)} />
      )}
      {children}
    </InovicesContext.Provider>
  );
};
