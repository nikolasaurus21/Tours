import React, { useContext, useEffect, useState } from "react";
import Button from "../../ui/Button";
import { useNavigate, useParams } from "react-router-dom";
import { addBankAccounts } from "../../api/interfaces";
import { getBankAccountById } from "../../api/api";
import { BankAccountsContext } from "../../context/BankAccountsContext";

const initialValues = {
  bank: "",
  account: "",
};
const EditGiroAccount = () => {
  const navigate = useNavigate();
  const { editAcc } = useContext(BankAccountsContext);
  const { id } = useParams();
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
      await editAcc(Number(id), addGiroAcc);

      navigate("/bankaccounts");
    } catch (error) {
      console.error("Error adding client:", error);
    }
  };

  useEffect(() => {
    const getSingleGiroAcc = async () => {
      try {
        const giroData = await getBankAccountById(Number(id));
        setAddGiroAcc(giroData);
      } catch (error) {
        console.log(error);
      }
    };
    getSingleGiroAcc();
  }, [id]);

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
          <h1>Izmijeni žiro-račun</h1>
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

          <button type="submit">Sačuvaj izmjene</button>
        </form>
      </div>
    </div>
  );
};

export default EditGiroAccount;
