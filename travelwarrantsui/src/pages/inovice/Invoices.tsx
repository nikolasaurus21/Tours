import React, { useContext } from "react";
import "./paginationInovice.css";
import Button from "../../ui/Button";
import {
  AiFillDelete,
  AiFillPrinter,
  AiOutlineArrowLeft,
  AiOutlineArrowRight,
} from "react-icons/ai";
import { useNavigate } from "react-router-dom";
import { InvoicesContext } from "../../context/InvoicesContext";
import { downloadPdf, invoiceToDelete } from "../../api/api";

const Inovices = () => {
  const { invoices, currentPage, totalPages, setCurrentPage } =
    useContext(InvoicesContext);

  const navigate = useNavigate();

  const handleDeleteClick = async (id: number) => {
    const inoviceData = await invoiceToDelete(id);
    navigate(`/invoices/delete/${id}`, { state: { inoviceData } });
  };

  const handleDownloadClick = async (id: number, inoviceNumber: string) => {
    const success = await downloadPdf(id, inoviceNumber);
    if (!success) {
      console.log("Neuspešno preuzimanje PDF-a");
    }
  };

  return (
    <div>
      <h1>Fakture</h1>
      <Button
        buttonStyle={{
          backgroundColor: "#005f40",
          marginLeft: "30px",
        }}
        onClick={() => navigate("/invoices/add")}
      >
        Nova faktura
      </Button>
      <div className="table-container">
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
            {invoices.map((invoice) => (
              <tr key={invoice.id} className="firstcolumnorrow">
                <td onClick={() => navigate(`/invoices/edit/${invoice.id}`)}>
                  {invoice.number}/{invoice.year}
                </td>
                <td onClick={() => navigate(`/invoices/edit/${invoice.id}`)}>
                  {invoice.date}
                </td>
                <td onClick={() => navigate(`/invoices/edit/${invoice.id}`)}>
                  {invoice.clientName}
                </td>
                <td onClick={() => navigate(`/invoices/edit/${invoice.id}`)}>
                  {invoice.amount}
                </td>
                <td
                  className="print-cell"
                  onClick={() =>
                    handleDownloadClick(
                      invoice.id,
                      `${invoice.number}/${invoice.year}`
                    )
                  }
                >
                  <AiFillPrinter style={{ color: "whitesmoke" }} />
                </td>
                <td
                  className="delete-cell"
                  onClick={() => handleDeleteClick(invoice.id)}
                >
                  <AiFillDelete style={{ color: "whitesmoke" }} />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      {invoices.length === 0 ? null : (
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
      )}
    </div>
  );
};

export default Inovices;
