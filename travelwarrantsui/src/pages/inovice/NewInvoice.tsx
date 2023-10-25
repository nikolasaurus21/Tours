import React, { useContext, useState } from "react";
import Button from "../../ui/Button";
import { useNavigate } from "react-router-dom";
import { ClientsContext } from "../../context/ClientsContext";
import { ServicesContext } from "../../context/ServicesContext";
import { HiOutlinePlusSm, HiMinus } from "react-icons/hi";
import "./inoviceform.css";
import { IAddInvoice, IAddItem } from "../../api/interfaces";
import { InvoicesContext } from "../../context/InvoicesContext";

const NewInovice = () => {
  const navigate = useNavigate();

  const { clients } = useContext(ClientsContext);
  const { services } = useContext(ServicesContext);
  const { addInvoice: addInovice } = useContext(InvoicesContext);

  const [items, setItems] = useState<IAddItem[]>([]);
  const [addInvoice, setAddInvoice] = useState<IAddInvoice>({
    clientId: 0,
    date: "",
    paymentDeadline: 0,
    note: "",
    priceWithoutVat: false,
    itemsOnInovice: [],
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

    const newInvoiceData: IAddInvoice = {
      clientId: addInvoice.clientId,
      date: addInvoice.date,
      paymentDeadline: addInvoice.paymentDeadline,
      note: addInvoice.note,
      priceWithoutVat: addInvoice.priceWithoutVat,
      itemsOnInovice: items,
    };

    //console.log("Podaci iz forme:", newInvoiceData);

    await addInovice(newInvoiceData);
    navigate("/invoices");
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
          onClick={() => navigate("/invoices")}
        >
          Nazad
        </Button>
        <h1
          style={{
            paddingTop: "10px",
          }}
        >
          Nova faktura
        </h1>
      </div>
      <form onSubmit={handleSubmit} className="form-container-inovice">
        <div>
          <label>Klijent</label>
          <select
            name="clientId"
            value={addInvoice.clientId || ""}
            onChange={(e) =>
              setAddInvoice({ ...addInvoice, clientId: Number(e.target.value) })
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
              setAddInvoice({ ...addInvoice, date: e.target.value })
            }
            value={addInvoice.date}
          />
        </div>
        <div>
          <label>Rok plaćanja(dana)</label>
          <input
            type="number"
            min={0}
            name="paymentDeadline"
            onChange={(e) =>
              setAddInvoice({
                ...addInvoice,
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
              setAddInvoice({
                ...addInvoice,
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
              setAddInvoice({ ...addInvoice, note: e.target.value })
            }
          ></textarea>
        </div>

        <button type="submit">Dodaj fakturu</button>
      </form>
    </div>
  );
};

export default NewInovice;
