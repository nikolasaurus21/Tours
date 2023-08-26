import React, { useState } from "react";
import Button from "../../ui/Button";
import TableReports from "../../ui/TableReports";
import { useNavigate } from "react-router-dom";
import { getReportsPerPeriod } from "../../api/api";
import { IReports } from "../../api/interfaces";
import { format } from "date-fns";
import Pagination from "../../ui/Pagination";
import { usePagination } from "../../context/PaginationContext";

const PeriodReports = () => {
  const navigate = useNavigate();
  const [fromDate, setFromDate] = useState<string>("");
  const [toDate, setToDate] = useState<string>("");
  const [reports, setReports] = useState<IReports[]>([]);
  const [enteredFrom, setEnteredFrom] = useState<string>("");
  const [enteredTo, setEnteredTo] = useState<string>("");

  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(reports.length / rowsPerPage);

  const displayedPeriod = reports.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  const handleFromDateChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setFromDate(e.target.value);
  };

  const handleToDateChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    setToDate(e.target.value);
  };

  const handleShowButtonClick = async () => {
    try {
      const from = new Date(fromDate);
      const to = new Date(toDate);
      const reportsData = await getReportsPerPeriod(from, to);
      setReports(reportsData);
      setEnteredFrom(format(new Date(from), "dd/MM/yyyy HH:mm "));
      setEnteredTo(format(new Date(to), "dd/MM/yyyy HH:mm "));
    } catch (error) {
      console.error("Error fetching reports:", error);
      setReports([]);
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
      <h1>
        Putni nalozi za period od:
        <span
          style={{
            color: "black",
            marginLeft: "3px",
            marginRight: "3px",
          }}
        >
          {enteredFrom}
        </span>
        do:
        <span style={{ color: "black", marginLeft: "3px" }}>{enteredTo}</span>
      </h1>
      <div className="table-container">
        <div className="selection">
          <span>Prikaži putne naloge od:</span>
          <input
            style={{
              marginLeft: "5px",
            }}
            type="datetime-local"
            name="from"
            value={fromDate}
            onChange={handleFromDateChange}
          />
          <span>do:</span>
          <input
            style={{
              marginLeft: "5px",
            }}
            type="datetime-local"
            name="to"
            value={toDate}
            onChange={handleToDateChange}
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
        <TableReports data={displayedPeriod} />
        <Pagination totalPages={totalPages} />
      </div>
    </div>
  );
};

export default PeriodReports;
