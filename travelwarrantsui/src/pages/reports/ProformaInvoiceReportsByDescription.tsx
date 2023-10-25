import React, { useState } from "react";
import {
  AiFillPrinter,
  AiFillDelete,
  AiOutlineArrowLeft,
  AiOutlineArrowRight,
} from "react-icons/ai";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { Invoices } from "../../api/interfaces";
import {
  downloadPdfProformaInvoice,
  getProformaInvoiceByDescription,
  proformaInvoiceToDelete,
} from "../../api/api";

const ProformaInvoiceReportsByDescription = () => {
  const navigate = useNavigate();
  const [proformaInvoices, setProformaInvoices] = useState<Invoices[]>([]);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [description, setDescription] = useState<string>("");
  const [enteredDescription, setEnteredDescription] = useState<string>("");
  const handleDestinationChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setDescription(event.target.value);
  };

  const handleShowButtonClick = async () => {
    if (description.trim() === "") {
      setProformaInvoices([]);
      setEnteredDescription("");
    } else {
      const data = await getProformaInvoiceByDescription(
        description,
        currentPage
      );
      setProformaInvoices(data);
      setTotalPages(Math.ceil(data.length / 10));

      setEnteredDescription(description);
    }
  };
  const handleDeleteClick = async (id: number) => {
    const inoviceData = await proformaInvoiceToDelete(id);
    navigate(`/proformainoivces/delete/${id}`, { state: { inoviceData } });
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
        Profakture {enteredDescription && `sa uslugom: ${enteredDescription}`}
      </h1>

      <div className="table-container">
        <div className="selection">
          <span>Prikaži profakture po opisu usluge:</span>
          <input
            type="text"
            name="basis"
            value={description}
            onChange={handleDestinationChange}
            placeholder="Opis usluge..."
          />

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

export default ProformaInvoiceReportsByDescription;
