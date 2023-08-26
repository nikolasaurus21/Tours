import React, { createContext, useState, useEffect } from "react";
import { ICompany } from "../api/interfaces";
import { getCompany, addCompany } from "../api/api";

export type CompanyContextData = {
  company: ICompany[];
  companyAdd: (data: ICompany) => Promise<void>;
  isCompanyAdded: boolean;
  isPopUpOpen: boolean;
  setIsPopUpOpen: React.Dispatch<React.SetStateAction<boolean>>;
};

export const CompanyContext = createContext<CompanyContextData>({
  company: [],
  companyAdd: async (data: ICompany) => {},
  isCompanyAdded: false,
  isPopUpOpen: false,
  setIsPopUpOpen: () => {},
});

export const CompanyProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [company, setCompany] = useState<ICompany[]>(() => {
    const savedCompany = localStorage.getItem("company");
    return savedCompany ? JSON.parse(savedCompany) : [];
  });

  const [isCompanyAdded, setIsCompanyAdded] = useState<boolean>(() => {
    const savedIsCompanyAdded = localStorage.getItem("isCompanyAdded");
    return savedIsCompanyAdded ? JSON.parse(savedIsCompanyAdded) : false;
  });
  const [isPopUpOpen, setIsPopUpOpen] = useState<boolean>(false);

  useEffect(() => {
    localStorage.setItem("isCompanyAdded", JSON.stringify(isCompanyAdded));
  }, [isCompanyAdded]);

  useEffect(() => {
    const fetchCompany = async () => {
      try {
        const companyData = await getCompany();
        localStorage.setItem("company", JSON.stringify(companyData));
        setCompany(companyData);
      } catch (error) {
        console.log(error);
      }
    };
    fetchCompany();
  }, []);

  useEffect(() => {
    localStorage.setItem("company", JSON.stringify(company));
  }, [company]);

  const companyAdd = async (data: ICompany): Promise<void> => {
    try {
      await addCompany(data);
      const companyData = await getCompany();
      localStorage.setItem("company", JSON.stringify(companyData));
      setCompany(companyData);
      setIsCompanyAdded(true);
    } catch (error) {
      console.log(error);
    }
  };
  return (
    <CompanyContext.Provider
      value={{
        company,
        companyAdd,
        isCompanyAdded,
        isPopUpOpen,
        setIsPopUpOpen,
      }}
    >
      {children}
    </CompanyContext.Provider>
  );
};
