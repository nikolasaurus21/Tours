import React, { useContext, useState } from "react";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { ClientsContext } from "../../context/ClientsContext";
import { ServicesContext } from "../../context/ServicesContext";
import { HiMinus, HiOutlinePlusSm } from "react-icons/hi";
import { IAddItem, IAddProformaInvoice } from "../../api/interfaces";
import { ProformaInovicesContext } from "../../context/ProformaInvoicesContext";

const NewProformaInvoice = () => {
  const navigate = useNavigate();

  const { clients } = useContext(ClientsContext);
  const { services } = useContext(ServicesContext);
  const { addProformaInvoice } = useContext(ProformaInovicesContext);

  const [items, setItems] = useState<IAddItem[]>([]);
  const [newProformaInvoice, setNewProformaInvoice] =
    useState<IAddProformaInvoice>({
      clientId: 0,
      date: "",
      paymentDeadline: 0,
      note: "",
      priceWithoutVat: false,
      itemsOnInovice: [],
      proinvoiceWithoutVat: false,
      file: null,
    });
  const addItem = () => {
    const newItem: IAddItem = {
      description: "",
      serviceId: 0,
      quantity: 0,
      price: 0,
      numberOfDays: "",
    };
    setItems([...items, newItem]);
  };

  const removeItem = (indexToRemove: any) => {
    setItems(items.filter((_, index) => index !== indexToRemove));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const newProformaInvoiceData: IAddProformaInvoice = {
      clientId: newProformaInvoice.clientId,
      date: newProformaInvoice.date,
      paymentDeadline: newProformaInvoice.paymentDeadline,
      note: newProformaInvoice.note,
      priceWithoutVat: newProformaInvoice.priceWithoutVat,
      itemsOnInovice: items,
      proinvoiceWithoutVat: newProformaInvoice.proinvoiceWithoutVat,
      file: newProformaInvoice.file,
    };

    console.log("Podaci iz forme:", newProformaInvoiceData);

    await addProformaInvoice(newProformaInvoiceData);
    navigate("/proformainvoices");
  };

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files ? event.target.files[0] : null;
    setNewProformaInvoice((prevState) => ({
      ...prevState,
      file: file,
    }));
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
          Nova profaktura
        </h1>
      </div>
      <form
        encType="multipart/form-data"
        onSubmit={handleSubmit}
        className="form-container-inovice"
      >
        <div>
          <label>Klijent</label>
          <select
            name="clientId"
            value={newProformaInvoice.clientId || ""}
            onChange={(e) =>
              setNewProformaInvoice({
                ...newProformaInvoice,
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
              setNewProformaInvoice({
                ...newProformaInvoice,
                date: e.target.value,
              })
            }
            value={newProformaInvoice.date}
          />
        </div>
        <div>
          <label>Rok plaćanja(dana)</label>
          <input
            type="number"
            min={0}
            name="paymentDeadline"
            onChange={(e) =>
              setNewProformaInvoice({
                ...newProformaInvoice,
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
            onChange={(e) =>
              setNewProformaInvoice({
                ...newProformaInvoice,
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
            onChange={(e) =>
              setNewProformaInvoice({
                ...newProformaInvoice,
                proinvoiceWithoutVat: e.target.checked,
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
              <div key={index} style={{ display: "flex", gap: "10px" }}>
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
                  style={{ width: "255px" }}
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
                  min={0}
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
                  min={0}
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
                  min={0}
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
            onChange={(e) =>
              setNewProformaInvoice({
                ...newProformaInvoice,
                note: e.target.value,
              })
            }
          ></textarea>
        </div>
        <label>Plan puta</label>
        <input
          type="file"
          name="routeplan"
          id="routeplan"
          className="custom-file-input"
          onChange={handleFileChange}
        />

        <button type="submit">Dodaj profakturu</button>
      </form>
    </div>
  );
};

export default NewProformaInvoice;
