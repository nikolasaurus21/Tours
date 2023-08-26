import React, { useContext } from "react";

import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { SearchesContext } from "../../context/SearchesContext";
import { usePagination } from "../../context/PaginationContext";
import Pagination from "../../ui/Pagination";

const Searches = () => {
  const navigate = useNavigate();
  const { searches } = useContext(SearchesContext);

  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(searches.length / rowsPerPage);

  const displayedSearches = searches.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  return (
    <div>
      <h1>Potraživanja</h1>
      <Button
        buttonStyle={{ marginLeft: "30px" }}
        onClick={() => navigate("/searches/add")}
      >
        Dodaj potraživanje
      </Button>
      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th>Datum</th>
              <th>Klijent</th>
              <th>Iznos</th>
              <th>Osnov</th>
            </tr>
          </thead>
          <tbody>
            {displayedSearches &&
              displayedSearches.map((s) => (
                <tr
                  key={s.id}
                  className="firstcolumnorrow"
                  onClick={() => navigate(`/searches/edit/${s.id}`)}
                >
                  <td>{s.date}</td>
                  <td>{s.client}</td>
                  <td>{s.amount}</td>
                  <td>{s.basis}</td>
                </tr>
              ))}
          </tbody>
        </table>
        <Pagination totalPages={totalPages} />
      </div>
    </div>
  );
};

export default Searches;
