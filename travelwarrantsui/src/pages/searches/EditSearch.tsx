import React, { useContext, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { ListItem, addPayment } from "../../api/interfaces";
import { getSearchById } from "../../api/api";
import Button from "../../ui/Button";
import { ClientsContext } from "../../context/ClientsContext";
import { SearchesContext } from "../../context/SearchesContext";
const defaultValues = {
  clientId: 0,
  date: "",
  amount: 0,
  basis: "",
};
const EditSearch = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { clients } = useContext(ClientsContext);
  const { editSearch } = useContext(SearchesContext);
  const [clientData, setClientData] = useState<ListItem[]>([]);
  const [addSearch, setAddSearch] = useState<addPayment>(defaultValues);

  useEffect(() => {
    const extractedData: ListItem[] = clients.map((client) => ({
      id: client.id,
      name: client.name,
    }));
    setClientData(extractedData);
  }, [clients]);

  useEffect(() => {
    const fetchSearch = async () => {
      try {
        const searchData = await getSearchById(Number(id));

        setAddSearch(searchData);
      } catch (error) {
        console.error("Error fetching client data:", error);
      }
    };

    fetchSearch();
  }, [id]);

  const handleClientSelected = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedClientId = parseInt(e.target.value, 10);
    setAddSearch((prevData) => ({
      ...prevData,
      clientId: selectedClientId,
    }));
  };

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target;
    setAddSearch((prevData) => ({ ...prevData, [name]: value }));
  };

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ): Promise<void> => {
    e.preventDefault();
    try {
      await editSearch(Number(id), addSearch);

      navigate("/searches");
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
          onClick={() => navigate("/searches")}
        >
          Nazad
        </Button>
        <h1 style={{ padding: "0px" }}>Izmjena potraživanja</h1>
      </div>
      <form onSubmit={handleSubmit} className="form-container-basic">
        <div>
          <div>
            <label>Datum i vrijeme</label>
            <input
              type="datetime-local"
              name="date"
              value={addSearch.date}
              onChange={handleChange}
            />
          </div>
          <label>Klijent</label>
          <select
            name="clientId"
            value={addSearch.clientId}
            onChange={handleClientSelected}
          >
            <option value="0">Izaberi klijenta...</option>
            {clientData.map((client) => (
              <option key={client.id} value={client.id}>
                {client.name}
              </option>
            ))}
          </select>
        </div>

        <div>
          <label>Iznos</label>
          <input
            type="number"
            name="amount"
            value={addSearch.amount}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Osnov</label>
          <input
            type="text"
            name="basis"
            value={addSearch.basis}
            onChange={handleChange}
          />
        </div>
        <div>
          <button type="submit">Sačuvaj izmjene</button>
        </div>
      </form>
    </div>
  );
};

export default EditSearch;
