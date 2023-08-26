import React, { useContext } from "react";

import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { PaymentsContext } from "../../context/PaymentsContext";
import Pagination from "../../ui/Pagination";
import { usePagination } from "../../context/PaginationContext";

const Payments = () => {
  const navigate = useNavigate();
  const { payments } = useContext(PaymentsContext);
  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(payments.length / rowsPerPage);

  const displayedPayments = payments.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  return (
    <div>
      <h1>Uplate</h1>
      <Button
        buttonStyle={{ marginLeft: "30px" }}
        onClick={() => navigate("/payments/add")}
      >
        Dodaj uplatu
      </Button>
      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th>Datum i vrijeme</th>
              <th>Klijent</th>
              <th>Iznos</th>
              <th>Osnov</th>
            </tr>
          </thead>
          <tbody>
            {displayedPayments.map((p) => (
              <tr
                key={p.id}
                className="firstcolumnorrow"
                onClick={() => navigate(`/payments/edit/${p.id}`)}
              >
                <td>{p.date}</td>
                <td>{p.client}</td>
                <td>{p.amount}</td>
                <td>{p.basis}</td>
              </tr>
            ))}
          </tbody>
        </table>
        <Pagination totalPages={totalPages} />
      </div>
    </div>
  );
};

export default Payments;
