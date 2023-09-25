import { createContext, useEffect, useState } from "react";
import { addTravelWarrant, allTravelWarrants } from "../api/interfaces";
import { addWarrant, editWarrant, getTravelWarrants } from "../api/api";
import axios, { AxiosError } from "axios";
import PopUp from "../ui/PopUp";

export type TravelWarrantsContextData = {
  travelwarrants: allTravelWarrants[];
  removeWarrant: (id: number) => Promise<void>;
  addTour: (data: addTravelWarrant) => Promise<void>;
  editTour: (id: number, data: addTravelWarrant) => Promise<void>;
};

export const TravelWarrantsContext = createContext<TravelWarrantsContextData>({
  travelwarrants: [],
  removeWarrant: async (id: number) => {},
  addTour: async (data: addTravelWarrant) => {},
  editTour: async (id: number, data: addTravelWarrant) => {},
});

export const TravelWarrantsProvider = ({
  children,
}: {
  children: React.ReactNode;
}) => {
  const [travelwarrants, setTravelwarrants] = useState<allTravelWarrants[]>([]);
  const [isPopupOpen, setPopupOpen] = useState<boolean>(false);

  useEffect(() => {
    const fetchWarrants = async () => {
      try {
        const warrantsData = await getTravelWarrants();
        setTravelwarrants(warrantsData);
      } catch (error) {
        console.log(error);
      }
    };
    fetchWarrants();
  }, []);

  const removeWarrant = async (id: number) => {
    try {
      await axios.delete(
        `https://localhost:7206/api/TravelWarrants/DeleteTour/${id}`
      );
      setTravelwarrants(travelwarrants.filter((x) => x.id !== id));
    } catch (error) {
      console.log(error);
    }
  };

  const addTour = async (data: addTravelWarrant): Promise<void> => {
    try {
      const newTour = await addWarrant(data);
      setTravelwarrants((x) => [newTour, ...x]);
    } catch (error) {
      const axiosError = error as AxiosError;
      if (axiosError.response && axiosError.response.status === 400) {
        setPopupOpen(true);
      }
    }
  };

  const editTour = async (
    id: number,
    data: addTravelWarrant
  ): Promise<void> => {
    try {
      const newTour = await editWarrant(id, data);
      setTravelwarrants((x) => {
        return x.map((tour) => (tour.id === id ? newTour : tour));
      });
    } catch (error) {
      console.log(error);
    }
  };
  return (
    <TravelWarrantsContext.Provider
      value={{
        travelwarrants,
        removeWarrant,
        addTour,
        editTour,
      }}
    >
      {isPopupOpen && (
        <PopUp isOpen={isPopupOpen} onClose={() => setPopupOpen(false)} />
      )}
      {children}
    </TravelWarrantsContext.Provider>
  );
};
