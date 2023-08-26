import React, { createContext, useState, useEffect } from "react";
import { addVehicle, allVehicles } from "../api/interfaces";
import { addVehicles, editVehicles, getVehicles } from "../api/api";
import axios from "axios";

export type VehiclesContextData = {
  vehicles: allVehicles[];
  removeVehicle: (id: number) => Promise<void>;
  addVehicle: (data: addVehicle) => Promise<void>;
  editVehicle: (id: number, data: addVehicle) => Promise<void>;
};

export const VehiclesContext = createContext<VehiclesContextData>({
  vehicles: [],
  removeVehicle: async (id: number) => {},
  addVehicle: async (data: addVehicle) => {},
  editVehicle: async (id: number, data: addVehicle) => {},
});

export const VehiclesProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [vehicles, setVehicles] = useState<allVehicles[]>([]);

  useEffect(() => {
    const fetchVehicles = async () => {
      try {
        const vehiclesData = await getVehicles();
        setVehicles(vehiclesData);
      } catch (error) {
        console.error("Error fetching vehicles:", error);
      }
    };

    fetchVehicles();
  }, []);

  const removeVehicle = async (id: number) => {
    try {
      await axios.delete(
        `https://localhost:7206/api/Fleet/DeleteVehicle/${id}`
      );
      setVehicles(vehicles.filter((x) => x.id !== id));
    } catch (error) {
      console.log(error);
    }
  };

  const addVehicle = async (data: addVehicle): Promise<void> => {
    try {
      const newVehicle = await addVehicles(data);
      setVehicles((prevVehicles) => [newVehicle, ...prevVehicles]);
    } catch (error) {
      console.log(error);
    }
  };

  const editVehicle = async (id: number, data: addVehicle): Promise<void> => {
    try {
      const updatedVehicle = await editVehicles(id, data);
      setVehicles((x) => {
        return x.map((acc) => (acc.id === id ? updatedVehicle : acc));
      });
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <VehiclesContext.Provider
      value={{ vehicles, removeVehicle, addVehicle, editVehicle }}
    >
      {children}
    </VehiclesContext.Provider>
  );
};
