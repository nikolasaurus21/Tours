import React, { createContext, useEffect, useState } from "react";
import { addPayment, allPayments } from "../api/interfaces";
import { addPayments, editPayments, getPayments } from "../api/api";
import { AxiosError } from "axios";
import PopUp from "../ui/PopUp";

export type PaymentsContextData = {
  payments: allPayments[];
  addNewPayment: (data: addPayment) => Promise<void>;
  editPayment: (id: number, data: addPayment) => Promise<void>;
  paymentsChanged: boolean;
  setPaymentsChanged: React.Dispatch<React.SetStateAction<boolean>>;
};

export const PaymentsContext = createContext<PaymentsContextData>({
  payments: [],
  addNewPayment: async (data: addPayment) => {},
  editPayment: async (id: number, data: addPayment) => {},
  paymentsChanged: false,
  setPaymentsChanged: () => {},
});

export const PaymentsProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [payments, setPayments] = useState<allPayments[]>([]);
  const [paymentsChanged, setPaymentsChanged] = useState<boolean>(false);
  const [isPopupOpen, setPopupOpen] = useState<boolean>(false);

  useEffect(() => {
    const fetchPayments = async () => {
      try {
        const paymentsData = await getPayments();
        setPayments(paymentsData);
      } catch (error) {
        console.log(error);
      }
    };
    fetchPayments();
  }, []);

  const addNewPayment = async (data: addPayment): Promise<void> => {
    try {
      const newPayment = await addPayments(data);
      setPayments((x) => [newPayment, ...x]);
      setPaymentsChanged(true);
    } catch (error) {
      const axiosError = error as AxiosError;
      if (axiosError.response && axiosError.response.status === 400) {
        setPopupOpen(true);
      }
    }
  };

  const editPayment = async (id: number, data: addPayment): Promise<void> => {
    try {
      const updatePayment = await editPayments(id, data);
      setPayments((x) => {
        return x.map((pay) => (pay.id === id ? updatePayment : pay));
      });
      setPaymentsChanged(true);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <PaymentsContext.Provider
      value={{
        payments,
        addNewPayment,
        editPayment,
        paymentsChanged,
        setPaymentsChanged,
      }}
    >
      {isPopupOpen && (
        <PopUp isOpen={isPopupOpen} onClose={() => setPopupOpen(false)} />
      )}
      {children}
    </PaymentsContext.Provider>
  );
};
