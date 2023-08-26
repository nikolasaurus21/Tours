import React from "react";
import "./App.css";
import SideMenu from "./components/sidemenu/SideMenu";

import Footer from "./components/footer/Footer";
import AppRoutes from "./AppRoutes";
function App() {
  return (
    <div className="container">
      <div className="left-side">
        <SideMenu />
        <Footer />
      </div>
      <div className="right-side">
        <AppRoutes />
      </div>
    </div>
  );
}

export default App;
