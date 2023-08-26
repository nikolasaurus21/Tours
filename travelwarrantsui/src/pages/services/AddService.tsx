import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { addService } from "../../api/interfaces";
import Button from "../../ui/Button";
import { ServicesContext } from "../../context/ServicesContext";
import PopUp from "../../ui/PopUp";
import { CompanyContext } from "../../context/CompanyContext";
const initialValues = {
  name: "",
  vat: 0,
};
const AddService = () => {
  const navigate = useNavigate();
  const { addNewService } = useContext(ServicesContext);
  const { isCompanyAdded, isPopUpOpen, setIsPopUpOpen } =
    useContext(CompanyContext);
  const [addService, setService] = useState<addService>(initialValues);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    const { name, value, type, checked } = e.target;
    const newValue = type === "checkbox" ? checked : value;
    setService((prevValues) => ({ ...prevValues, [name]: newValue }));
  };

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ): Promise<void> => {
    e.preventDefault();
    try {
      if (!isCompanyAdded) {
        setIsPopUpOpen(true);
      } else {
        await addNewService(addService);

        navigate("/services");
      }
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
            onClick={() => navigate("/services")}
          >
            Nazad
          </Button>
          <h1>Nova usluga</h1>
        </div>
        <form onSubmit={handleSubmit} className="form-container-basic">
          <div>
            <label>Naziv</label>
            <input
              type="text"
              name="name"
              value={addService.name}
              onChange={handleChange}
            />
          </div>
          <div>
            <label>PDV</label>
            <input
              type="text"
              name="vat"
              value={addService.vat}
              onChange={handleChange}
            />
          </div>

          <button type="submit">Dodaj uslugu</button>
        </form>
        <PopUp isOpen={isPopUpOpen} onClose={() => setIsPopUpOpen(false)} />
      </div>
    </div>
  );
};

export default AddService;
