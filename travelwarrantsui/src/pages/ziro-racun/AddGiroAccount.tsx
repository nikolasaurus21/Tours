import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { addGiroAccounts } from "../../api/interfaces";

import Button from "../../ui/Button";
import { GiroAccountsContext } from "../../context/ZiroRacuniContext";
import { CompanyContext } from "../../context/CompanyContext";
import PopUp from "../../ui/PopUp";

const initialValues = {
  bank: "",
  account: "",
};
const AddGiroAccount = () => {
  const navigate = useNavigate();
  const { addAcc } = useContext(GiroAccountsContext);
  const { isCompanyAdded, isPopUpOpen, setIsPopUpOpen } =
    useContext(CompanyContext);
  const [addGiroAcc, setAddGiroAcc] = useState<addGiroAccounts>(initialValues);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>): void => {
    const { name, value, type, checked } = e.target;
    const newValue = type === "checkbox" ? checked : value;
    setAddGiroAcc((prevValues) => ({ ...prevValues, [name]: newValue }));
  };

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ): Promise<void> => {
    e.preventDefault();
    try {
      if (!isCompanyAdded) {
        setIsPopUpOpen(true);
      } else {
        await addAcc(addGiroAcc);

        navigate("/giroaccounts");
      }
    } catch (error) {
      console.error("Error adding client:", error);
    }
  };
  return (
    <div>
      <div>
        <div>
          <Button
            buttonStyle={{
              backgroundColor: "rgb(100,100,100)",
              marginTop: "10px",
              marginLeft: "10px",
            }}
            onClick={() => navigate("/giroaccounts")}
          >
            Nazad
          </Button>
          <h1>Novi žiro-račun</h1>
        </div>
        <form onSubmit={handleSubmit} className="form-container-basic">
          <div>
            <label>Banka</label>
            <input
              type="text"
              name="bank"
              value={addGiroAcc.bank}
              onChange={handleChange}
            />
          </div>
          <div>
            <label>Račun</label>
            <input
              type="text"
              name="account"
              value={addGiroAcc.account}
              onChange={handleChange}
            />
          </div>

          <button type="submit">Dodaj žiro-račun</button>
        </form>
        <PopUp isOpen={isPopUpOpen} onClose={() => setIsPopUpOpen(false)} />
      </div>
    </div>
  );
};

export default AddGiroAccount;
