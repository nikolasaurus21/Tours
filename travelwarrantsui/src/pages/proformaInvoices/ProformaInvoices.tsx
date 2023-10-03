import React, { useContext } from "react";
import {
  AiFillDelete,
  AiFillPrinter,
  AiOutlineArrowLeft,
  AiOutlineArrowRight,
} from "react-icons/ai";
import { BsCheck2All, BsFillClipboardCheckFill } from "react-icons/bs";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { ProformaInovicesContext } from "../../context/ProformaInvoicesContext";
import {
  downloadPdfProformaInvoice,
  proformaInvoiceToDelete,
} from "../../api/api";

const ProformaInvoices = () => {
  const navigate = useNavigate();
  const { proformaInvoices, currentPage, totalPages, setCurrentPage } =
    useContext(ProformaInovicesContext);

  const handleDeleteClick = async (id: number) => {
    const proformaInvoiceData = await proformaInvoiceToDelete(id);
    navigate(`/proformainvoices/delete/${id}`, {
      state: { inoviceData: proformaInvoiceData },
    });
  };

  const handleDownloadClick = async (
    id: number,
    proformaInvoiceNumber: string
  ) => {
    const success = await downloadPdfProformaInvoice(id, proformaInvoiceNumber);
    if (!success) {
      console.log("Neuspešno preuzimanje PDF-a");
    }
  };

  return (
    <div>
      <h1>Profakture</h1>
      <Button
        buttonStyle={{
          backgroundColor: "#005f40",
          marginLeft: "30px",
        }}
        onClick={() => navigate("/proformainvoices/add")}
      >
        Nova profaktura
      </Button>
      <div className="table-container">
        <table>
          <thead>
            <tr>
              <th>
                <BsFillClipboardCheckFill />
              </th>
              <th>Broj</th>
              <th>Datum</th>
              <th>Klijent</th>
              <th>Iznos</th>
              <th>Štampaj</th>
              <th>Obriši</th>
            </tr>
          </thead>
          <tbody>
            {proformaInvoices.map((proinvoice) => (
              <tr key={proinvoice.id} className="firstcolumnorrow">
                <td>
                  {proinvoice.accepted ? (
                    <BsCheck2All style={{ color: "#005f40" }} />
                  ) : (
                    ""
                  )}
                </td>
                <td
                  onClick={() =>
                    navigate(`/proformainvoices/edit/${proinvoice.id}`)
                  }
                >
                  {proinvoice.number}
                </td>
                <td
                  onClick={() =>
                    navigate(`/proformainvoices/edit/${proinvoice.id}`)
                  }
                >
                  {proinvoice.date}
                </td>
                <td
                  onClick={() =>
                    navigate(`/proformainvoices/edit/${proinvoice.id}`)
                  }
                >
                  {proinvoice.clientName}
                </td>
                <td
                  onClick={() =>
                    navigate(`/proformainvoices/edit/${proinvoice.id}`)
                  }
                >
                  {proinvoice.amount}
                </td>
                <td
                  className="print-cell"
                  onClick={() =>
                    handleDownloadClick(proinvoice.id, proinvoice.number)
                  }
                >
                  <AiFillPrinter style={{ color: "whitesmoke" }} />
                </td>
                <td
                  className="delete-cell"
                  onClick={() => handleDeleteClick(proinvoice.id)}
                >
                  <AiFillDelete style={{ color: "whitesmoke" }} />
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      {proformaInvoices.length === 0 ? null : (
        <div className="paginationInoivice">
          <button
            className={`page-button ${currentPage === 1 ? "disabled" : ""}`}
            onClick={() => setCurrentPage((prev) => Math.max(prev - 1, 1))}
            disabled={currentPage === 1}
          >
            <AiOutlineArrowLeft />
          </button>
          <span className="page-info">
            {currentPage} / {totalPages}
          </span>
          <button
            className={`page-button ${
              currentPage === totalPages ? "disabled" : ""
            }`}
            onClick={() =>
              setCurrentPage((prev) => Math.min(prev + 1, totalPages))
            }
            disabled={currentPage === totalPages}
          >
            <AiOutlineArrowRight />
          </button>
        </div>
      )}
    </div>
  );
};

export default ProformaInvoices;
