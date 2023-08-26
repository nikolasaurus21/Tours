import React, { createContext, useState, useEffect } from "react";
import { addDriver, allDrivers } from "../api/interfaces";
import {
  addDrivers,
  editDrivers,
  getDrivers,
  getDriversById,
} from "../api/api";
import axios from "axios";

export type DriversContextData = {
  drivers: allDrivers[];
  removeDriver: (id: number) => Promise<void>;
  addDriver: (data: addDriver) => Promise<void>;
  editDriver: (id: number, data: addDriver) => Promise<void>;
  getDriversById: (id: number) => Promise<addDriver>;
};

export const DriversContext = createContext<DriversContextData>({
  drivers: [],
  removeDriver: async (id: number) => {},
  addDriver: async (data: addDriver) => {},
  editDriver: async (id: number, data: addDriver) => {},
  getDriversById: async (id: number) => {
    return {} as addDriver;
  },
});

export const DriversProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [drivers, setDrivers] = useState<allDrivers[]>([]);

  useEffect(() => {
    const fetchDrivers = async () => {
      try {
        const driversData = await getDrivers();
        setDrivers(driversData);
      } catch (error) {
        console.error("Error fetching drivers:", error);
      }
    };

    fetchDrivers();
  }, []);

  const removeDriver = async (id: number) => {
    try {
      await axios.delete(
        `https://localhost:7206/api/Drivers/DeleteDriver/${id}`
      );
      setDrivers(drivers.filter((x) => x.id !== id));
    } catch (error) {
      console.log(error);
    }
  };

  const addDriver = async (data: addDriver): Promise<void> => {
    try {
      const newDriver = await addDrivers(data);
      setDrivers((x) => [newDriver, ...x]);
    } catch (error) {}
  };

  const editDriver = async (id: number, data: addDriver): Promise<void> => {
    try {
      const updateDriver = await editDrivers(id, data);
      setDrivers((x) => {
        return x.map((acc) => (acc.id === id ? updateDriver : acc));
      });
    } catch (error) {}
  };
  return (
    <DriversContext.Provider
      value={{ drivers, removeDriver, addDriver, editDriver, getDriversById }}
    >
      {children}
    </DriversContext.Provider>
  );
};
