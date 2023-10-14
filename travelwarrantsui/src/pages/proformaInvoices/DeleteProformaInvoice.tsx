import React, { useContext } from "react";
import Button from "../../ui/Button";
import { useNavigate, useParams, useLocation } from "react-router-dom";

import { ProformaInovicesContext } from "../../context/ProformaInvoicesContext";

const DeleteProformaInvoice = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const { removeProformaInvoice } = useContext(ProformaInovicesContext);
  const location = useLocation();
  const proformaInvoiceData = location.state?.proformaInvoiceData;

  const handleRemoveClick = () => {
    removeProformaInvoice(Number(id));
    navigate("/proformainvoices");
  };
  return (
    <div>
      <Button
        onClick={() => navigate("/proformainvoices")}
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
        Jeste li sigurni da želite obrisati ovu profakturu?
      </h1>
      <div className="delete-client-container">
        <div className="delete-client-content">
          <div>
            <p>
              <span className="text-delete">Broj:</span>{" "}
              {proformaInvoiceData.number}
            </p>
            <p>
              <span className="text-delete">Klijent:</span>{" "}
              {proformaInvoiceData.clientName}
            </p>
            <p>
              {" "}
              <span className="text-delete">Datum:</span>{" "}
              {proformaInvoiceData.date}
            </p>
            <p>
              <span className="text-delete">Amount:</span>{" "}
              {proformaInvoiceData.amount}
            </p>
          </div>
          <div className="button-delete">
            <Button
              buttonStyle={{
                backgroundColor: "rgb(2,123,50)",
                marginRight: "5px",
              }}
              onClick={() => navigate(`/proformainvoices/edit/${Number(id)}`)}
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

export default DeleteProformaInvoice;
