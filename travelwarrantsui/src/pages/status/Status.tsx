import React, { useContext } from "react";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import "../../ui/tablestyle.css";
import { StatusesContext } from "../../context/StatusesContext";
import Pagination from "../../ui/Pagination";
import { usePagination } from "../../context/PaginationContext";

const Status = () => {
  const navigate = useNavigate();
  const { statuses } = useContext(StatusesContext);

  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(statuses.length / rowsPerPage);

  const displayedStatuses = statuses.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  return (
    <div>
      <div>
        <Button
          buttonStyle={{
            backgroundColor: "rgb(100,100,100)",
            marginTop: "10px",
            marginLeft: "10px",
          }}
          onClick={() => navigate("/")}
        >
          Nazad
        </Button>
        <h1>Stanje finansija</h1>
        <div className="table-container">
          <table>
            <thead>
              <tr>
                <th>Klijent</th>
                <th>Potraživanje</th>
                <th>Uplata</th>
                <th>Saldo</th>
              </tr>
            </thead>
            <tbody>
              {displayedStatuses &&
                displayedStatuses.map((status: any) => (
                  <tr key={status.id} className="firstcolumnorrow">
                    <td
                      onClick={() =>
                        navigate(`/clients/edit/${status.clientId}`)
                      }
                    >
                      {status.client}
                    </td>
                    <td
                      onClick={() => navigate("/searches")}
                      className="hover-red"
                    >
                      {status.search}
                    </td>
                    <td
                      onClick={() => navigate("/payments")}
                      className="hover-green"
                    >
                      {status.deposit}
                    </td>
                    <td>{status.balance}</td>
                  </tr>
                ))}
            </tbody>
          </table>
          <Pagination totalPages={totalPages} />
        </div>
      </div>
    </div>
  );
};

export default Status;
