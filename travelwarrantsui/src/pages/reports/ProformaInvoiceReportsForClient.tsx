import React, { useContext, useEffect, useState } from "react";
import Button from "../../ui/Button";
import {
  AiFillPrinter,
  AiFillDelete,
  AiOutlineArrowLeft,
  AiOutlineArrowRight,
} from "react-icons/ai";
import { useNavigate } from "react-router-dom";
import { ClientsContext } from "../../context/ClientsContext";
import { Inovices, ListItem } from "../../api/interfaces";
import {
  downloadPdfProformaInvoice,
  getProformaInvoiceForClients,
  proformaInvoiceToDelete,
} from "../../api/api";

const ProformaInvoiceReportsForClient = () => {
  const navigate = useNavigate();

  const { clients } = useContext(ClientsContext);

  const [proformaInvoices, setProformaInvoices] = useState<Inovices[]>([]);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [clientid, setClientid] = useState<number | null>(null);
  const [clientData, setClientData] = useState<ListItem[]>([]);
  const [selectedClientName, setSelectedClientName] = useState<string>("");

  const handleShowButtonClick = async () => {
    try {
      if (clientid !== null) {
        const inovicesData = await getProformaInvoiceForClients(
          clientid,
          currentPage
        );
        setProformaInvoices(inovicesData);
        setTotalPages(Math.ceil(inovicesData.length / 10));
        const selectedClient = clientData.find(
          (client) => client.id === clientid
        );
        setSelectedClientName(selectedClient ? selectedClient.name : "");
      }
    } catch (error) {
      console.log(error);
    }
  };

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

  const handleDeleteClick = async (id: number) => {
    const inoviceData = await proformaInvoiceToDelete(id);
    navigate(`/proformainvoices/delete/${id}`, { state: { inoviceData } });
  };

  const handleDownloadClick = async (id: number, inoviceNumber: string) => {
    const success = await downloadPdfProformaInvoice(id, inoviceNumber);
    if (!success) {
      console.log("Neuspešno preuzimanje PDF-a");
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
      <h1>
        Profakture {selectedClientName && `za klijenta: ${selectedClientName}`}
      </h1>

      <div className="table-container">
        <div className="selection">
          <span>Prikaži profakture za klijenta:</span>
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
        <table>
          <thead>
            <tr>
              <th>Broj</th>
              <th>Datum</th>
              <th>Klijent</th>
              <th>Iznos</th>
              <th>Štampaj</th>
              <th>Obriši</th>
            </tr>
          </thead>
          <tbody>
            {proformaInvoices.map((inovice) => (
              <tr key={inovice.id} className="firstcolumnorrow">
                <td
                  onClick={() =>
                    navigate(`/proformainvoices/edit/${inovice.id}`)
                  }
                >
                  {inovice.number}/{inovice.year}
                </td>
                <td
                  onClick={() =>
                    navigate(`/proformainvoices/edit/${inovice.id}`)
                  }
                >
                  {inovice.date}
                </td>
                <td
                  onClick={() =>
                    navigate(`/proformainvoices/edit/${inovice.id}`)
                  }
                >
                  {inovice.clientName}
                </td>
                <td
                  onClick={() =>
                    navigate(`/proformainvoices/edit/${inovice.id}`)
                  }
                >
                  {inovice.amount}
                </td>
                <td
                  className="print-cell"
                  onClick={() =>
                    handleDownloadClick(
                      inovice.id,
                      `${inovice.number}/${inovice.year}`
                    )
                  }
                >
                  <AiFillPrinter style={{ color: "whitesmoke" }} />
                </td>
                <td
                  className="delete-cell"
                  onClick={() => handleDeleteClick(inovice.id)}
                >
                  <AiFillDelete style={{ color: "whitesmoke" }} />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div className="paginationInoivice">
        <button
          className={`page-button ${currentPage === 1 ? "disabled" : ""}`}
          onClick={() => setCurrentPage((prev) => Math.max(prev - 1, 1))}
          disabled={currentPage === 1}
        >
          <AiOutlineArrowLeft />
        </button>
        <span className="page-info">
          {currentPage} / {totalPages}
        </span>
        <button
          className={`page-button ${
            currentPage === totalPages ? "disabled" : ""
          }`}
          onClick={() =>
            setCurrentPage((prev) => Math.min(prev + 1, totalPages))
          }
          disabled={currentPage === totalPages}
        >
          <AiOutlineArrowRight />
        </button>
      </div>
    </div>
  );
};

export default ProformaInvoiceReportsForClient;
