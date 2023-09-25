import axios, { AxiosError } from "axios";
import { createContext, useState, useEffect } from "react";
import { addGiroAccount, editGiroAccount, getGiroAccounts } from "../api/api";
import { addGiroAccounts, allGiroAccounts } from "../api/interfaces";
import PopUp from "../ui/PopUp";

export type GiroAccountsContextData = {
  giroAccounts: allGiroAccounts[];
  removeGiroAcc: (id: number) => Promise<void>;
  addAcc: (data: addGiroAccounts) => Promise<void>;
  editAcc: (id: number, data: addGiroAccounts) => Promise<void>;
};

export const GiroAccountsContext = createContext<GiroAccountsContextData>({
  giroAccounts: [],
  removeGiroAcc: async (id: number) => {},
  addAcc: async (data: addGiroAccounts) => {},
  editAcc: async (id: number, data: addGiroAccounts) => {},
});

export const ZiroRacuniProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [giroAccounts, setGiroAccounts] = useState<allGiroAccounts[]>([]);
  const [isPopupOpen, setPopupOpen] = useState<boolean>(false);

  useEffect(() => {
    const fetchGiroAcc = async () => {
      try {
        const giroData = await getGiroAccounts();
        setGiroAccounts(giroData);
      } catch (error) {
        console.log(error);
      }
    };
    fetchGiroAcc();
  }, []);

  const removeGiroAcc = async (id: number) => {
    try {
      await axios.delete(
        `https://localhost:7206/api/GiroAccounts/DeleteGiroAcc/${id}`
      );
      setGiroAccounts(giroAccounts.filter((x) => x.id !== id));
    } catch (error) {
      console.log(error);
    }
  };

  const addAcc = async (data: addGiroAccounts): Promise<void> => {
    try {
      const newAcc = await addGiroAccount(data);
      setGiroAccounts((x) => [newAcc, ...x]);
    } catch (error) {
      const axiosError = error as AxiosError;
      if (axiosError.response && axiosError.response.status === 400) {
        setPopupOpen(true);
      }
    }
  };

  const editAcc = async (id: number, data: addGiroAccounts): Promise<void> => {
    try {
      const updatedAcc = await editGiroAccount(id, data);
      setGiroAccounts((prevGiroAccounts) => {
        return prevGiroAccounts.map((acc) =>
          acc.id === id ? updatedAcc : acc
        );
      });
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <GiroAccountsContext.Provider
      value={{
        giroAccounts,
        removeGiroAcc,
        addAcc,
        editAcc,
      }}
    >
      {isPopupOpen && (
        <PopUp isOpen={isPopupOpen} onClose={() => setPopupOpen(false)} />
      )}
      {children}
    </GiroAccountsContext.Provider>
  );
};
