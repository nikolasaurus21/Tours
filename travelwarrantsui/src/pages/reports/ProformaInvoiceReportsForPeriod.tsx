import { format } from "date-fns";
import React, { useState } from "react";
import {
  AiFillPrinter,
  AiFillDelete,
  AiOutlineArrowLeft,
  AiOutlineArrowRight,
} from "react-icons/ai";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { Inovices } from "../../api/interfaces";
import {
  getProformaInvoiceForPeriod,
  proformaInvoiceToDelete,
  downloadPdfProformaInvoice,
} from "../../api/api";

const ProformaInvoiceReportsForPeriod = () => {
  const navigate = useNavigate();

  const [proformaInvoices, setProformaInvoices] = useState<Inovices[]>([]);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(1);

  const [fromDate, setFromDate] = useState<string>("");
  const [toDate, setToDate] = useState<string>("");

  const [enteredFrom, setEnteredFrom] = useState<string>("");
  const [enteredTo, setEnteredTo] = useState<string>("");

  const handleFromDateChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setFromDate(e.target.value);
  };

  const handleToDateChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setToDate(e.target.value);
  };

  const handleShowButtonClick = async () => {
    try {
      const from = new Date(fromDate);
      const to = new Date(toDate);
      const inovicesData = await getProformaInvoiceForPeriod(
        from,
        to,
        currentPage
      );
      setProformaInvoices(inovicesData);
      setTotalPages(Math.ceil(inovicesData.length / 10));
      setEnteredFrom(format(new Date(from), "dd/MM/yyyy "));
      setEnteredTo(format(new Date(to), "dd/MM/yyyy  "));
    } catch (error) {
      console.error("Error fetching reports:", error);
      setProformaInvoices([]);
    }
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
        Profakture{" "}
        {enteredFrom && enteredTo
          ? `za period: ${enteredFrom} - ${enteredTo}`
          : ""}
      </h1>

      <div className="table-container">
        <div className="selection">
          <span>Prikaži profakture od:</span>
          <input
            style={{
              marginLeft: "5px",
            }}
            type="datetime-local"
            name="from"
            value={fromDate}
            onChange={handleFromDateChange}
          />
          <span>do:</span>
          <input
            style={{
              marginLeft: "5px",
            }}
            type="datetime-local"
            name="to"
            value={toDate}
            onChange={handleToDateChange}
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
                <td onClick={() => navigate(`/inovices/edit/${inovice.id}`)}>
                  {inovice.number}/{inovice.year}
                </td>
                <td onClick={() => navigate(`/inovices/edit/${inovice.id}`)}>
                  {inovice.date}
                </td>
                <td onClick={() => navigate(`/inovices/edit/${inovice.id}`)}>
                  {inovice.clientName}
                </td>
                <td onClick={() => navigate(`/inovices/edit/${inovice.id}`)}>
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

export default ProformaInvoiceReportsForPeriod;
