import React from "react";
import "./popup.css";
import Button from "./Button";

type Props = {
  isOpen: boolean;
  onClose: () => void;
  text?: string;
};

const PopUp = ({
  isOpen,
  onClose,
  text = "Molim Vas dodajte prvo firmu da bi ste nastavili.",
}: Props) => {
  if (!isOpen) return null;
  return (
    <div className="modalOverlay">
      <div className="modalContent">
        <h2>Greška</h2>
        <p>{text}</p>
        <Button onClick={onClose}>U redu</Button>
      </div>
    </div>
  );
};

export default PopUp;
