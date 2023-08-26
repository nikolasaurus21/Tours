import React, { useContext } from "react";

import { useLocation, useNavigate, useParams } from "react-router-dom";
import Button from "../../ui/Button";
import "../../ui/delete.css";
import { ClientsContext } from "../../context/ClientsContext";

const DeleteClient = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const { removeClient } = useContext(ClientsContext);
  const location = useLocation();
  const clientData = location.state?.clientData;

  const handleRemoveClick = () => {
    removeClient(Number(id));
    navigate("/clients");
  };

  return (
    <div>
      <Button
        onClick={() => navigate("/clients")}
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
        Jeste li sigurni da želite obrisati ovog klijenta?
      </h1>
      <div className="delete-client-container">
        <div className="delete-client-content">
          <div>
            <p>
              <span className="text-delete">Ime:</span> {clientData.name}
            </p>
            <p>
              <span className="text-delete">Adresa:</span> {clientData.address}
            </p>
            <p>
              {" "}
              <span className="text-delete">Mjesto:</span> {clientData.place}
            </p>
            <p>
              <span className="text-delete">PIB/JMBG:</span> {clientData.tinurn}
            </p>
            <p>
              <span className="text-delete">PDV:</span> {clientData.vat}
            </p>
            <p>
              <span className="text-delete">Telefon:</span> {clientData.phone}
            </p>
            <p>
              <span className="text-delete">Fax:</span> {clientData.fax}
            </p>
            <p>
              <span className="text-delete">Email:</span> {clientData.email}
            </p>
            <p>
              <span className="text-delete">Ekskurzija:</span>{" "}
              <input type="checkbox" checked={clientData.excursion} disabled />
            </p>
          </div>
          <div className="button-delete">
            <Button
              buttonStyle={{
                backgroundColor: "rgb(2,123,50)",
                marginRight: "5px",
              }}
              onClick={() => navigate(`/clients/edit/${Number(id)}`)}
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

export default DeleteClient;
