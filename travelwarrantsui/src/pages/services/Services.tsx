import React, { useContext } from "react";

import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { getServicesById } from "../../api/api";
import { AiFillDelete } from "react-icons/ai";
import { ServicesContext } from "../../context/ServicesContext";
import Pagination from "../../ui/Pagination";
import { usePagination } from "../../context/PaginationContext";

const Services = () => {
  const { services } = useContext(ServicesContext);
  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(services.length / rowsPerPage);

  const displayedServices = services.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );
  const navigate = useNavigate();

  const handleDeleteClick = async (id: number) => {
    const serviceData = await getServicesById(id);
    navigate(`/services/delete/${id}`, { state: { serviceData: serviceData } });
  };
  return (
    <div>
      <h1>Usluge</h1>
      <Button
        buttonStyle={{
          backgroundColor: "#005f40",
          marginLeft: "30px",
        }}
        onClick={() => navigate("/services/add")}
      >
        Dodaj uslugu
      </Button>
      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th>Naziv</th>
              <th>PDV</th>
              <th>Obriši</th>
            </tr>
          </thead>
          <tbody>
            {displayedServices &&
              displayedServices.map((service) => (
                <tr key={service.id}>
                  <td
                    className="firstcolumnorrow"
                    onClick={() => navigate(`/services/edit/${service.id}`)}
                  >
                    {service.description}
                  </td>
                  <td>{service.vat}</td>
                  <td
                    className="delete-cell"
                    onClick={() => handleDeleteClick(service.id)}
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

export default Services;
