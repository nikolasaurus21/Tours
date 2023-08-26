import React from "react";
import { AiFillDelete } from "react-icons/ai";
import { IReports } from "../api/interfaces";

interface TableReportsProps {
  data?: IReports[];
  onDelete?: (index: number) => void;
  itemsPerPage?: number;
}

const TableReports = ({ data, onDelete }: TableReportsProps) => {
  return (
    <div>
      <table>
        <thead>
          <tr>
            <th>Datum i vrijeme</th>
            <th>Klijent</th>
            <th>Ruta</th>
            <th>Kilometraža</th>
            <th>Obriši</th>
          </tr>
        </thead>
        <tbody>
          {data?.map((row, index) => (
            <tr key={index} className="firstcolumnorrow">
              <td>{row.date}</td>
              <td>{row.client}</td>
              <td>
                {row.departure &&
                row.intermediateDestinations &&
                row.destination ? (
                  `${row.departure} - ${row.intermediateDestinations} - ${row.destination}`
                ) : (
                  <>
                    {row.departure && `${row.departure} - `}
                    {row.intermediateDestinations &&
                      `${row.intermediateDestinations} - `}
                    {row.destination}
                  </>
                )}
              </td>
              <td>{row.kilometers}</td>
              <td className="delete-cell" style={{ paddingLeft: "35px" }}>
                <AiFillDelete />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default TableReports;
