import React, { useContext, useState } from "react";
import { addDriver } from "../../api/interfaces";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { DriversContext } from "../../context/DriversContext";

const initialValues = {
  name: "",
  phone: "",
};
const AddDriver = () => {
  const navigate = useNavigate();
  const { addDriver } = useContext(DriversContext);

  const [addNewDriver, setAddNewDriver] = useState<addDriver>(initialValues);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    const { name, value, type, checked } = e.target;
    const newValue = type === "checkbox" ? checked : value;
    setAddNewDriver((prevValues) => ({ ...prevValues, [name]: newValue }));
  };
  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ): Promise<void> => {
    e.preventDefault();
    try {
      await addDriver(addNewDriver);
      navigate("/drivers");
    } catch (error) {
      console.error("Error adding client:", error);
    }
  };
  return (
    <div>
      <div>
        <div>
          <Button
            buttonStyle={{
              backgroundColor: "rgb(100,100,100)",
              marginTop: "10px",
              marginLeft: "10px",
            }}
            onClick={() => navigate("/drivers")}
          >
            Nazad
          </Button>
          <h1>Novi vozač</h1>
        </div>
        <form onSubmit={handleSubmit} className="form-container-basic">
          <div>
            <label>Ime</label>
            <input
              type="text"
              name="name"
              value={addNewDriver.name}
              onChange={handleChange}
            />
          </div>
          <div>
            <label>Telefon</label>
            <input
              type="text"
              name="phone"
              value={addNewDriver.phone}
              onChange={handleChange}
            />
          </div>

          <button type="submit">Dodaj vozača</button>
        </form>
      </div>
    </div>
  );
};

export default AddDriver;
