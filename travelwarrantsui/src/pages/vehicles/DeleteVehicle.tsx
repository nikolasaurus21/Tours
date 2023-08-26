import { useContext } from "react";
import { useNavigate, useParams, useLocation } from "react-router-dom";

import Button from "../../ui/Button";
import { VehiclesContext } from "../../context/VehiclesContext";

const DeleteVehicle = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const { removeVehicle } = useContext(VehiclesContext);
  const location = useLocation();
  const vehicleData = location.state?.vehicleData;
  const handleRemoveClick = () => {
    removeVehicle(Number(id));
    navigate("/vehicles");
  };
  return (
    <div>
      <Button
        onClick={() => navigate("/vehicles")}
        buttonStyle={{
          backgroundColor: "rgb(100,100,100)",
          marginLeft: "10px",
          marginTop: "5px",
        }}
      >
        Back
      </Button>
      <h1
        style={{
          marginBottom: "0px",
          marginRight: "280px",
        }}
      >
        Jeste li sigurni da želite obrisati ovo vozilo?
      </h1>
      <div className="delete-client-container">
        <div className="delete-client-content">
          <div>
            <p>
              <span className="text-delete">Naziv:</span> {vehicleData.name}
            </p>
            <p>
              <span className="text-delete">Registracija:</span>{" "}
              {vehicleData.registration}
            </p>
            <p>
              {" "}
              <span className="text-delete">Potrošnja:</span> {vehicleData.fuel}
            </p>
            <p>
              <span className="text-delete">Kilometraža:</span>{" "}
              {vehicleData.mileage}
            </p>
            <p>
              <span className="text-delete">Napomena:</span> {vehicleData.note}
            </p>
            <p>
              <span className="text-delete">Broj mjesta:</span>{" "}
              {vehicleData.numberOfSeats}
            </p>
          </div>
          <div className="button-delete">
            <Button
              buttonStyle={{
                backgroundColor: "rgb(2,123,50)",
                marginRight: "5px",
              }}
              onClick={() => navigate(`/vehicles/edit/${Number(id)}`)}
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

export default DeleteVehicle;
