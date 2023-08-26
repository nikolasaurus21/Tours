import React, { CSSProperties } from "react";
import "./button.css";
interface StyledButtonProps {
  buttonStyle?: CSSProperties;
  onClick?: () => void;
  children?: React.ReactNode;
}
const Button = ({ buttonStyle, onClick, children }: StyledButtonProps) => {
  return (
    <button
      className="dugme-hover"
      style={{
        padding: "10px 20px",
        backgroundColor: "#005f40",
        color: "#fff",
        border: "none",
        borderRadius: "4px",
        cursor: "pointer",
        letterSpacing: "2px",
        whiteSpace: "nowrap",

        ...buttonStyle,
      }}
      onClick={onClick}
    >
      {children}
    </button>
  );
};

export default Button;
