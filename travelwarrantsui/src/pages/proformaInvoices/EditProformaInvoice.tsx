import React, { useContext, useEffect, useState } from "react";
import { HiOutlinePlusSm, HiMinus } from "react-icons/hi";
import Button from "../../ui/Button";
import { useParams, useNavigate } from "react-router-dom";
import { CiCircleRemove } from "react-icons/ci";
import {
  deleteRoutePlan,
  downloadRoutePlan,
  getProformaInvoiceById,
  uploadfileStreaming,
} from "../../api/api";
import { IItemsEdit, IEditProformaInvoice } from "../../api/interfaces";
import { ClientsContext } from "../../context/ClientsContext";

import { ServicesContext } from "../../context/ServicesContext";
import { ProformaInovicesContext } from "../../context/ProformaInvoicesContext";

const EditProformaInvoice = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { clients } = useContext(ClientsContext);
  const { services } = useContext(ServicesContext);
  const { updateProformaInvoice } = useContext(ProformaInovicesContext);

  const [fileName, setFileName] = useState<string | null>(null);
  const [fileId, setFileId] = useState<number | undefined>(undefined);
  const [items, setItems] = useState<IItemsEdit[]>([]);

  const [proformaInvoiceNumber, setProformaInvoiceNumber] = useState<
    string | null
  >(null);
  const [addProformaInvoice, setAddProformaInvoice] =
    useState<IEditProformaInvoice>({
      clientId: 0,
      date: "",
      paymentDeadline: 0,
      note: "",
      priceWithoutVat: false,
      itemsOnInovice: [],
      itemsToDelete: [],
      offerAccepted: false,
      proformaWithoutVat: false,
      file: null,
    });

  const handleFileUpload = async (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const fileToUpload = event.target.files ? event.target.files[0] : null;
    if (fileToUpload) {
      try {
        // Otpremanje fajla na server
        const fileIdFromApi = await uploadfileStreaming(fileToUpload); // uploadFile je vaša funkcija za otpremanje fajla
        setFileId(fileIdFromApi); // Postavljanje ID fajla u stanje
        setFileName(fileToUpload.name);
        setAddProformaInvoice((prevState) => ({
          ...prevState,
          file: fileIdFromApi, // Ažuriranje file polja u vašem stanju proforma fakture
        }));
      } catch (error) {
        console.error("Error uploading file:", error);
      }
    }
  };

  const handleFileDeletion = async (
    event: React.MouseEvent<HTMLButtonElement>
  ) => {
    event.preventDefault();
    setFileId(undefined);
    setFileName(null);
    setAddProformaInvoice((prevState) => ({
      ...prevState,
      file: null, // Ažuriranje file polja u vašem stanju proforma fakture
    }));
  };

  useEffect(() => {
    const fetchInovice = async () => {
      try {
        const proformaInvoice = await getProformaInvoiceById(Number(id));
        console.log(proformaInvoice);
        console.log(proformaInvoice.fileId);
        setFileId(proformaInvoice.fileId);
        setAddProformaInvoice(proformaInvoice);
        setFileName(proformaInvoice.fileName);
        if (proformaInvoice.itemsOnInovice) {
          setItems(proformaInvoice.itemsOnInovice);
        }
        setProformaInvoiceNumber(proformaInvoice.number);
      } catch (error) {
        console.log(error);
      }
    };
    fetchInovice();
  }, [id]);

  const addItem = () => {
    const newItem: IItemsEdit = {
      description: "",
      serviceId: 0,
      quantity: 0,
      price: 0,
      numberOfDays: "",
    };
    setItems([...items, newItem]);
  };

  const removeItem = (indexToRemove: number) => {
    // Prije nego što obrišemo stavku, pridružimo njen ID u itemsToDelete niz
    const itemId = items[indexToRemove].id; // Pretpostavimo da svaka stavka ima jedinstveni 'id'

    if (itemId !== undefined) {
      // Dodamo ID stavke u itemsToDelete niz
      setAddProformaInvoice((prevInvoice) => ({
        ...prevInvoice,
        itemsToDelete: prevInvoice.itemsToDelete
          ? [...prevInvoice.itemsToDelete, itemId]
          : [itemId],
      }));
    }

    // Onda uklonimo stavku iz niza
    setItems(items.filter((_, index) => index !== indexToRemove));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const newInvoiceData: IEditProformaInvoice = {
      clientId: addProformaInvoice.clientId,
      date: addProformaInvoice.date,
      paymentDeadline: addProformaInvoice.paymentDeadline,
      note: addProformaInvoice.note,
      priceWithoutVat: addProformaInvoice.priceWithoutVat,
      itemsOnInovice: items,
      itemsToDelete: addProformaInvoice.itemsToDelete || [],
      offerAccepted: addProformaInvoice.offerAccepted,
      proformaWithoutVat: addProformaInvoice.proformaWithoutVat,
      file: addProformaInvoice.file,
    };

    console.log("Podaci iz forme:", newInvoiceData);

    await updateProformaInvoice(Number(id), newInvoiceData);
    navigate("/proformainvoices");
  };

  return (
    <div>
      <div>
        <Button
          buttonStyle={{
            marginLeft: "15px",
            marginTop: "15px",
            backgroundColor: "rgb(100,100,100)",
          }}
          onClick={() => navigate("/proformainvoices")}
        >
          Nazad
        </Button>
        <h1
          style={{
            paddingTop: "10px",
          }}
        >
          Izmjena profakture: {proformaInvoiceNumber}
        </h1>
      </div>
      <form onSubmit={handleSubmit} className="form-container-inovice">
        <div
          style={{
            display: "flex",
            alignItems: "center",
            marginBottom: "2px",
          }}
        >
          <input
            type="checkbox"
            checked={addProformaInvoice.offerAccepted}
            onChange={(e) =>
              setAddProformaInvoice({
                ...addProformaInvoice,
                offerAccepted: e.target.checked,
              })
            }
          />
          <label
            htmlFor="offerAccepted"
            style={{ marginTop: "6px", fontSize: "16px" }}
          >
            Ponuda prihvaćena
          </label>
        </div>
        <div>
          <label>Klijent</label>
          <select
            name="clientId"
            value={addProformaInvoice.clientId || ""}
            onChange={(e) =>
              setAddProformaInvoice({
                ...addProformaInvoice,
                clientId: Number(e.target.value),
              })
            }
          >
            <option value="" disabled>
              Izaberi klijenta
            </option>
            {clients.map((client) => (
              <option value={client.id} key={client.id}>
                {client.name}
              </option>
            ))}
          </select>
        </div>
        <div>
          <label>Datum</label>
          <input
            type="datetime-local"
            name="date"
            onChange={(e) =>
              setAddProformaInvoice({
                ...addProformaInvoice,
                date: e.target.value,
              })
            }
            value={addProformaInvoice.date}
          />
        </div>
        <div>
          <label>Rok plaćanja(dana)</label>
          <input
            type="number"
            name="paymentDeadline"
            value={
              addProformaInvoice.paymentDeadline !== null
                ? addProformaInvoice.paymentDeadline
                : ""
            }
            onChange={(e) =>
              setAddProformaInvoice({
                ...addProformaInvoice,
                paymentDeadline: Number(e.target.value),
              })
            }
          />
        </div>
        <div
          style={{
            display: "flex",
            alignItems: "center",
            marginBottom: "2px",
          }}
        >
          <input
            type="checkbox"
            name="priceWithoutVAT"
            id="priceWithoutVAT"
            checked={addProformaInvoice.priceWithoutVat}
            onChange={(e) =>
              setAddProformaInvoice({
                ...addProformaInvoice,
                priceWithoutVat: e.target.checked,
              })
            }
          />
          <label
            htmlFor="priceWithoutVAT"
            style={{ marginTop: "6px", fontSize: "13px" }}
          >
            Cijena bez PDV-a
          </label>

          <input
            type="checkbox"
            name="proinvoiceWithoutVat"
            id="proinvoiceWithoutVat"
            checked={addProformaInvoice.proformaWithoutVat}
            onChange={(e) =>
              setAddProformaInvoice({
                ...addProformaInvoice,
                proformaWithoutVat: e.target.checked,
              })
            }
            style={{ marginLeft: "10px" }}
          />
          <label
            htmlFor="proinvoiceWithoutVat"
            style={{ marginTop: "6px", fontSize: "13px" }}
          >
            Ponuda bez prikazivanja PDV-a
          </label>
        </div>
        <div>
          <label style={{ fontSize: "17px" }}>Stavke na računu</label>
          <button type="button" onClick={addItem} className="add-item-button">
            <HiOutlinePlusSm size="13px" style={{}} /> <span>Dodaj stavke</span>
          </button>

          {items.map((item, index) => (
            <div style={{ color: "whitesmoke" }} key={index}>
              <p style={{ marginBottom: "1px", color: "whitesmoke" }}>
                {index + 1}.
              </p>
              <div
                key={index}
                style={{
                  display: "flex",
                  gap: "10px",
                }}
              >
                <select
                  style={{ width: "125px" }}
                  value={item.serviceId || ""}
                  onChange={(e) =>
                    setItems((prevItems) => {
                      const newItems = [...prevItems];
                      newItems[index].serviceId = Number(e.target.value);
                      return newItems;
                    })
                  }
                >
                  <option value="" disabled>
                    Izaberi uslugu
                  </option>
                  {services.map((service) => (
                    <option key={service.id} value={service.id}>
                      {service.description}
                    </option>
                  ))}
                </select>
                <input
                  type="text"
                  style={{ width: "225px" }}
                  placeholder="Opis"
                  value={item.description}
                  onChange={(e) =>
                    setItems((prevItems) => {
                      const newItems = [...prevItems];
                      newItems[index].description = e.target.value;
                      return newItems;
                    })
                  }
                />
                Cijena:
                <input
                  style={{ width: "60px" }}
                  type="number"
                  value={item.price}
                  onChange={(e) =>
                    setItems((prevItems) => {
                      const newItems = [...prevItems];
                      newItems[index].price = Number(e.target.value);
                      return newItems;
                    })
                  }
                />
                Količina:
                <input
                  style={{ width: "60px" }}
                  type="number"
                  value={item.quantity}
                  onChange={(e) =>
                    setItems((prevItems) => {
                      const newItems = [...prevItems];
                      newItems[index].quantity = Number(e.target.value);
                      return newItems;
                    })
                  }
                />
                Broj dana:
                <input
                  style={{ width: "60px" }}
                  type="number"
                  value={item.numberOfDays}
                  onChange={(e) =>
                    setItems((prevItems) => {
                      const newItems = [...prevItems];
                      newItems[index].numberOfDays = e.target.value;
                      return newItems;
                    })
                  }
                />
              </div>
              <button
                type="button"
                onClick={() => removeItem(index)}
                className="remove-item-button"
              >
                <HiMinus style={{ marginRight: "2px" }} />
                Obriši stavku
              </button>
            </div>
          ))}
        </div>

        <div>
          <label>Napomena</label>
          <textarea
            name="note"
            rows={3}
            cols={52}
            value={addProformaInvoice.note || ""}
            onChange={(e) =>
              setAddProformaInvoice({
                ...addProformaInvoice,
                note: e.target.value,
              })
            }
          ></textarea>
        </div>
        <div>
          <label
            htmlFor="file-upload"
            style={{ marginTop: "6px", fontSize: "14px" }}
          >
            Plan puta
          </label>
          {fileId ? (
            <div
              style={{
                display: "flex",
                justifyContent: "start",
                alignItems: "center",
                paddingTop: "2px",
              }}
            >
              <button
                style={{
                  color: "whitesmoke",
                  textDecoration: "underline",
                  cursor: "pointer",
                  paddingBottom: "6px",
                  background: "none",
                  border: "none",
                  padding: 0,
                }}
                onClick={(e) => {
                  e.preventDefault();
                  downloadRoutePlan(Number(id));
                }}
              >
                {fileName}
              </button>

              <button
                style={{
                  display: "flex",
                  justifyContent: "start",
                  alignItems: "center",
                  color: "#ff4d4d",
                  paddingTop: "2px",
                  cursor: "pointer",
                  background: "none",
                  border: "none",
                  padding: 0,
                }}
                onClick={handleFileDeletion}
              >
                <CiCircleRemove
                  style={{
                    color: "#ff4d4d",
                    marginLeft: "15px",
                  }}
                  size="19px"
                />
                Obriši plan puta
              </button>
            </div>
          ) : (
            <input
              type="file"
              name="routeplan"
              id="routeplan"
              className="custom-file-input"
              onChange={handleFileUpload}
            />
          )}
        </div>

        <button type="submit">Sačuvaj izmjene</button>
      </form>
    </div>
  );
};

export default EditProformaInvoice;
