import React, { useContext } from "react";
import Button from "../../ui/Button";
import { useNavigate, useParams, useLocation } from "react-router-dom";
import { GiroAccountsContext } from "../../context/ZiroRacuniContext";

const DeleteGiroAccount = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const { removeGiroAcc } = useContext(GiroAccountsContext);
  const location = useLocation();
  const giroData = location.state?.giroData;
  const handleRemoveClick = () => {
    removeGiroAcc(Number(id));
    navigate("/giroaccounts");
  };
  return (
    <div>
      <Button
        onClick={() => navigate("/giroaccounts")}
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
        Jeste li sigurni da želite obrisati ovaj žiro-račun?
      </h1>
      <div className="delete-client-container">
        <div className="delete-client-content">
          <div>
            <p>
              <span className="text-delete">Banka:</span> {giroData.bank}
            </p>
            <p>
              <span className="text-delete">Račun:</span> {giroData.account}
            </p>
          </div>
          <div className="button-delete">
            <Button
              buttonStyle={{
                backgroundColor: "rgb(2,123,50)",
                marginRight: "5px",
              }}
              onClick={() => navigate(`/giroaccounts/edit/${Number(id)}`)}
            >
              Izmijeni
            </Button>
            <Button
              onClick={handleRemoveClick}
              buttonStyle={{ backgroundColor: "rgb(165,0,0)" }}
            >
              Obriši
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DeleteGiroAccount;
