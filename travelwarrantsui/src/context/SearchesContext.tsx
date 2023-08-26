import React, { createContext, useEffect, useState } from "react";
import { addPayment, allPayments } from "../api/interfaces";
import { addSearches, editSearches, getSearches } from "../api/api";

export type SearchesContextData = {
  searches: allPayments[];
  addNewSearch: (data: addPayment) => Promise<void>;
  editSearch: (id: number, data: addPayment) => Promise<void>;
  searchesChanged: boolean;
  setSearchesChanged: React.Dispatch<React.SetStateAction<boolean>>;
};

export const SearchesContext = createContext<SearchesContextData>({
  searches: [],
  addNewSearch: async (data: addPayment) => {},
  editSearch: async (id: number, data: addPayment) => {},
  searchesChanged: false,
  setSearchesChanged: () => {},
});

export const SearchesProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [searches, setSearches] = useState<allPayments[]>([]);
  const [searchesChanged, setSearchesChanged] = useState<boolean>(false);

  useEffect(() => {
    const fetchSearches = async () => {
      try {
        const searchesData = await getSearches();
        const sortedSearches = searchesData.sort(
          (a, b) => new Date(b.date).getTime() - new Date(a.date).getTime()
        );
        setSearches(sortedSearches);
      } catch (error) {
        console.log(error);
      }
    };
    fetchSearches();
  }, []);

  const addNewSearch = async (data: addPayment): Promise<void> => {
    try {
      const newSearch = await addSearches(data);
      setSearches((x) => [newSearch, ...x]);
      setSearchesChanged(true);
    } catch (error) {
      console.log(error);
    }
  };

  const editSearch = async (id: number, data: addPayment): Promise<void> => {
    try {
      const updateSearch = await editSearches(id, data);
      setSearches((x) => {
        return x.map((s) => (s.id === id ? updateSearch : s));
      });
      setSearchesChanged(true);
    } catch (error) {}
  };

  return (
    <SearchesContext.Provider
      value={{
        searches,
        addNewSearch,
        editSearch,
        searchesChanged,
        setSearchesChanged,
      }}
    >
      {children}
    </SearchesContext.Provider>
  );
};
