import React, { useContext } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../../ui/Button";
import { getDriversById } from "../../api/api";
import { AiFillDelete } from "react-icons/ai";
import { DriversContext } from "../../context/DriversContext";
import Pagination from "../../ui/Pagination";
import { usePagination } from "../../context/PaginationContext";

const Drivers = () => {
  const navigate = useNavigate();
  const { drivers } = useContext(DriversContext);

  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(drivers.length / rowsPerPage);

  const displayedDrivers = drivers.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  const handleDeleteClick = async (id: number) => {
    const driverData = await getDriversById(id);
    navigate(`/drivers/delete/${id}`, { state: { driverData: driverData } });
  };
  return (
    <div>
      <h1>Vozači</h1>
      <Button
        buttonStyle={{ backgroundColor: "#005f40", marginLeft: "30px" }}
        onClick={() => navigate("/drivers/add")}
      >
        Dodaj vozača
      </Button>
      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th>Ime</th>
              <th>Obriši</th>
            </tr>
          </thead>
          <tbody>
            {displayedDrivers &&
              displayedDrivers.map((driver) => (
                <tr key={driver.id}>
                  <td
                    className="firstcolumnorrow"
                    onClick={() => navigate(`/drivers/edit/${driver.id}`)}
                  >
                    {driver.name}
                  </td>
                  <td
                    className="delete-cell"
                    onClick={() => handleDeleteClick(driver.id)}
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

export default Drivers;
