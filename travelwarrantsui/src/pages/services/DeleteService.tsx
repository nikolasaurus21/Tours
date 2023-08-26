import React, { useContext } from "react";
import Button from "../../ui/Button";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { ServicesContext } from "../../context/ServicesContext";

const DeleteService = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const { removeService } = useContext(ServicesContext);
  const location = useLocation();
  const serviceData = location.state?.serviceData;
  const handleRemoveClick = () => {
    removeService(Number(id));
    navigate("/services");
  };
  return (
    <div>
      <Button
        onClick={() => navigate("/services")}
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
        Jeste li sigurni da želite obrisati ovu uslugu?
      </h1>
      <div className="delete-client-container">
        <div className="delete-client-content">
          <div>
            <p>
              <span className="text-delete">Naziv:</span> {serviceData.name}
            </p>
            <p>
              <span className="text-delete">PDV:</span> {serviceData.vat}
            </p>
          </div>
          <div className="button-delete">
            <Button
              buttonStyle={{
                backgroundColor: "rgb(2,123,50)",
                marginRight: "5px",
              }}
              onClick={() => navigate(`/services/edit/${Number(id)}`)}
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

export default DeleteService;
