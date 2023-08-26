import React, { createContext, useEffect, useState } from "react";
import { addPayment, allPayments } from "../api/interfaces";
import { addPayments, editPayments, getPayments } from "../api/api";

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
      console.log(error);
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
      {children}
    </PaymentsContext.Provider>
  );
};
