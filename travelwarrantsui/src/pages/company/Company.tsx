import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { ICompany } from "../../api/interfaces";
import Button from "../../ui/Button";
import { CompanyContext } from "../../context/CompanyContext";

const defaultCompany = {
  name: "",
  description: "",
  address: "",
  ptt: "",
  place: "",
  telephone: "",
  fax: "",
  mobilePhone: "",
  tin: "",
  vat: "",
};

const Company = () => {
  const navigate = useNavigate();
  const { company, companyAdd } = useContext(CompanyContext);

  const companyToAdd = company.length > 0 ? company[0] : defaultCompany;

  const [addOrEditCompany, setCompanyAddorEdit] =
    useState<ICompany>(companyToAdd);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    const { name, value, type, checked } = e.target;
    const newValue = type === "checkbox" ? checked : value;
    setCompanyAddorEdit((prevValues) => ({ ...prevValues, [name]: newValue }));
  };

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ): Promise<void> => {
    e.preventDefault();
    try {
      await companyAdd(addOrEditCompany);

      navigate("/");
    } catch (error) {
      console.error("Error adding client:", error);
    }
  };

  return (
    <div>
      <div>
        <Button
          buttonStyle={{ backgroundColor: "rgb(100,100,100)", margin: "10px" }}
          onClick={() => navigate("/")}
        >
          Nazad
        </Button>
        <h1
          style={{
            paddingTop: "0px",
          }}
        >
          Company
        </h1>
      </div>
      <form onSubmit={handleSubmit} className="form-container-basic">
        <div>
          <label>Naziv</label>
          <input
            type="text"
            name="name"
            value={addOrEditCompany.name}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Napomena</label>
          <input
            type="text"
            name="description"
            value={addOrEditCompany.description}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Adresa</label>
          <input
            type="text"
            name="address"
            value={addOrEditCompany.address}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>PTT</label>
          <input
            type="text"
            name="ptt"
            value={addOrEditCompany.ptt}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Mjesto</label>
          <input
            type="text"
            name="place"
            value={addOrEditCompany.place}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Telefon</label>
          <input
            type="text"
            name="telephone"
            value={addOrEditCompany.telephone}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Fax</label>
          <input
            type="text"
            name="fax"
            value={addOrEditCompany.fax}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Mobilni telefon</label>
          <input
            type="text"
            name="mobilePhone"
            value={addOrEditCompany.mobilePhone}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>PIB</label>
          <input
            type="text"
            name="tin"
            value={addOrEditCompany.tin}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>PDV</label>
          <input
            type="text"
            name="vat"
            value={addOrEditCompany.vat}
            onChange={handleChange}
          />
        </div>
        <button type="submit">Sačuvaj</button>
      </form>
    </div>
  );
};

export default Company;
