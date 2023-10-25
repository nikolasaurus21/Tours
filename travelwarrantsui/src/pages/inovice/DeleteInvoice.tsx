import React, { useContext } from "react";
import { InvoicesContext } from "../../context/InvoicesContext";
import { useNavigate, useParams, useLocation } from "react-router-dom";
import Button from "../../ui/Button";

const DeleteInovice = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const { removeInvoice: removeInovice } = useContext(InvoicesContext);
  const location = useLocation();
  const inoviceData = location.state?.inoviceData;

  const handleRemoveClick = () => {
    removeInovice(Number(id));
    navigate("/invoices");
  };
  return (
    <div>
      <Button
        onClick={() => navigate("/invoices")}
        buttonStyle={{
          backgroundColor: "rgb(100,100,100)",
          marginLeft: "10px",
          marginTop: "5px",
        }}
      >
        Nazad
      </Button>
      <h1
        style={{
          marginBottom: "0px",
          marginRight: "280px",
        }}
      >
        Jeste li sigurni da želite obrisati ovu fakturu?
      </h1>
      <div className="delete-client-container">
        <div className="delete-client-content">
          <div>
            <p>
              <span className="text-delete">Broj:</span> {inoviceData.number}
            </p>
            <p>
              <span className="text-delete">Klijent:</span>{" "}
              {inoviceData.clientName}
            </p>
            <p>
              {" "}
              <span className="text-delete">Datum:</span> {inoviceData.date}
            </p>
            <p>
              <span className="text-delete">Amount:</span> {inoviceData.amount}
            </p>
          </div>
          <div className="button-delete">
            <Button
              buttonStyle={{
                backgroundColor: "rgb(2,123,50)",
                marginRight: "5px",
              }}
              onClick={() => navigate(`/invoices/edit/${Number(id)}`)}
            >
              Izmijeni
            </Button>
            <Button
              onClick={handleRemoveClick}
              buttonStyle={{ backgroundColor: "rgb(160,0,0)" }}
            >
              Obriši
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DeleteInovice;
