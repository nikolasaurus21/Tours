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
import { InovicesContext } from "../../context/InovicesContext";
import { downloadPdf, inoviceToDelete } from "../../api/api";

const Inovices = () => {
  const { inovices, currentPage, totalPages, setCurrentPage } =
    useContext(InovicesContext);

  const navigate = useNavigate();

  const handleDeleteClick = async (id: number) => {
    const inoviceData = await inoviceToDelete(id);
    navigate(`/inovices/delete/${id}`, { state: { inoviceData } });
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
        onClick={() => navigate("/inovices/add")}
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
            {inovices.map((inovice) => (
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
      {inovices.length === 0 ? null : (
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
