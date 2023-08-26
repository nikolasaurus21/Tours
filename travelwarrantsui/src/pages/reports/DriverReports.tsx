import React, { useContext, useEffect, useState } from "react";
import TableReports from "../../ui/TableReports";
import { getReportsPerDriver } from "../../api/api";
import { IReports, ListItem } from "../../api/interfaces";
import "../../ui/tablestyle.css";
import "./reports.css";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { DriversContext } from "../../context/DriversContext";
import Pagination from "../../ui/Pagination";
import { usePagination } from "../../context/PaginationContext";

const DriverReports = () => {
  const navigate = useNavigate();
  const { drivers } = useContext(DriversContext);

  const [getDrivers, setGetDrivers] = useState<IReports[]>([]);
  const [driverData, setDriverData] = useState<ListItem[]>([]);
  const [driverId, setDriverId] = useState<number | null>(null);
  const [selectedDriverName, setSelectedDriverName] = useState<string>("");

  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(getDrivers.length / rowsPerPage);

  const displayedDrivers = getDrivers.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  useEffect(() => {
    const extractedData: ListItem[] = drivers.map((driver) => ({
      id: driver.id,
      name: driver.name,
    }));
    setDriverData(extractedData);
  }, [drivers]);

  const handleVehicleSelected = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const selectedDriverId = parseInt(e.target.value, 10);
    setDriverId(selectedDriverId);
  };

  const handleShowButtonClick = async () => {
    try {
      if (driverId !== null) {
        const driversData = await getReportsPerDriver(driverId);
        setGetDrivers(driversData);

        const selectedDriver = driverData.find(
          (driver) => driver.id === driverId
        );
        setSelectedDriverName(selectedDriver ? selectedDriver.name : "");
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
      <h1>Izvještaji za vozača: {selectedDriverName}</h1>
      <div className="table-container">
        <div className="selection">
          <span>Prikaži putne naloge za vozača:</span>
          <select onChange={handleVehicleSelected}>
            <option value="">Izaberi vozača...</option>
            {driverData.map((vehicle) => (
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
        <TableReports data={displayedDrivers} />

        <Pagination totalPages={totalPages} />
      </div>
    </div>
  );
};

export default DriverReports;
