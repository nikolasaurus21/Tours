import React, { useState } from "react";
import "./footer.css";
import "./emailModal.css";

const Footer = () => {
  const currentYear = new Date().getFullYear();
  const emailAddress = "nikola01djikanovic@gmail.com"; // Dodajte stvarnu mail adresu ovdje

  const [isModalOpen, setIsModalOpen] = useState(false);

  const handleEmailClick = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
  };

  return (
    <div className="footer">
      <span onClick={handleEmailClick}>&copy; Nikola {currentYear}</span>

      {isModalOpen && (
        <div className="email-modal-overlay">
          <div className="email-modal-content">
            <span className="close-button" onClick={handleCloseModal}>
              &times;
            </span>
            <div className="email-address">Email: {emailAddress}</div>
            <button className="close-modal-button" onClick={handleCloseModal}>
              Close
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default Footer;
