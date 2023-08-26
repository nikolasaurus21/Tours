import React, { useState } from "react";
import TableReports from "../../ui/TableReports";
import { IReports } from "../../api/interfaces";
import "../../ui/tablestyle.css";
import "./reports.css";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { getReportsForDestination } from "../../api/api";
import { usePagination } from "../../context/PaginationContext";
import Pagination from "../../ui/Pagination";

const DestinationReports = () => {
  const [destinations, setDestinations] = useState<IReports[]>([]);

  const [destination, setDestination] = useState<string>("");
  const [enteredDestination, setEnteredDestination] = useState<string>("");

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

  const handleShowButtonClick = async () => {
    if (destination.trim() === "") {
      setDestinations([]);
      setEnteredDestination("");
    } else {
      const data = await getReportsForDestination(destination);
      setDestinations(data);

      const selectedDestination = destinations.find(
        (x) => x.destination === destination
      );
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
        Nazad
      </Button>
      <h1>Putni nalozi do destinacije: {enteredDestination}</h1>
      <div className="table-container">
        <div className="selection">
          <span>Prikaži putne naloge do destinacije:</span>
          <input
            type="text"
            name="basis"
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

export default DestinationReports;
