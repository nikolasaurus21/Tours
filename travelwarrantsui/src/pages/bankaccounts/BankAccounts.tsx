import React, { useContext } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../../ui/Button";
import { getBankAccountById } from "../../api/api";
import { AiFillDelete } from "react-icons/ai";
import { BankAccountsContext } from "../../context/BankAccountsContext";
import { usePagination } from "../../context/PaginationContext";
import Pagination from "../../ui/Pagination";

const GiroAccounts = () => {
  const { bankAccounts: giroAccounts } = useContext(BankAccountsContext);

  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(giroAccounts.length / rowsPerPage);

  const displayedAcc = giroAccounts.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  const navigate = useNavigate();

  const handleDeleteClick = async (id: number) => {
    const giroData = await getBankAccountById(id);
    navigate(`/bankaccounts/delete/${id}`, { state: { giroData: giroData } });
  };
  return (
    <div>
      <h1>Žiro-računi</h1>
      <Button
        buttonStyle={{ marginLeft: "30px" }}
        onClick={() => navigate("/bankaccounts/add")}
      >
        Dodaj žiro-račun
      </Button>
      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th>Račun</th>
              <th>Banka</th>
              <th>Obriši</th>
            </tr>
          </thead>
          <tbody>
            {displayedAcc &&
              displayedAcc.map((ga) => (
                <tr key={ga.id}>
                  <td
                    className="firstcolumnorrow"
                    onClick={() => navigate(`/bankaccounts/edit/${ga.id}`)}
                  >
                    {ga.bank}
                  </td>
                  <td>{ga.account}</td>
                  <td
                    className="delete-cell"
                    onClick={() => handleDeleteClick(ga.id)}
                  >
                    <AiFillDelete style={{ color: "whitesmoke" }} />
                  </td>
                </tr>
              ))}
          </tbody>
        </table>
        <Pagination totalPages={totalPages} />
      </div>
    </div>
  );
};

export default GiroAccounts;
