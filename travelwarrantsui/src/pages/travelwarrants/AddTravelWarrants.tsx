import React, { useContext, useEffect, useState } from "react";
import "./travelform.css";
import {
  ListItem,
  ListItemVehicle,
  addTravelWarrant,
} from "../../api/interfaces";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";

import { ClientsContext } from "../../context/ClientsContext";
import { VehiclesContext } from "../../context/VehiclesContext";
import { DriversContext } from "../../context/DriversContext";
import { TravelWarrantsContext } from "../../context/ToursContext";

const defalutValues = {
  departure: "",
  destination: "",
  mileage: 0,
  numberOfPassengers: 0,
  price: 0,
  toll: 0,
  fuel: 0,
  timeOfTour: "",
  startMileage: 0,
  endMileage: 0,
  note: "",
  intermediateDestinations: "",
  driverId: 0,
  vehicleId: 0,
  clientId: 0,
  fuelPrice: 0,
  numberOfDays: 0,
};

const AddTravelWarrants = () => {
  const navigate = useNavigate();

  const { drivers } = useContext(DriversContext);
  const { clients } = useContext(ClientsContext);
  const { vehicles } = useContext(VehiclesContext);
  const { addTour } = useContext(TravelWarrantsContext);

  const [clientData, setClientData] = useState<ListItem[]>([]);
  const [driverData, setDriverData] = useState<ListItem[]>([]);
  const [vehicleData, setVehicleData] = useState<ListItemVehicle[]>([]);

  const [errors, setErrors] = useState({
    departure: "",
    destination: "",
    mileage: "",
    startMileage: "",
    endMileage: "",
    timeOfTour: "",
    vehicleId: "",
    clientId: "",
    driverId: "",
  });

  const [addTravelWarrant, setAddTravelWarrant] =
    useState<addTravelWarrant>(defalutValues);

  useEffect(() => {
    const extractedData: ListItem[] = clients.map((client) => ({
      id: client.id,
      name: client.name,
    }));
    setClientData(extractedData);
  }, [clients]);

  useEffect(() => {
    const extractedData: ListItem[] = drivers.map((driver) => ({
      id: driver.id,
      name: driver.name,
    }));
    setDriverData(extractedData);
  }, [drivers]);

  useEffect(() => {
    const extractedData: ListItemVehicle[] = vehicles.map((vehicle) => ({
      id: vehicle.id,
      name: vehicle.registration,
      mileage: vehicle.mileage,
    }));
    setVehicleData(extractedData);
  }, [vehicles]);

  const handleClientSelected = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedClientId = parseInt(e.target.value, 10);
    setAddTravelWarrant((prevData) => ({
      ...prevData,
      clientId: selectedClientId,
    }));
  };

  const handleDriverSelected = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedDriverId = parseInt(e.target.value, 10);
    setAddTravelWarrant((prevData) => ({
      ...prevData,
      driverId: selectedDriverId,
    }));
  };

  const handleVehicleSelected = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedVehicleId = parseInt(e.target.value, 10);
    const selectedVehicle = vehicleData.find(
      (vehicle) => vehicle.id === selectedVehicleId
    );
    if (selectedVehicle) {
      setAddTravelWarrant((prevData) => ({
        ...prevData,
        vehicleId: selectedVehicle.id,
        startMileage: selectedVehicle.mileage,
        endMileage: Number(prevData.mileage) + Number(selectedVehicle.mileage),
      }));
    }
  };

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setAddTravelWarrant((prevData) => ({ ...prevData, [name]: value }));
  };

  const validateForm = () => {
    let isValid = true;
    const newErrors = { ...errors };

    if (!addTravelWarrant.departure.trim()) {
      newErrors.departure = "Departure is required.";
      isValid = false;
    }

    if (!addTravelWarrant.destination.trim()) {
      newErrors.destination = "Destination is required.";
      isValid = false;
    }

    if (addTravelWarrant.mileage <= 0) {
      newErrors.mileage = "Mileage must be greater than 0.";
      isValid = false;
    }

    if (addTravelWarrant.startMileage < 0) {
      newErrors.startMileage = "Start mileage must be greater than 0.";
      isValid = false;
    }

    if (addTravelWarrant.endMileage <= 0) {
      newErrors.endMileage = "End mileage must be greater than 0.";
      isValid = false;
    }

    if (!addTravelWarrant.timeOfTour) {
      newErrors.timeOfTour = "Time of tour is required.";
      isValid = false;
    }

    if (addTravelWarrant.vehicleId === 0) {
      newErrors.vehicleId = "Please select a vehicle.";
      isValid = false;
    }

    if (addTravelWarrant.clientId === 0) {
      newErrors.clientId = "Please select a client.";
      isValid = false;
    }

    if (addTravelWarrant.driverId === 0) {
      newErrors.driverId = "Please select a driver.";
      isValid = false;
    }

    setErrors(newErrors);
    return isValid;
  };

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ): Promise<void> => {
    e.preventDefault();

    if (!validateForm()) {
      return;
    }
    try {
      await addTour(addTravelWarrant);

      navigate("/");
    } catch (error) {
      console.error("Error adding client:", error);
    }
  };

  return (
    <div>
      <div>
        <Button
          buttonStyle={{
            backgroundColor: "rgb(100,100,100)",
            marginLeft: "10px",
            marginTop: "10px",
          }}
          onClick={() => navigate("/")}
        >
          Nazad
        </Button>
        <h1 style={{ padding: "0px" }}>Novi putni nalog</h1>
      </div>
      <form onSubmit={handleSubmit} className="form-container">
        <div className="left-side-tw">
          <div>
            <label>Klijent</label>
            <select
              name="clientId"
              value={addTravelWarrant.clientId}
              onChange={handleClientSelected}
            >
              <option value="0">Izaberi klijenta...</option>
              {clientData.map((client) => (
                <option key={client.id} value={client.id}>
                  {client.name}
                </option>
              ))}
            </select>
            {errors.clientId && (
              <span className="error">{errors.clientId}</span>
            )}
          </div>
          <div>
            <label>Datum i vrijeme</label>
            <input
              type="datetime-local"
              name="timeOfTour"
              value={addTravelWarrant.timeOfTour}
              onChange={handleChange}
            />
            {errors.timeOfTour && (
              <span className="error">{errors.timeOfTour}</span>
            )}
          </div>
          <div>
            <label>Polazak</label>
            <input
              type="text"
              name="departure"
              value={addTravelWarrant.departure}
              onChange={handleChange}
            />
          </div>
          {errors.departure && (
            <span className="error">{errors.departure}</span>
          )}
          <div>
            <label>Destinacija</label>
            <input
              type="text"
              name="destination"
              value={addTravelWarrant.destination}
              onChange={handleChange}
            />
            {errors.destination && (
              <span className="error">{errors.destination}</span>
            )}
          </div>
          <div>
            <label>Međudestinacije:</label>
            <input
              type="text"
              name="intermediateDestinations"
              value={addTravelWarrant.intermediateDestinations}
              onChange={handleChange}
            />
          </div>
          <div>
            <label>Kilometara</label>
            <input
              type="number"
              name="mileage"
              value={addTravelWarrant.mileage}
              onChange={handleChange}
            />
            {errors.mileage && <span className="error">{errors.mileage}</span>}
          </div>
          <div>
            <label>Broj putnika</label>
            <input
              type="number"
              name="numberOfPassengers"
              value={addTravelWarrant.numberOfPassengers}
              onChange={handleChange}
            />
          </div>
          <div>
            <label>Cijena</label>
            <input
              type="number"
              name="price"
              value={addTravelWarrant.price}
              onChange={handleChange}
            />
          </div>
          <div>
            <label>Putarina</label>
            <input
              type="number"
              name="toll"
              value={addTravelWarrant.toll}
              onChange={handleChange}
            />
          </div>
        </div>
        <div className="right-side-tw">
          <div>
            <label>Gorivo (kiločina)</label>
            <input
              type="number"
              name="fuel"
              value={addTravelWarrant.fuel}
              onChange={handleChange}
            />
          </div>
          <div>
            <label>Gorivo (cijena)</label>
            <input
              type="number"
              name="fuelPrice"
              value={addTravelWarrant.fuelPrice}
              onChange={handleChange}
            />
          </div>
          <div>
            <label>Vozač</label>
            <select
              name="driverId"
              value={addTravelWarrant.driverId}
              onChange={handleDriverSelected}
            >
              <option value="0">Izaberi vozača...</option>
              {driverData.map((driver) => (
                <option key={driver.id} value={driver.id}>
                  {driver.name}
                </option>
              ))}
            </select>
            {errors.driverId && (
              <span className="error">{errors.driverId}</span>
            )}
          </div>
          <div>
            <label>Vozilo</label>
            <select
              name="vehicleId"
              value={addTravelWarrant.vehicleId}
              onChange={handleVehicleSelected}
            >
              <option value="0">Izaberi vozilo...</option>
              {vehicleData.map((vehicle) => (
                <option key={vehicle.id} value={vehicle.id}>
                  {vehicle.name}
                </option>
              ))}
            </select>
            {errors.vehicleId && (
              <span className="error">{errors.vehicleId}</span>
            )}
          </div>
          <div>
            <label>Stara kilometraža</label>
            <input
              type="number"
              name="startMileage"
              value={addTravelWarrant.startMileage}
              onChange={handleChange}
            />
            {errors.startMileage && (
              <span className="error">{errors.startMileage}</span>
            )}
          </div>
          <div>
            <label>Nova kilometraža</label>
            <input
              type="number"
              name="endMileage"
              value={addTravelWarrant.endMileage}
              onChange={handleChange}
            />
            {errors.endMileage && (
              <span className="error">{errors.endMileage}</span>
            )}
          </div>
          <div>
            <label>Napomena</label>
            <input
              type="text"
              name="note"
              value={addTravelWarrant.note}
              onChange={handleChange}
            />
          </div>
          <div>
            <label>Broj dana</label>
            <input
              type="number"
              name="numberOfDays"
              value={addTravelWarrant.numberOfDays}
              onChange={handleChange}
            />
          </div>
        </div>
        <div>
          <button type="submit">Dodaj nalog</button>
        </div>
      </form>
    </div>
  );
};

export default AddTravelWarrants;
