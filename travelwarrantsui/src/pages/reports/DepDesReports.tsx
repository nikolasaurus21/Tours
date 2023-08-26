import React, { useState } from "react";
import TableReports from "../../ui/TableReports";

import { IReports } from "../../api/interfaces";

import "../../ui/tablestyle.css";
import "./reports.css";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { getReportsDepDest } from "../../api/api";
import { usePagination } from "../../context/PaginationContext";
import Pagination from "../../ui/Pagination";

const DepDesReports = () => {
  const [destinations, setDestinations] = useState<IReports[]>([]);
  const [destination, setDestination] = useState<string>("");
  const [departure, setDeparture] = useState<string>("");
  const [enteredDestination, setEnteredDestination] = useState<string>("");
  const [enteredDeparture, setEnteredDeparture] = useState<string>("");

  const navigate = useNavigate();
  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(destinations.length / rowsPerPage);

  const displayedDestinations = destinations.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  const handleDestinationChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setDestination(event.target.value.toLowerCase());
  };

  const handleDepartureChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setDeparture(event.target.value.toLowerCase());
  };

  const handleShowButtonClick = async () => {
    if (destination.trim() === "" && departure.trim() === "") {
      setDestinations([]);
      setEnteredDestination("");
      setEnteredDeparture("");
    } else {
      const data = await getReportsDepDest(departure, destination);
      setDestinations(data);

      const selectedDeparture = destinations.find(
        (x) => x.departure === departure
      );
      const selectedDestination = destinations.find(
        (x) => x.destination === destination
      );

      setEnteredDeparture(selectedDeparture ? selectedDeparture.departure : "");

      setEnteredDestination(
        selectedDestination ? selectedDestination.destination : ""
      );
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
        Back
      </Button>
      <h1>
        Travel warrants from {enteredDeparture} to {enteredDestination}
      </h1>
      <div className="table-container">
        <div className="selection">
          <span>Prikaži ture od polaska:</span>
          <input
            type="text"
            name="departure"
            value={departure}
            onChange={handleDepartureChange}
            placeholder="Write departure..."
          />
          <span>do destinacije:</span>
          <input
            type="text"
            name="destination"
            value={destination}
            onChange={handleDestinationChange}
            placeholder="Write destination..."
          />

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
        <TableReports data={displayedDestinations} />
        <Pagination totalPages={totalPages} />
      </div>
    </div>
  );
};

export default DepDesReports;
