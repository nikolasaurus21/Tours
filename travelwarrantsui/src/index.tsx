import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { BrowserRouter } from "react-router-dom";
import { TravelWarrantsProvider } from "./context/ToursContext";
import { ClientsProvider } from "./context/ClientsContext";
import { VehiclesProvider } from "./context/VehiclesContext";
import { DriversProvider } from "./context/DriversContext";
import { CompanyProvider } from "./context/CompanyContext";
import { BankAccountsProvider } from "./context/BankAccountsContext";
import { ServicesProvider } from "./context/ServicesContext";
import { PaymentsProvider } from "./context/PaymentsContext";
import { SearchesProvider } from "./context/SearchesContext";
import { StatusesProvider } from "./context/StatusesContext";
import { PaginationProvider } from "./context/PaginationContext";
import { InvoicesProvider } from "./context/InvoicesContext";
import { ProformaInvoicesProvider } from "./context/ProformaInvoicesContext";
const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <React.StrictMode>
    <ClientsProvider>
      <VehiclesProvider>
        <DriversProvider>
          <CompanyProvider>
            <BankAccountsProvider>
              <ServicesProvider>
                <TravelWarrantsProvider>
                  <PaymentsProvider>
                    <SearchesProvider>
                      <StatusesProvider>
                        <InvoicesProvider>
                          <ProformaInvoicesProvider>
                            <PaginationProvider>
                              <BrowserRouter>
                                <App />
                              </BrowserRouter>
                            </PaginationProvider>
                          </ProformaInvoicesProvider>
                        </InvoicesProvider>
                      </StatusesProvider>
                    </SearchesProvider>
                  </PaymentsProvider>
                </TravelWarrantsProvider>
              </ServicesProvider>
            </BankAccountsProvider>
          </CompanyProvider>
        </DriversProvider>
      </VehiclesProvider>
    </ClientsProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
