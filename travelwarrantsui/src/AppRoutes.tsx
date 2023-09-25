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
import GiroAccounts from "./pages/ziro-racun/GiroAccounts";
import AddGiroAccount from "./pages/ziro-racun/AddGiroAccount";
import EditGiroAccount from "./pages/ziro-racun/EditGiroAccount";
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
import DeleteGiroAccount from "./pages/ziro-racun/DeleteGiroAccount";
import DeleteService from "./pages/services/DeleteService";
import DeleteTour from "./pages/travelwarrants/DeleteTour";
import Inovices from "./pages/inovice/Inovices";
import NewInovice from "./pages/inovice/NewInovice";

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

        <Route path="/giroaccounts" element={<GiroAccounts />} />
        <Route path="/giroaccounts/add" element={<AddGiroAccount />} />
        <Route path="/giroaccounts/edit/:id" element={<EditGiroAccount />} />
        <Route
          path="/giroaccounts/delete/:id"
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

        <Route path="/status" element={<Status />} />

        <Route path="/inovices" element={<Inovices />} />
        <Route path="/inovices/add" element={<NewInovice />} />
      </Routes>
    </div>
  );
};

export default AppRoutes;
