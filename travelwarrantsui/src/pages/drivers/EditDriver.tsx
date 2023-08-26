import React, { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { addDriver } from "../../api/interfaces";
import { initialValues } from "../clients/AddClient";
import Button from "../../ui/Button";
import { DriversContext } from "../../context/DriversContext";

const EditDriver = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { editDriver, getDriversById } = useContext(DriversContext);
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
      await editDriver(Number(id), addNewDriver);

      navigate("/drivers");
    } catch (error) {
      console.error("Error adding client:", error);
    }
  };

  useEffect(() => {
    const getSingleDriver = async () => {
      try {
        const driverData = await getDriversById(Number(id));
        setAddNewDriver(driverData);
      } catch (error) {
        console.log(error);
      }
    };
    getSingleDriver();
  }, [getDriversById, id]);

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
          <h1>Izmijeni vozača</h1>
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

          <button type="submit">Sačuvaj izmjene</button>
        </form>
      </div>
    </div>
  );
};

export default EditDriver;
