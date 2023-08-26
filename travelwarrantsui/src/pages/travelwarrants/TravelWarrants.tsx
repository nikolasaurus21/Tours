import { useContext } from "react";
import { TravelWarrantsContext } from "../../context/ToursContext";
import { AiFillDelete } from "react-icons/ai";
import { useNavigate } from "react-router-dom";
import Button from "../../ui/Button";
import { getTravelWarrantsDelete } from "../../api/api";
import Pagination from "../../ui/Pagination";
import { usePagination } from "../../context/PaginationContext";

const TravelWarrants = () => {
  const { travelwarrants } = useContext(TravelWarrantsContext);
  const navigate = useNavigate();

  const { currentPage, rowsPerPage } = usePagination();
  const totalPages = Math.ceil(travelwarrants.length / rowsPerPage);

  const displayedWarrants = travelwarrants.slice(
    (currentPage - 1) * rowsPerPage,
    currentPage * rowsPerPage
  );

  const handleDeleteClick = async (id: number) => {
    try {
      const tourData = await getTravelWarrantsDelete(id);

      navigate(`/travelwarrants/delete/${id}`, {
        state: { tourData: tourData },
      });
    } catch (error) {
      console.error("Error while fetching tour data:", error);
    }
  };

  return (
    <div>
      <h1>Putni nalozi</h1>
      <Button
        buttonStyle={{
          backgroundColor: "#005f40",
          marginLeft: "30px",
        }}
        onClick={() => navigate("/travelwarrants/add")}
      >
        Dodaj putni nalog
      </Button>
      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th></th>
              <th>Datum i vrijeme</th>
              <th>Klijent</th>
              <th>Ruta</th>
              <th>Kilometraža</th>
              <th>Obriši</th>
            </tr>
          </thead>
          <tbody>
            {displayedWarrants.map((x: any) => (
              <tr key={x.id} className="firstcolumnorrow">
                <td>
                  <input type="checkbox" name="" id="" />
                </td>
                <td onClick={() => navigate(`/travelwarrants/edit/${x.id}`)}>
                  {x.date}
                </td>
                <td onClick={() => navigate(`/travelwarrants/edit/${x.id}`)}>
                  {x.client}
                </td>
                <td onClick={() => navigate(`/travelwarrants/edit/${x.id}`)}>
                  {x.departure && x.interdestination && x.destination ? (
                    `${x.departure} - ${x.interdestination} - ${x.destination}`
                  ) : (
                    <>
                      {x.departure && `${x.departure} - `}
                      {x.interdestination && `${x.interdestination} - `}
                      {x.destination}
                    </>
                  )}
                </td>
                <td onClick={() => navigate(`/travelwarrants/edit/${x.id}`)}>
                  {x.mileage}km
                </td>
                <td
                  className="delete-cell"
                  onClick={() => handleDeleteClick(x.id)}
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

export default TravelWarrants;
