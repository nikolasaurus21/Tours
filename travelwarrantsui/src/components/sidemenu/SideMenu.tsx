import React, { useState } from "react";
import "./sidemenu.css";
import {
  AiFillHome,
  AiOutlineInfoCircle,
  AiOutlineCar,
  AiOutlineBarChart,
  AiOutlineDollar,
} from "react-icons/ai";
import { MdDescription } from "react-icons/md";
import { useNavigate } from "react-router-dom";
import { FaChevronDown, FaChevronUp } from "react-icons/fa";

const SideMenu = () => {
  const navigate = useNavigate();
  const [isBasicInfoMenuOpen, setIsBasicInfoMenuOpen] = useState(false);
  const [isFinanceMenuOpen, setIsFinanceMenuOpen] = useState(false);
  const [isInvoiceMenuOpen, setIsInvoiceMenuOpen] = useState(false);

  const handleBasicInfoClick = () => {
    setIsBasicInfoMenuOpen((prevState) => !prevState);
  };

  const handleFinanceClick = () => {
    setIsFinanceMenuOpen((prevState) => !prevState);
  };

  const handleInvoiceClick = () => {
    setIsInvoiceMenuOpen((prevState) => !prevState);
  };

  return (
    <div className="side-menu">
      <div className="menu-item" onClick={() => navigate("/")}>
        <span className="icon">
          <AiFillHome />
        </span>
        <span className="text">Početna</span>
      </div>
      <div
        className={`menu-item ${isBasicInfoMenuOpen ? "open" : ""}`}
        onClick={handleBasicInfoClick}
      >
        <span className="icon">
          <AiOutlineInfoCircle />
        </span>
        <span className="text">Osnovni podaci</span>
        <span className="arrow-icon">
          {isBasicInfoMenuOpen ? (
            <FaChevronUp style={{ marginLeft: "5px" }} />
          ) : (
            <FaChevronDown style={{ marginLeft: "5px" }} />
          )}
        </span>
      </div>
      {isBasicInfoMenuOpen && (
        <div className="submenu">
          <div className="submenu-item" onClick={() => navigate("/clients")}>
            Klijent
          </div>
          <div className="submenu-item" onClick={() => navigate("/vehicles")}>
            Vozila
          </div>
          <div className="submenu-item" onClick={() => navigate("/drivers")}>
            Vozači
          </div>
          <div className="submenu-item" onClick={() => navigate("/company")}>
            Firma
          </div>
          <div
            className="submenu-item"
            onClick={() => navigate("/giroaccounts")}
          >
            Žiro-Račun
          </div>
          <div className="submenu-item" onClick={() => navigate("/services")}>
            Usluge
          </div>
        </div>
      )}
      <div className="menu-item " onClick={() => navigate("/")}>
        <span className="icon">
          <AiOutlineCar />
        </span>
        <span className="text">Putni nalozi</span>
      </div>

      <div
        className={`menu-item ${isInvoiceMenuOpen ? "open" : ""}`}
        onClick={handleInvoiceClick}
      >
        <span className="icon">
          <MdDescription />
        </span>
        <span className="text">Fakture</span>
        <span className="arrow-icon">
          {isInvoiceMenuOpen ? (
            <FaChevronUp style={{ marginLeft: "5px" }} />
          ) : (
            <FaChevronDown style={{ marginLeft: "5px" }} />
          )}
        </span>
      </div>
      {isInvoiceMenuOpen && (
        <div className="submenu">
          <div className="submenu-item">Fakture</div>
          <div className="submenu-item">Profakture</div>
        </div>
      )}
      <div className="menu-item " onClick={() => navigate("/reports")}>
        <span className="icon">
          <AiOutlineBarChart />
        </span>
        <span className="text">Izvještaji</span>
      </div>
      <div
        className={`menu-item ${isFinanceMenuOpen ? "open" : ""}`}
        onClick={handleFinanceClick}
      >
        <span className="icon">
          <AiOutlineDollar />
        </span>
        <span className="text">Finansije</span>
        <span className="arrow-icon">
          {isFinanceMenuOpen ? (
            <FaChevronUp style={{ marginLeft: "5px" }} />
          ) : (
            <FaChevronDown style={{ marginLeft: "5px" }} />
          )}
        </span>
      </div>
      {isFinanceMenuOpen && (
        <div className="submenu">
          <div className="submenu-item" onClick={() => navigate("/payments")}>
            Uplate
          </div>
          <div className="submenu-item" onClick={() => navigate("/searches")}>
            Potraživanja
          </div>
          <div className="submenu-item" onClick={() => navigate("/status")}>
            Stanje
          </div>
        </div>
      )}
    </div>
  );
};

export default SideMenu;
