import React, { useContext, useEffect, useState } from "react";
import TableReports from "../../ui/TableReports";
import { getReportsPerClient } from "../../api/api";
import { IReports, ListItem } from "../../api/interfaces";

import "../../ui/tablestyle.css";
import "./reports.css";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { ClientsContext } from "../../context/ClientsContext";
import Pagination from "../../ui/Pagination";
import { usePagination } from "../../context/PaginationContext";

const ClientReports = () => {
  const navigate = useNavigate();
  const { clients } = useContext(ClientsContext);

  const [getclients, setGetClients] = useState<IReports[]>([]);
  const [clientData, setClientData] = useState<ListItem[]>([]);
  const [clientid, setClientid] = useState<number | null>(null);
  const [selectedClientName, setSelectedClientName] = useState<string>("");

  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(getclients.length / rowsPerPage);

  const displayedClients = getclients.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  useEffect(() => {
    const extractedData: ListItem[] = clients.map((client) => ({
      id: client.id,
      name: client.name,
    }));
    setClientData(extractedData);
  }, [clients]);

  const handleClientSelected = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedClientId = parseInt(e.target.value, 10);
    setClientid(selectedClientId);
  };

  const handleShowButtonClick = async () => {
    try {
      if (clientid !== null) {
        const clientsData = await getReportsPerClient(clientid);
        setGetClients(clientsData);

        const selectedClient = clientData.find(
          (client) => client.id === clientid
        );
        setSelectedClientName(selectedClient ? selectedClient.name : "");
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div>
      <Button
        buttonStyle={{
          backgroundColor: "rgb(100,100,100)",
          marginLeft: "10px",
          marginTop: "10px",
        }}
        onClick={() => navigate("/reports")}
      >
        Nazad
      </Button>
      <h1>Izvještaji za klijenta: {selectedClientName}</h1>
      <div className="table-container">
        <div className="selection">
          <span>Prikaži putne naloge za klijenta:</span>
          <select onChange={handleClientSelected}>
            <option value="">Izaberi klijenta...</option>
            {clientData.map((client) => (
              <option key={client.id} value={client.id}>
                {client.name}
              </option>
            ))}
          </select>

          <span>
            <Button
              buttonStyle={{ marginBottom: "7px" }}
              onClick={handleShowButtonClick}
            >
              Prikaži
            </Button>
            <Button
              buttonStyle={{
                marginBottom: "7px",
                marginLeft: "5px",
              }}
            >
              Štampaj
            </Button>
          </span>
        </div>

        <TableReports data={displayedClients} />
        <Pagination totalPages={totalPages} />
      </div>
    </div>
  );
};

export default ClientReports;
