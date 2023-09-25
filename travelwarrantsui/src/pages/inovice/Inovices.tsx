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

const Inovices = () => {
  const { inovices, currentPage, totalPages, setCurrentPage } =
    useContext(InovicesContext);

  const navigate = useNavigate();
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
            {inovices.map((inovice, index) => (
              <tr key={index}>
                <td>
                  {inovice.number}/{inovice.year}
                </td>
                <td>{inovice.date}</td>
                <td>{inovice.clientName}</td>
                <td>{inovice.amount}</td>
                <td className="print-cell">
                  <AiFillPrinter style={{ color: "whitesmoke" }} />
                </td>
                <td
                  className="delete-cell"
                  //onClick={() => handleDeleteClick(inovice.id)}
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

export default Inovices;
