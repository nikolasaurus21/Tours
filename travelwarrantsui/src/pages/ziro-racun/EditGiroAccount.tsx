import React, { useContext, useEffect, useState } from "react";
import Button from "../../ui/Button";
import { useNavigate, useParams } from "react-router-dom";
import { addGiroAccounts } from "../../api/interfaces";
import { getGiroAccountById } from "../../api/api";
import { GiroAccountsContext } from "../../context/ZiroRacuniContext";

const initialValues = {
  bank: "",
  account: "",
};
const EditGiroAccount = () => {
  const navigate = useNavigate();
  const { editAcc } = useContext(GiroAccountsContext);
  const { id } = useParams();
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
      await editAcc(Number(id), addGiroAcc);

      navigate("/giroaccounts");
    } catch (error) {
      console.error("Error adding client:", error);
    }
  };

  useEffect(() => {
    const getSingleGiroAcc = async () => {
      try {
        const giroData = await getGiroAccountById(Number(id));
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
            onClick={() => navigate("/giroaccounts")}
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
