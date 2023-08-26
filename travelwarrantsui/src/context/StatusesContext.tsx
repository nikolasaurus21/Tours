import React, {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useState,
} from "react";
import { Statuses } from "../api/interfaces";
import { getStatuses } from "../api/api";
import { PaymentsContext } from "./PaymentsContext";
import { SearchesContext } from "./SearchesContext";
export type StatusesContextData = {
  statuses: Statuses[];
  refreshStatuses: () => Promise<void>;
};

export const StatusesContext = createContext<StatusesContextData>({
  statuses: [],
  refreshStatuses: async () => {},
});

export const StatusesProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [statuses, setStatuses] = useState<Statuses[]>([]);
  const { paymentsChanged, setPaymentsChanged } = useContext(PaymentsContext);
  const { searchesChanged, setSearchesChanged } = useContext(SearchesContext);

  const refreshStatuses = useCallback(async () => {
    if (paymentsChanged || searchesChanged) {
      try {
        await fetchStatuses();
        setPaymentsChanged(false);
        setSearchesChanged(false);
      } catch (error) {
        console.log(error);
      }
    }
  }, [
    paymentsChanged,
    setPaymentsChanged,
    searchesChanged,
    setSearchesChanged,
  ]);

  useEffect(() => {
    refreshStatuses();
  }, [refreshStatuses]);

  const fetchStatuses = async (): Promise<void> => {
    try {
      const statusesData = await getStatuses();
      setStatuses(statusesData);
    } catch (error) {
      console.log(error);
    }
  };
  useEffect(() => {
    fetchStatuses();
  }, []);

  return (
    <StatusesContext.Provider
      value={{
        statuses,

        refreshStatuses,
      }}
    >
      {children}
    </StatusesContext.Provider>
  );
};
