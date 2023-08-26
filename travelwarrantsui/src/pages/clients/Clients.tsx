import React, { useContext } from "react";
import "../../ui/tablestyle.css";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";

import { AiFillDelete } from "react-icons/ai";
import { ClientsContext } from "../../context/ClientsContext";
import { usePagination } from "../../context/PaginationContext";
import Pagination from "../../ui/Pagination";

const Clients = () => {
  const navigate = useNavigate();
  const { clients, getClientById } = useContext(ClientsContext);

  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(clients.length / rowsPerPage);

  const displayedClients = clients.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  const handleDeleteClick = async (id: number) => {
    const clientData = await getClientById(id);
    navigate(`/clients/delete/${id}`, { state: { clientData } });
  };
  return (
    <div>
      <h1>Klijenti</h1>
      <Button
        buttonStyle={{
          backgroundColor: "#005f40",
          marginLeft: "30px",
        }}
        onClick={() => navigate("/clients/add")}
      >
        Novi klijent
      </Button>
      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th>Naziv</th>
              <th>Adresa</th>
              <th>Mjesto</th>
              <th>Obriši</th>
            </tr>
          </thead>
          <tbody>
            {displayedClients &&
              displayedClients.map((client) => (
                <tr key={client.id}>
                  <td
                    className="firstcolumnorrow"
                    onClick={() => navigate(`/clients/edit/${client.id}`)}
                  >
                    {client.name}
                  </td>

                  <td>{client.address}</td>
                  <td>{client.place}</td>
                  <td
                    className="delete-cell"
                    onClick={() => handleDeleteClick(client.id)}
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

export default Clients;
