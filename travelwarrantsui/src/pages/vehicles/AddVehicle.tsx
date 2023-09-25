import React, { useContext, useState } from "react";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { addVehicle } from "../../api/interfaces";
import { VehiclesContext } from "../../context/VehiclesContext";

const initialValues = {
  registration: "",
  name: "",
  note: "",
  numberOfSeats: "",
  fuel: "",
  mileage: 0,
};
const AddVehicle = () => {
  const navigate = useNavigate();
  const { addVehicle } = useContext(VehiclesContext);

  const [addNewVehicle, setAddNewVehicle] = useState<addVehicle>(initialValues);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    const { name, value, type, checked } = e.target;
    const newValue = type === "checkbox" ? checked : value;
    setAddNewVehicle((prevValues) => ({ ...prevValues, [name]: newValue }));
  };

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ): Promise<void> => {
    e.preventDefault();
    try {
      await addVehicle(addNewVehicle);

      navigate("/vehicles");
    } catch (error) {
      console.error("Error adding client:", error);
    }
  };

  return (
    <div>
      <div>
        <Button
          buttonStyle={{
            marginLeft: "15px",
            marginTop: "15px",
            backgroundColor: "rgb(100,100,100)",
          }}
          onClick={() => navigate("/vehicles")}
        >
          Nazad
        </Button>
        <h1
          style={{
            paddingTop: "40px",
          }}
        >
          Novo vozilo
        </h1>
      </div>
      <form onSubmit={handleSubmit} className="form-container-basic">
        <div>
          <label>Registracija</label>
          <input
            type="text"
            name="registration"
            value={addNewVehicle.registration}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Naziv</label>
          <input
            type="text"
            name="name"
            value={addNewVehicle.name}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Napomena</label>
          <input
            type="text"
            name="note"
            value={addNewVehicle.note}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Broj mjesta</label>
          <input
            type="text"
            name="numberOfSeats"
            value={addNewVehicle.numberOfSeats}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Potrošnja</label>
          <input
            type="text"
            name="fuel"
            value={addNewVehicle.fuel}
            onChange={handleChange}
          />
        </div>

        <div>
          <label>Kilometraža</label>
          <input
            type="number"
            name="mileage"
            value={addNewVehicle.mileage}
            onChange={handleChange}
          />
        </div>

        <button type="submit">Dodaj vozilo</button>
      </form>
    </div>
  );
};

export default AddVehicle;
