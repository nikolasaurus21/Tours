import React, { useContext, useEffect, useState } from "react";
import Button from "../../ui/Button";
import { useNavigate, useParams } from "react-router-dom";
import { addVehicle } from "../../api/interfaces";
import { getVehicleById } from "../../api/api";
import { VehiclesContext } from "../../context/VehiclesContext";

const EditVehicle = () => {
  const { id } = useParams();
  const initialValues = {
    registration: "",
    name: "",
    note: "",
    numberOfSeats: "",
    fuel: "",
    mileage: 0,
  };
  const navigate = useNavigate();
  const { editVehicle } = useContext(VehiclesContext);
  const [addNewVehicle, setAddNewVehicle] = useState<addVehicle>(initialValues);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    const { name, value, type, checked } = e.target;
    const newValue = type === "checkbox" ? checked : value;
    setAddNewVehicle((prevValues) => ({ ...prevValues, [name]: newValue }));
  };

  useEffect(() => {
    const getSingleVehicle = async () => {
      try {
        const vehicles = await getVehicleById(Number(id));
        setAddNewVehicle(vehicles);
      } catch (error) {
        console.log(error);
      }
    };
    getSingleVehicle();
  }, [id]);

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ): Promise<void> => {
    e.preventDefault();
    try {
      await editVehicle(Number(id), addNewVehicle);
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
          Izmijeni vozilo
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

        <button type="submit">Sačuvaj izmjene</button>
      </form>
    </div>
  );
};

export default EditVehicle;
