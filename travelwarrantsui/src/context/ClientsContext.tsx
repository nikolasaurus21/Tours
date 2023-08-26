import React, { createContext, useEffect, useState } from "react";
import {
  addClients,
  getAllClients,
  editClients,
  getClientById,
} from "../api/api"; // Uvoz novih funkcija
import axios from "axios";
import { addClient, allClients } from "../api/interfaces";

export type ClientsContextData = {
  clients: allClients[];

  removeClient: (id: number) => Promise<void>;
  getClientById: (id: number) => Promise<addClient>;
  addClient: (data: addClient) => Promise<void>;
  editClient: (id: number, data: addClient) => Promise<void>;
};

export const ClientsContext = createContext<ClientsContextData>({
  clients: [],
  removeClient: async (id: number) => {},
  addClient: async (data: addClient) => {},
  editClient: async (id: number, data: addClient) => {},
  getClientById: async (id: number): Promise<addClient> => {
    return {} as addClient;
  },
});

export const ClientsProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [clients, setClients] = useState<allClients[]>([]);

  useEffect(() => {
    const fetchClients = async () => {
      try {
        const clientsData = await getAllClients();
        setClients(clientsData);
      } catch (error) {
        console.error("Error fetching clients:", error);
      }
    };

    fetchClients();
  }, []);

  const removeClient = async (id: number) => {
    try {
      await axios.delete(
        `https://localhost:7206/api/Clients/DeleteClient/${id}`
      );
      setClients(clients.filter((clnt) => clnt.id !== id));
    } catch (error) {
      console.log(error);
    }
  };
  const addClient = async (data: addClient): Promise<void> => {
    try {
      const newClient = await addClients(data);

      setClients((prevClients) => [newClient, ...prevClients]);
    } catch (error) {
      console.log(error);
    }
  };

  const editClient = async (id: number, data: addClient): Promise<void> => {
    try {
      const newClient = await editClients(id, data);

      setClients((x) => {
        return x.map((acc) => (acc.id === id ? newClient : acc));
      });
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <ClientsContext.Provider
      value={{
        clients,
        removeClient,
        addClient,
        editClient,
        getClientById,
      }}
    >
      {children}
    </ClientsContext.Provider>
  );
};
