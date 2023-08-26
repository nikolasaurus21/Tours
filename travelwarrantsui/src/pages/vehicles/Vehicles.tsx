import { useContext } from "react";

import { AiFillDelete } from "react-icons/ai";
import { useNavigate } from "react-router-dom";
import Button from "../../ui/Button";
import { getVehicleById } from "../../api/api";
import { VehiclesContext } from "../../context/VehiclesContext";
import Pagination from "../../ui/Pagination";
import { usePagination } from "../../context/PaginationContext";

const Vehicles = () => {
  const { vehicles } = useContext(VehiclesContext);

  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(vehicles.length / rowsPerPage);

  const displayedVehicles = vehicles.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  const navigate = useNavigate();

  const handleDeleteClick = async (id: number) => {
    const vehicleData = await getVehicleById(id);
    navigate(`/vehicles/delete/${id}`, { state: { vehicleData: vehicleData } });
  };
  return (
    <div>
      <h1>Vozila</h1>
      <Button
        buttonStyle={{ backgroundColor: "#005f40", marginLeft: "30px" }}
        onClick={() => navigate("/vehicles/add")}
      >
        Dodaj vozilo
      </Button>
      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th>Registracija</th>
              <th>Naziv</th>
              <th>Napomena</th>
              <th>Broj mjesta</th>
              <th>Potrošnja</th>
              <th>Obriši</th>
            </tr>
          </thead>
          <tbody>
            {displayedVehicles &&
              displayedVehicles.map((vehicle: any) => (
                <tr key={vehicle.id}>
                  <td
                    className="firstcolumnorrow"
                    onClick={() => navigate(`/vehicles/edit/${vehicle.id}`)}
                  >
                    {vehicle.registration}
                  </td>
                  <td>{vehicle.name}</td>
                  <td>{vehicle.note}</td>
                  <td>{vehicle.numberOfSeats}</td>
                  <td>{vehicle.fuel}</td>
                  <td
                    className="delete-cell"
                    onClick={() => handleDeleteClick(vehicle.id)}
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

export default Vehicles;
