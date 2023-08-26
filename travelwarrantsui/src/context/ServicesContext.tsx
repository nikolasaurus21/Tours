import axios from "axios";
import { createContext, useState, useEffect } from "react";
import { addServices, editServices, getServices } from "../api/api";
import { addService, allServices } from "../api/interfaces";

export type ServicesContextData = {
  services: allServices[];
  removeService: (id: number) => Promise<void>;
  addNewService: (data: addService) => Promise<void>;
  editService: (id: number, data: addService) => Promise<void>;
};

export const ServicesContext = createContext<ServicesContextData>({
  services: [],
  removeService: async (id: number) => {},
  addNewService: async (data: addService) => {},
  editService: async (id: number, data: addService) => {},
});
export const ServicesProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [services, setServices] = useState<allServices[]>([]);

  useEffect(() => {
    const fetchServices = async () => {
      try {
        const serviceData = await getServices();
        setServices(serviceData);
      } catch (error) {
        console.log(error);
      }
    };
    fetchServices();
  }, []);

  const removeService = async (id: number) => {
    try {
      await axios.delete(
        `https://localhost:7206/api/Services/DeleteService/${id}`
      );
      setServices(services.filter((x) => x.id !== id));
    } catch (error) {
      console.log(error);
    }
  };

  const addService = async (data: addService): Promise<void> => {
    try {
      const newService = await addServices(data);
      setServices((x) => [newService, ...x]);
    } catch (error) {
      console.log(error);
    }
  };

  const editService = async (id: number, data: addService): Promise<void> => {
    try {
      const newService = await editServices(id, data);
      setServices((x) => {
        return x.map((service) => (service.id === id ? newService : service));
      });
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <ServicesContext.Provider
      value={{
        services,
        removeService,
        addNewService: addService,
        editService,
      }}
    >
      {children}
    </ServicesContext.Provider>
  );
};
