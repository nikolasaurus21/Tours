import React, { useContext } from "react";
import Button from "../../ui/Button";
import { useLocation, useNavigate, useParams } from "react-router-dom";

import { DriversContext } from "../../context/DriversContext";

const DeleteDriver = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const { removeDriver } = useContext(DriversContext);
  const location = useLocation();
  const driverData = location.state?.driverData;
  const handleRemoveClick = () => {
    removeDriver(Number(id));
    navigate("/drivers");
  };
  return (
    <div>
      <Button
        onClick={() => navigate("/drivers")}
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
        Jeste li sigurni da želite obrisati ovog vozača?
      </h1>
      <div className="delete-client-container">
        <div className="delete-client-content">
          <div>
            <p>
              <span className="text-delete">Ime:</span> {driverData.name}
            </p>
            <p>
              <span className="text-delete">Telefon:</span> {driverData.phone}
            </p>
          </div>
          <div className="button-delete">
            <Button
              buttonStyle={{
                backgroundColor: "rgb(2,123,50)",
                marginRight: "5px",
              }}
              onClick={() => navigate(`/drivers/edit/${Number(id)}`)}
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

export default DeleteDriver;
