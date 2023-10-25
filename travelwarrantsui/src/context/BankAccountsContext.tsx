import axios, { AxiosError } from "axios";
import { createContext, useState, useEffect } from "react";
import { addBankAccount, editBankAccount, getBankAccounts } from "../api/api";
import { addBankAccounts, allBankAccounts } from "../api/interfaces";
import PopUp from "../ui/PopUp";

export type BankAccountsContextData = {
  bankAccounts: allBankAccounts[];
  removeBankAcc: (id: number) => Promise<void>;
  addAcc: (data: addBankAccounts) => Promise<void>;
  editAcc: (id: number, data: addBankAccounts) => Promise<void>;
};

export const BankAccountsContext = createContext<BankAccountsContextData>({
  bankAccounts: [],
  removeBankAcc: async (id: number) => {},
  addAcc: async (data: addBankAccounts) => {},
  editAcc: async (id: number, data: addBankAccounts) => {},
});

export const BankAccountsProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [bankAccounts, setBankAccounts] = useState<allBankAccounts[]>([]);
  const [isPopupOpen, setPopupOpen] = useState<boolean>(false);

  useEffect(() => {
    const fetchGiroAcc = async () => {
      try {
        const giroData = await getBankAccounts();
        setBankAccounts(giroData);
      } catch (error) {
        console.log(error);
      }
    };
    fetchGiroAcc();
  }, []);

  const removeBankAcc = async (id: number) => {
    try {
      await axios.delete(
        `https://localhost:7206/api/GiroAccounts/DeleteGiroAcc/${id}`
      );
      setBankAccounts(bankAccounts.filter((x) => x.id !== id));
    } catch (error) {
      console.log(error);
    }
  };

  const addAcc = async (data: addBankAccounts): Promise<void> => {
    try {
      const newAcc = await addBankAccount(data);
      setBankAccounts((x) => [newAcc, ...x]);
    } catch (error) {
      const axiosError = error as AxiosError;
      if (axiosError.response && axiosError.response.status === 400) {
        setPopupOpen(true);
      }
    }
  };

  const editAcc = async (id: number, data: addBankAccounts): Promise<void> => {
    try {
      const updatedAcc = await editBankAccount(id, data);
      setBankAccounts((prevGiroAccounts) => {
        return prevGiroAccounts.map((acc) =>
          acc.id === id ? updatedAcc : acc
        );
      });
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <BankAccountsContext.Provider
      value={{
        bankAccounts,
        removeBankAcc,
        addAcc,
        editAcc,
      }}
    >
      {isPopupOpen && (
        <PopUp isOpen={isPopupOpen} onClose={() => setPopupOpen(false)} />
      )}
      {children}
    </BankAccountsContext.Provider>
  );
};
