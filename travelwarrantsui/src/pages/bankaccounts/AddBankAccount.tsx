import React, { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { addBankAccounts } from "../../api/interfaces";

import Button from "../../ui/Button";
import { BankAccountsContext } from "../../context/BankAccountsContext";

const initialValues = {
  bank: "",
  account: "",
};
const AddGiroAccount = () => {
  const navigate = useNavigate();
  const { addAcc } = useContext(BankAccountsContext);

  const [addGiroAcc, setAddGiroAcc] = useState<addBankAccounts>(initialValues);

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
      await addAcc(addGiroAcc);

      navigate("/bankaccounts");
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
            onClick={() => navigate("/bankaccounts")}
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
      </div>
    </div>
  );
};

export default AddGiroAccount;
