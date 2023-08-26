import React, { useContext } from "react";
import Button from "../../ui/Button";
import { useLocation, useNavigate, useParams } from "react-router-dom";
import { TravelWarrantsContext } from "../../context/ToursContext";

const DeleteTour = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const { removeWarrant } = useContext(TravelWarrantsContext);
  const location = useLocation();
  const tourData = location.state?.tourData;

  const handleRemoveClick = () => {
    removeWarrant(Number(id));
    navigate("/");
  };
  return (
    <div>
      <Button
        onClick={() => navigate("/")}
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
        Jeste li sigurni da želite obrisati ovaj nalog?
      </h1>
      <div className="delete-client-container">
        <div className="delete-client-content">
          <div>
            <p>
              <span className="text-delete">Datum i vrijeme:</span>{" "}
              {tourData.datetime}
            </p>
            <p>
              <span className="text-delete">Klijent:</span> {tourData.client}
            </p>
            <p>
              {" "}
              <span className="text-delete">Polazak:</span> {tourData.departure}
            </p>
            <p>
              <span className="text-delete">Destinacija:</span>{" "}
              {tourData.destination}
            </p>
            <p>
              <span className="text-delete">Registracija:</span>{" "}
              {tourData.registration}
            </p>
            <p>
              <span className="text-delete">Vozač:</span> {tourData.driver}
            </p>
            <p>
              <span className="text-delete">
                Kilometraža vozila ce biti smanjena za:
              </span>{" "}
              {tourData.mileage}
            </p>
          </div>
          <div className="button-delete">
            <Button
              buttonStyle={{
                backgroundColor: "rgb(2,123,50)",
                marginRight: "5px",
              }}
              onClick={() => navigate(`/travelwarrants/edit/${Number(id)}`)}
            >
              Izmijeni
            </Button>
            <Button
              buttonStyle={{ backgroundColor: "rgb(165,0,0)" }}
              onClick={handleRemoveClick}
            >
              Obriši
            </Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DeleteTour;
