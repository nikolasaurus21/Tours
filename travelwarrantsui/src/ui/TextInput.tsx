import React, { CSSProperties } from "react";
interface TextInputProps {
  value: string;
  onChange: (newValue: string) => void;
  placeholder?: string;
  style?: CSSProperties;
}
const defaultStyle: CSSProperties = {
  padding: "10px",
  fontSize: "16px",
  border: "none",
  width: "250px",
};
const TextInput = ({ value, onChange, placeholder, style }: TextInputProps) => {
  const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    onChange(event.target.value);
  };
  return (
    <input
      type="text"
      value={value}
      onChange={handleChange}
      placeholder={placeholder}
      style={{ ...defaultStyle, ...style }}
    />
  );
};

export default TextInput;
