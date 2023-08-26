import React, { useContext, useEffect, useState } from "react";
import TableReports from "../../ui/TableReports";
import { getReportsPerVehicle } from "../../api/api";
import { IReports, ListItem } from "../../api/interfaces";

import "../../ui/tablestyle.css";
import "./reports.css";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { VehiclesContext } from "../../context/VehiclesContext";
import { usePagination } from "../../context/PaginationContext";
import Pagination from "../../ui/Pagination";

const VehicleReports = () => {
  const navigate = useNavigate();
  const { vehicles } = useContext(VehiclesContext);

  const [getVehicles, setGetVehicles] = useState<IReports[]>([]);
  const [vehicleData, setVehicleData] = useState<ListItem[]>([]);
  const [vehicleId, setVehicleId] = useState<number | null>(null);
  const [selectedVehicleName, setSelectedVehicleName] = useState<string>("");

  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(getVehicles.length / rowsPerPage);

  const displayedVehicles = getVehicles.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  useEffect(() => {
    const extractedData: ListItem[] = vehicles.map((vehicle) => ({
      id: vehicle.id,
      name: vehicle.registration,
    }));
    setVehicleData(extractedData);
  }, [vehicles]);

  const handleVehicleSelected = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedClientId = parseInt(e.target.value, 10);
    setVehicleId(selectedClientId);
  };

  const handleShowButtonClick = async () => {
    try {
      if (vehicleId !== null) {
        const vehiclesData = await getReportsPerVehicle(vehicleId);
        setGetVehicles(vehiclesData);

        const selectedVehicle = vehicleData.find(
          (vehicle) => vehicle.id === vehicleId
        );
        setSelectedVehicleName(selectedVehicle ? selectedVehicle.name : "");
      }
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div>
      <Button
        buttonStyle={{
          backgroundColor: "rgb(100,100,100)",
          marginLeft: "10px",
          marginTop: "10px",
        }}
        onClick={() => navigate("/reports")}
      >
        Nazad
      </Button>
      <h1>Izvještaji za vozilo: {selectedVehicleName}</h1>
      <div className="table-container">
        <div className="selection">
          <span>Prikaži putne naloge za vozilo:</span>
          <select onChange={handleVehicleSelected}>
            <option value="">Izaberi vozilo...</option>
            {vehicleData.map((vehicle) => (
              <option key={vehicle.id} value={vehicle.id}>
                {vehicle.name}
              </option>
            ))}
          </select>

          <span>
            <Button
              buttonStyle={{ marginBottom: "7px" }}
              onClick={handleShowButtonClick}
            >
              Prikaži
            </Button>
            <Button
              buttonStyle={{
                marginBottom: "7px",
                marginLeft: "5px",
              }}
            >
              Štampaj
            </Button>
          </span>
        </div>
        <TableReports data={displayedVehicles} />
        <Pagination totalPages={totalPages} />
      </div>
    </div>
  );
};

export default VehicleReports;
