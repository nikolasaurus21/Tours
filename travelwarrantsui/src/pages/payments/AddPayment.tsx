import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { ListItem, addPayment } from "../../api/interfaces";
import Button from "../../ui/Button";
import "../../ui/form.css";
import { ClientsContext } from "../../context/ClientsContext";
import { PaymentsContext } from "../../context/PaymentsContext";
import { CompanyContext } from "../../context/CompanyContext";
import PopUp from "../../ui/PopUp";

const currentDate = new Date();
const formattedDate = `${currentDate.getFullYear()}-${String(
  currentDate.getMonth() + 1
).padStart(2, "0")}-${String(currentDate.getDate()).padStart(2, "0")}T${String(
  currentDate.getHours()
).padStart(2, "0")}:${String(currentDate.getMinutes()).padStart(2, "0")}`;

const defaultValues = {
  clientId: 0,
  date: formattedDate,
  amount: 0,
  basis: "",
};
const AddPayment = () => {
  const navigate = useNavigate();
  const { clients } = useContext(ClientsContext);
  const { addNewPayment } = useContext(PaymentsContext);
  const { isCompanyAdded, isPopUpOpen, setIsPopUpOpen } =
    useContext(CompanyContext);

  const [clientData, setClientData] = useState<ListItem[]>([]);
  const [addPayment, setAddPayment] = useState<addPayment>(defaultValues);

  useEffect(() => {
    const extractedData: ListItem[] = clients.map((client) => ({
      id: client.id,
      name: client.name,
    }));
    setClientData(extractedData);
  }, [clients]);

  const handleClientSelected = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedClientId = parseInt(e.target.value, 10);
    setAddPayment((prevData) => ({
      ...prevData,
      clientId: selectedClientId,
    }));
  };

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setAddPayment((prevData) => ({ ...prevData, [name]: value }));
  };

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ): Promise<void> => {
    e.preventDefault();
    try {
      if (!isCompanyAdded) {
        setIsPopUpOpen(true);
      } else {
        await addNewPayment(addPayment);
        navigate("/payments");
      }
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
          onClick={() => navigate("/payments")}
        >
          Nazad
        </Button>
        <h1 style={{ padding: "0px" }}>Nova uplata</h1>
      </div>
      <form onSubmit={handleSubmit} className="form-container-basic">
        <div>
          <label>Datum i vrijeme</label>
          <input
            type="datetime-local"
            name="date"
            value={addPayment.date}
            onChange={handleChange}
          />
        </div>
        <label>Klijent</label>
        <select
          name="clientId"
          value={addPayment.clientId}
          onChange={handleClientSelected}
        >
          <option value="0">Izaberite klijenta...</option>
          {clientData.map((client) => (
            <option key={client.id} value={client.id}>
              {client.name}
            </option>
          ))}
        </select>

        <div>
          <label>Iznos</label>
          <input
            type="number"
            name="amount"
            value={addPayment.amount}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Osnov</label>
          <input
            type="text"
            name="basis"
            value={addPayment.basis}
            onChange={handleChange}
          />
        </div>
        <div>
          <button type="submit">Dodaj uplatu</button>
        </div>
      </form>
      <PopUp isOpen={isPopUpOpen} onClose={() => setIsPopUpOpen(false)} />
    </div>
  );
};

export default AddPayment;
