import React, { createContext, useEffect, useState } from "react";
import { Inovices } from "../api/interfaces";
import { getInovices } from "../api/api";

export type InovicesContextData = {
  inovices: Inovices[];
  setCurrentPage: React.Dispatch<React.SetStateAction<number>>;
};

export const InovicesContext = createContext<InovicesContextData>({
  inovices: [],
  setCurrentPage: () => {},
});
export const InovicesProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [inovices, setInovices] = useState<Inovices[]>([]);
  const [currentPage, setCurrentPage] = useState<number>(1);

  useEffect(() => {
    const fetchInovices = async () => {
      try {
        const inovicesData = await getInovices(currentPage);
        setInovices(inovicesData);
      } catch (error) {
        console.error("Error fetching inovices:", error);
      }
    };

    fetchInovices();
  }, [currentPage]);
  return (
    <InovicesContext.Provider value={{ inovices, setCurrentPage }}>
      {children}
    </InovicesContext.Provider>
  );
};
