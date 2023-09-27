import React, { useContext, useEffect, useState } from "react";
import Button from "../../ui/Button";
import { useNavigate, useParams } from "react-router-dom";

import { addClient } from "../../api/interfaces";
import { initialValues } from "./AddClient";
import { ClientsContext } from "../../context/ClientsContext";

const EditClient = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { editClient, getClientById } = useContext(ClientsContext);
  const [addNewClient, setAddNewClient] = useState<addClient>(initialValues);

  useEffect(() => {
    const fetchClientData = async () => {
      try {
        const clientData = await getClientById(Number(id));

        setAddNewClient(clientData);
      } catch (error) {
        console.error("Error fetching client data:", error);
      }
    };

    fetchClientData();
  }, [getClientById, id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    const { name, value, type, checked } = e.target;
    const newValue = type === "checkbox" ? checked : value;
    setAddNewClient((prevValues) => ({ ...prevValues, [name]: newValue }));
  };

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ): Promise<void> => {
    e.preventDefault();
    try {
      await editClient(Number(id), addNewClient);
      navigate("/clients");
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
          onClick={() => navigate("/clients")}
        >
          Nazad
        </Button>
        <h1
          style={{
            paddingTop: "40px",
          }}
        >
          Izmijeni podatke klijenta
        </h1>
      </div>
      <form onSubmit={handleSubmit} className="form-container-basic">
        <div>
          <label>Ime</label>
          <input
            type="text"
            name="name"
            value={addNewClient.name}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Adresa</label>
          <input
            type="text"
            name="address"
            value={addNewClient.address}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Mjesto</label>
          <input
            type="text"
            name="place"
            value={addNewClient.place}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>PIB / JMBG</label>
          <input
            type="text"
            name="tinurn"
            value={addNewClient.tinurn}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Fax</label>
          <input
            type="text"
            name="fax"
            value={addNewClient.fax}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Telefon</label>
          <input
            type="text"
            name="phone"
            value={addNewClient.phone}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>PDV</label>
          <input
            type="number"
            name="vat"
            value={addNewClient.vat.toString()}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Email</label>
          <input
            type="email"
            name="email"
            value={addNewClient.email}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Ekskurzija</label>
          <input
            type="checkbox"
            name="excursion"
            checked={addNewClient.excursion}
            onChange={handleChange}
          />
        </div>
        <button type="submit">Sačuvaj izmjene</button>
      </form>
    </div>
  );
};

export default EditClient;
