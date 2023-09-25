import { useContext, useState } from "react";
import { addClient } from "../../api/interfaces";
import "../../ui/form.css";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { ClientsContext } from "../../context/ClientsContext";

export const initialValues: addClient = {
  name: "",
  address: "",
  place: "",
  tinurn: "",
  fax: "",
  phone: "",
  email: "",
  vat: 0,
  excursion: false,
};
const AddClient = () => {
  const { addClient } = useContext(ClientsContext);

  const navigate = useNavigate();
  const [addNewClient, setAddNewClient] = useState<addClient>(initialValues);

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
      await addClient(addNewClient);
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
          Novi klijent
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
        <button type="submit">Dodaj klijenta</button>
      </form>
    </div>
  );
};

export default AddClient;
