import { Routes, Route } from "react-router-dom";
import TravelWarrants from "./pages/travelwarrants/TravelWarrants";
import Clients from "./pages/clients/Clients";
import AddClient from "./pages/clients/AddClient";
import EditClient from "./pages/clients/EditClient";
import Vehicles from "./pages/vehicles/Vehicles";
import AddVehicle from "./pages/vehicles/AddVehicle";
import EditVehicle from "./pages/vehicles/EditVehicle";
import Drivers from "./pages/drivers/Drivers";
import AddDriver from "./pages/drivers/AddDriver";
import EditDriver from "./pages/drivers/EditDriver";
import Company from "./pages/company/Company";
import GiroAccounts from "./pages/bankaccounts/BankAccounts";
import AddGiroAccount from "./pages/bankaccounts/AddBankAccount";
import EditGiroAccount from "./pages/bankaccounts/EditBankAccount";
import Services from "./pages/services/Services";
import AddService from "./pages/services/AddService";
import EditService from "./pages/services/EditService";
import AddTravelWarrants from "./pages/travelwarrants/AddTravelWarrants";
import EditTravelWarrants from "./pages/travelwarrants/EditTravelWarrants";
import Payments from "./pages/payments/Payments";
import AddPayment from "./pages/payments/AddPayment";
import EditPayment from "./pages/payments/EditPayment";
import Searches from "./pages/searches/Searches";
import AddSearch from "./pages/searches/AddSearch";
import EditSearch from "./pages/searches/EditSearch";
import Reports from "./pages/reports/Reports";
import ClientReports from "./pages/reports/ClientReports";
import DestinationReports from "./pages/reports/DestinationReports";
import DepDesReports from "./pages/reports/DepDesReports";
import VehicleReports from "./pages/reports/VehicleReports";
import DriverReports from "./pages/reports/DriverReports";
import PeriodReports from "./pages/reports/PeriodReports";
import Status from "./pages/status/Status";
import DeleteClient from "./pages/clients/DeleteClient";
import DeleteVehicle from "./pages/vehicles/DeleteVehicle";
import DeleteDriver from "./pages/drivers/DeleteDriver";
import DeleteGiroAccount from "./pages/bankaccounts/DeleteBankAccount";
import DeleteService from "./pages/services/DeleteService";
import DeleteTour from "./pages/travelwarrants/DeleteTour";
import Inovices from "./pages/inovice/Invoices";
import NewInovice from "./pages/inovice/NewInvoice";
import EditInovice from "./pages/inovice/EditInovice";
import DeleteInovice from "./pages/inovice/DeleteInvoice";
import InoviceReportsForClient from "./pages/reports/InoviceReportsForClient";
import InoviceReportsForPeriod from "./pages/reports/InoviceReportsForPeriod";
import InoviceReportsByDescription from "./pages/reports/InoviceReportsByDescription";
import ProformaInvoices from "./pages/proformaInvoices/ProformaInvoices";
import NewProformaInvoice from "./pages/proformaInvoices/NewProformaInvoice";
import EditProformaInvoice from "./pages/proformaInvoices/EditProformaInvoice";
import DeleteProformaInvoice from "./pages/proformaInvoices/DeleteProformaInvoice";
import ProformaInvoiceReportsForClient from "./pages/reports/ProformaInvoiceReportsForClient";
import ProformaInvoiceReportsForPeriod from "./pages/reports/ProformaInvoiceReportsForPeriod";
import ProformaInvoiceReportsByDescription from "./pages/reports/ProformaInvoiceReportsByDescription";

const AppRoutes = () => {
  return (
    <div>
      <Routes>
        <Route path="/" element={<TravelWarrants />} />

        <Route path="/travelwarrants" element={<TravelWarrants />} />
        <Route path="/travelwarrants/add" element={<AddTravelWarrants />} />
        <Route
          path="/travelwarrants/edit/:id"
          element={<EditTravelWarrants />}
        />
        <Route path="/travelwarrants/delete/:id" element={<DeleteTour />} />

        <Route path="/clients" element={<Clients />} />
        <Route path="/clients/add" element={<AddClient />} />
        <Route path="/clients/edit/:id" element={<EditClient />} />
        <Route path="/clients/delete/:id" element={<DeleteClient />} />

        <Route path="/vehicles" element={<Vehicles />} />
        <Route path="/vehicles/add" element={<AddVehicle />} />
        <Route path="/vehicles/edit/:id" element={<EditVehicle />} />
        <Route path="/vehicles/delete/:id" element={<DeleteVehicle />} />

        <Route path="/drivers" element={<Drivers />} />
        <Route path="/drivers/add" element={<AddDriver />} />
        <Route path="/drivers/edit/:id" element={<EditDriver />} />
        <Route path="/drivers/delete/:id" element={<DeleteDriver />} />

        <Route path="/company" element={<Company />} />

        <Route path="/bankaccounts" element={<GiroAccounts />} />
        <Route path="/bankaccounts/add" element={<AddGiroAccount />} />
        <Route path="/bankaccounts/edit/:id" element={<EditGiroAccount />} />
        <Route
          path="/bankaccounts/delete/:id"
          element={<DeleteGiroAccount />}
        />

        <Route path="/services" element={<Services />} />
        <Route path="/services/add" element={<AddService />} />
        <Route path="/services/edit/:id" element={<EditService />} />
        <Route path="/services/delete/:id" element={<DeleteService />} />

        <Route path="/payments" element={<Payments />} />
        <Route path="/payments/add" element={<AddPayment />} />
        <Route path="/payments/edit/:id" element={<EditPayment />} />

        <Route path="/searches" element={<Searches />} />
        <Route path="/searches/add" element={<AddSearch />} />
        <Route path="/searches/edit/:id" element={<EditSearch />} />

        <Route path="/reports" element={<Reports />} />
        <Route
          path="/reports/traverwarrantsperclient"
          element={<ClientReports />}
        />
        <Route
          path="/reports/traverwarrantstodestination"
          element={<DestinationReports />}
        />
        <Route
          path="/reports/traverwarrantsdep-desreports"
          element={<DepDesReports />}
        />
        <Route
          path="/reports/traverwarrantsvehiclereports"
          element={<VehicleReports />}
        />
        <Route
          path="/reports/traverwarrantsdriverreports"
          element={<DriverReports />}
        />
        <Route
          path="/reports/traverwarrantsperiodreports"
          element={<PeriodReports />}
        />
        <Route
          path="/reports/invoicesforclient"
          element={<InoviceReportsForClient />}
        />
        <Route
          path="/reports/inovicesforperiod"
          element={<InoviceReportsForPeriod />}
        />
        <Route
          path="/reports/invoicesbydescription"
          element={<InoviceReportsByDescription />}
        />
        <Route
          path="/reports/proformainvoiceforclient"
          element={<ProformaInvoiceReportsForClient />}
        />
        <Route
          path="/reports/proformainvoiceforperiod"
          element={<ProformaInvoiceReportsForPeriod />}
        />
        <Route
          path="/reports/proformainvoicebydescription"
          element={<ProformaInvoiceReportsByDescription />}
        />

        <Route path="/status" element={<Status />} />

        <Route path="/invoices" element={<Inovices />} />
        <Route path="/invoices/add" element={<NewInovice />} />
        <Route path="/invoices/edit/:id" element={<EditInovice />} />
        <Route path="/invoices/delete/:id" element={<DeleteInovice />} />

        <Route path="/proformainvoices" element={<ProformaInvoices />} />
        <Route path="/proformainvoices/add" element={<NewProformaInvoice />} />
        <Route
          path="/proformainvoices/edit/:id"
          element={<EditProformaInvoice />}
        />
        <Route
          path="/proformainvoices/delete/:id"
          element={<DeleteProformaInvoice />}
        />
      </Routes>
    </div>
  );
};

export default AppRoutes;
