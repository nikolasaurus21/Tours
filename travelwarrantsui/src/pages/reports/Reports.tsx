import { useNavigate } from "react-router-dom";
import { excursion } from "../../api/api";
import { useState, useEffect } from "react";

const Reports = () => {
  const navigate = useNavigate();

  const [isChecked, setIsChecked] = useState(false);

  useEffect(() => {
    const savedCheckState = localStorage.getItem("isChecked");
    if (savedCheckState) {
      setIsChecked(JSON.parse(savedCheckState));
    }
  }, []);

  const handleCheckboxChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const checked = e.target.checked;
    excursion(checked);
    setIsChecked(checked);

    localStorage.setItem("isChecked", JSON.stringify(checked));
  };

  return (
    <div>
      <h1>Izvještaji</h1>
      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th>Putni nalozi</th>
              <th>Fakture</th>
              <th>Profakture</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td
                className="firstcolumnorrow"
                onClick={() => navigate("/reports/traverwarrantsperclient")}
              >
                Putni nalozi po klijentu
              </td>
              <td
                className="firstcolumnorrow"
                onClick={() => navigate("/reports/inovicesforclient")}
              >
                Fakture po klijentu
              </td>
              <td className="firstcolumnorrow">Profakture po klijentu</td>
            </tr>
            <tr>
              <td
                className="firstcolumnorrow"
                onClick={() => navigate("/reports/traverwarrantsperiodreports")}
              >
                Putni nalozi za period
              </td>
              <td
                className="firstcolumnorrow"
                onClick={() => navigate("/reports/inovicesforperiod")}
              >
                Fakture za period
              </td>
              <td className="firstcolumnorrow">Profakture za period</td>
            </tr>
            <tr>
              <td
                className="firstcolumnorrow"
                onClick={() => navigate("/reports/traverwarrantstodestination")}
              >
                Putni nalozi po destinaciji
              </td>

              <td
                className="firstcolumnorrow"
                onClick={() => navigate("/reports/inovicesbydescription")}
              >
                Fakture po opisu usluge
              </td>
              <td className="firstcolumnorrow">Profakture po opisu usluge</td>
            </tr>

            <tr>
              <td
                className="firstcolumnorrow"
                onClick={() =>
                  navigate("/reports/traverwarrantsdep-desreports")
                }
              >
                Potni nalozi po polasku i destinaciji
              </td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td
                className="firstcolumnorrow"
                onClick={() =>
                  navigate("/reports/traverwarrantsvehiclereports")
                }
              >
                Putni nalozi po vozilu
              </td>
              <td></td>
              <td></td>
            </tr>
            <tr>
              <td
                className="firstcolumnorrow"
                onClick={() => navigate("/reports/traverwarrantsdriverreports")}
              >
                Putni nalozi po vozaču
              </td>
              <td></td>
              <td></td>
            </tr>
          </tbody>
        </table>

        <div className="ekskurzija">
          <input
            type="checkbox"
            onChange={handleCheckboxChange}
            checked={isChecked}
          />{" "}
          <span>Uključi ekskurzije u izvještaje</span>
        </div>
      </div>
    </div>
  );
};

export default Reports;
