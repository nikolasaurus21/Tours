import {  addClient, addVehicle,allClients, allVehicles,allDrivers,addDriver,ICompany, allBankAccounts, addBankAccounts, allServices, addService, allTravelWarrants, addTravelWarrant, allPayments, addPayment, IReports, Statuses, deleteTour, Invoices, IAddInvoice, IDeleteInvoice, IGetInvoiceById, IItemsEdit, IEditInvoice, IProformaInvoices, IAddProformaInvoice, IGetProformaInvoiceById, IEditProformaInvoice} from "./interfaces";
import axios from "axios";
import { format } from "date-fns";
import { utcToZonedTime } from "date-fns-tz";

//Clients
export const getAllClients =async ():Promise<allClients[]> => {
    const response = await axios.get("https://localhost:7206/api/Clients/GetClients");
    const jsonResponse = await response.data.map((x:any)=>({
        id:x.id,
        name:x.name,
        address:x.address,
        place:x.place
    }));
    return jsonResponse
}

export const addClients = async (data: addClient):Promise<allClients> => {
    
  const response =await axios.post(
        "https://localhost:7206/api/Clients/NewClient",
        {
          name: data.name,
          address: data.address,
          placeName: data.place,
          registrationNumber: data.tinurn,
          vat: data.vat,
          telephone: data.phone,
          fax: data.fax,
          email: data.email,
          excursion: data.excursion,
        },
        {
          headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
          },
        }
      );
      
      const addedClient:allClients = {
        id: response.data.id,
        name:response.data.name,
        address:response.data.address,
        place:response.data.place

      }
      return addedClient;
  };


  export const editClients = async (id:number,data: addClient):Promise<allClients> => {
    
    const response = await axios.put(
      `https://localhost:7206/api/Clients/EditClient/${id}`,
      {
        name: data.name,
        address: data.address,
        placeName: data.place,
        registrationNumber: data.tinurn,
        vat: data.vat,
        telephone: data.phone,
        fax: data.fax,
        email: data.email,
        excursion: data.excursion,
      },
      {
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
      }
    );

    const editedClient:allClients = {
      id: response.data.id,
      name:response.data.name,
      address:response.data.address,
      place:response.data.place

    }
    return editedClient;
 
};

export const getClientById = async (id: number): Promise<addClient> => {
    const response = await axios.get(`https://localhost:7206/api/Clients/GetClient/${id}`);
    const clientData = response.data; 
    const addClientData: addClient = {
      name: clientData.name,
      address: clientData.address,
      place: clientData.placeName,
      tinurn: clientData.registrationNumber,
      vat: clientData.vat,
      phone: clientData.telephone,
      fax: clientData.fax,
      email: clientData.email,
      excursion: clientData.excursion,
    };
    
   return addClientData;
  };


  
  //Vehicles
  export const getVehicles =async ():Promise<allVehicles[]> => {
    const response = await axios.get("https://localhost:7206/api/Fleet/GetFleet");
    const jsonResponse = await response.data.map((x:any)=>({
        id: x.id,
        registration: x.registration,
        name: x.name,
        note: x.note,
        numberOfSeats: x.numberOfSeats,
        fuel: x.fuelConsumption,
        mileage: x.mileage
    }));
    
    return jsonResponse
  }

  export const addVehicles =async (data:addVehicle):Promise<allVehicles> => {
    const response = await axios.post("https://localhost:7206/api/Fleet/NewVehicle",{
      name:data.name,
      registration:data.registration,
      note: data.note,
      numberOfSeats:data.numberOfSeats,
      fuelConsumption:data.fuel,
      mileage:data.mileage
    },
    {
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    })
    const newVehicle:allVehicles ={
      id:response.data.id,
      registration:response.data.registration,
      name:response.data.name,
      note:response.data.note,
      numberOfSeats:response.data.numberOfSeats,
      fuel:response.data.fuelConsumption,
      mileage:response.data.mileage

    }
    return newVehicle
  }

  export const editVehicles =async (id:number,data:addVehicle):Promise<allVehicles> => {
    const response = await axios.put(`https://localhost:7206/api/Fleet/EditVehicle/${id}`,{
      name:data.name,
      registration:data.registration,
      note: data.note,
      numberOfSeats:data.numberOfSeats,
      fuelConsumption:data.fuel,
      mileage:data.mileage
    },
    {
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    })

    const updatedVehicle:allVehicles ={
      id:response.data.id,
      registration:response.data.registration,
      name:response.data.name,
      note:response.data.note,
      numberOfSeats:response.data.numberOfSeats,
      fuel:response.data.fuelConsumption,
      mileage:response.data.mileage

    }
    return updatedVehicle
  }

  export const getVehicleById = async (id: number): Promise<addVehicle> => {
    const response = await axios.get(`https://localhost:7206/api/Fleet/GetVehicle/${id}`);
    const vehicleData = response.data; 
    const addVehicleData: addVehicle = {
      name: vehicleData.name,
      registration:vehicleData.registration,
      fuel: vehicleData.fuelConsumption,
      mileage:vehicleData.mileage,
      note:vehicleData.note,
      numberOfSeats:vehicleData.numberOfSeats
    };
    
   return addVehicleData;
  };

  //Drivers
  export const getDrivers =async ():Promise<allDrivers[]> => {

    const response = await axios.get("https://localhost:7206/api/Drivers/GetDrivers")
    const jsonResponse = await response.data.map((x:any)=>({
      id:x.id,
      name:x.name

    }))
    return jsonResponse
  }

  export const addDrivers =async (data:addDriver):Promise<allDrivers> => {
    const response = await axios.post("https://localhost:7206/api/Drivers/NewDriver",{
      name: data.name,
      numberOfPhone:data.phone
    },{
      headers:{
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    })

    const newDriver:allDrivers ={
      id:response.data.id,
      name:response.data.name,
    
    }
    return newDriver
  }

  export const editDrivers =async (id:number,data:addDriver):Promise<allDrivers> => {
    const response = await axios.put(`https://localhost:7206/api/Drivers/EditDriver/${id}`,{
      name: data.name,
      numberOfPhone:data.phone
    },{
      headers:{
        "Content-Type": "application/json",
        Accept: "application/json",
      },
    })

    const updatedDriver:allDrivers ={
      id:response.data.id,
      name:response.data.name,
    
    }
    return updatedDriver
  }

  export const getDriversById =async (id:number):Promise<addDriver> => {

    const response = await axios.get(`https://localhost:7206/api/Drivers/GetDriver/${id}`)
    const jsonResponse = await response.data
      const singleDriver = {

        phone:jsonResponse.numberOfPhone,
        name:jsonResponse.name
      }
    
    return singleDriver
  }

  //Company
  export const getCompany =async ():Promise<ICompany[]> => {
    const response =await axios.get("https://localhost:7206/api/Company/Get")
   
    const jsonResponse =  response.data.map((x:any)=> ({
      name: x.name,
      description: x.description,
      address: x.address,
      ptt: x.ptt,
      place: x.place,
      telephone: x.telephone,
      fax: x.fax,
      mobilePhone: x.mobilePhone,
      tin: x.tin,
      vat: x.vat
    }))
    
    return  jsonResponse
  }
  export const addCompany =async(data:ICompany) =>{
  await axios.post("https://localhost:7206/api/Company/NewCompany",{
    name: data.name,
      description: data.description,
      address: data.address,
      ptt: data.ptt,
      place: data.place,
      telephone: data.telephone,
      fax: data.fax,
      mobilePhone: data.mobilePhone,
      tin: data.tin,
      vat: data.vat
  },{
    headers:{
      "Content-Type": "application/json",
      Accept:"application/json",
    }
  })
}

//BankAccount

export const getBankAccounts =async ():Promise<allBankAccounts[]> => {
  const response = await axios.get("https://localhost:7206/api/BankAccounts/Get")
  const jsonResponse = await response.data.map((x:any) => ({
    id:x.id,
    bank:x.bank,
    account: x.accountNumber
  }))
  return  jsonResponse
}

export const getBankAccountById =async (id:number):Promise<addBankAccounts> => {
  const response = await axios.get(`https://localhost:7206/api/BankAccounts/GetAcc/${id}`)
  const jsonResponse = await response.data
  const singleAcc:addBankAccounts ={
    bank:jsonResponse.bank,
    account: jsonResponse.accountNumber
  }
  
  return  singleAcc
}

export const addBankAccount =async (data:addBankAccounts):Promise<allBankAccounts> => {
 const response =  await axios.post("https://localhost:7206/api/BankAccounts/NewBankAcc",{
    bank:data.bank,
    accountNumber:data.account,
    companyId:1
  },{
    headers:{
      "Content-Type":"application/json",
      Accept:"application/json",
    }
  })

  const newAcc:allBankAccounts ={
    id:response.data.id,
    bank:response.data.bank,
    account:response.data.accountNumber
  }

  return newAcc
}

export const editBankAccount =async (id:number,data:addBankAccounts):Promise<allBankAccounts> => {
  const response= await axios.put(`https://localhost:7206/api/BankAccounts/EditBankAcc/${id}`,{
    bank:data.bank,
    accountNumber:data.account,
    companyId:14
  },{
    headers:{
      "Content-Type":"application/json",
      Accept:"application/json",
    }
  })
  const updatedAcc:allBankAccounts ={
    id:response.data.id,
    bank:response.data.bank,
    account:response.data.accountNumber
  }

  return updatedAcc
}

//Services
export const getServices =async ():Promise<allServices[]> => {
  const response = await axios.get("https://localhost:7206/api/Services/GetServices")
  const jsonResponse = await response.data.map((x:any) => ({
    description:x.name,
    vat:x.vatRate,
    id:x.id
  }))
  return jsonResponse
}

export const getServicesById =async (id:number):Promise<addService> => {
  const response = await axios.get(`https://localhost:7206/api/Services/GetService/${id}`)
  const jsonResponse = await response.data
  const singleService:addService={
    
    name:jsonResponse.name,
    vat:jsonResponse.vatRate,
  }
  
  return singleService
}

export const addServices =async (data:addService):Promise<allServices> => {
  const response = await axios.post("https://localhost:7206/api/Services/NewService",{
    name: data.name,
    vatRate:data.vat
  },{
    headers:{
      "Content-Type":"application/json",
      Accept:"application/json"
    }
  })
  const newService:allServices ={
    id:response.data.id,
    description:response.data.name,
    vat:response.data.vatRate

  } 

  return newService
}

export const editServices =async (id:number,data:addService):Promise<allServices> => {
  const response= await axios.put(`https://localhost:7206/api/Services/EditService/${id}`,{
    name: data.name,
    vatRate:data.vat
  },{
    headers:{
      "Content-Type":"application/json",
      Accept:"application/json"
    }
  })

  const newService:allServices ={
    id:response.data.id,
    description:response.data.name,
    vat:response.data.vatRate

  } 

  return newService
}

//Travel Warrants

export const getTravelWarrants =async ():Promise<allTravelWarrants[]> => {
  const response = await axios.get("https://localhost:7206/api/TravelWarrants/GetTours")
  const jsonResponse = response.data.map((x:any) =>({
  id: x.id,
  client: x.clientName,
  departure: x.departure,
  destination: x.destination,
  interdestination: x.intermediateDestinations,
  mileage: x.mileage,
  date: format(new Date(x.timeOfTour), "dd/MM/yyyy HH:mm")
  }))
  return jsonResponse
}

export const getTravelWarrantsById =async (id:number):Promise<addTravelWarrant> => {
  const response = await axios.get(`https://localhost:7206/api/TravelWarrants/GetTour/${id}`)
  const jsonResponse = response.data
  const date = new Date(jsonResponse.timeOfTour);
  const formattedDate = format(date, "yyyy-MM-dd'T'HH:mm");
  const singleWarrant:addTravelWarrant={
    clientId: jsonResponse.clientId,
    departure: jsonResponse.departure,
    destination: jsonResponse.destination,
    intermediateDestinations: jsonResponse.intermediateDestinations,
    mileage: jsonResponse.mileage,
    timeOfTour: formattedDate,
  numberOfPassengers: jsonResponse.numberOfPassengers,
  price: jsonResponse.price,
  toll: jsonResponse.toll,
  fuel: jsonResponse.fuel,
  startMileage: jsonResponse.startMileage,
  endMileage: jsonResponse.endMileage,
  note: jsonResponse.note,
  driverId: jsonResponse.driverId,
  vehicleId: jsonResponse.vehicleId,
  fuelPrice: jsonResponse.fuelPrice,
  numberOfDays: jsonResponse.numberOfDays
  
  }
  
  return  singleWarrant
}
export const getTravelWarrantsDelete =async (id:number):Promise<deleteTour> => {
  const response = await axios.get(`https://localhost:7206/api/TravelWarrants/GetForDelete/${id}`)
  const jsonResponse = response.data
  const singleTour:deleteTour={
    client: jsonResponse.client,
    departure: jsonResponse.departure,
    destination: jsonResponse.destination,
    datetime:format(new Date(jsonResponse.date), "dd/MM/yyyy HH:mm"),
    registration:jsonResponse.registration,
    driver:jsonResponse.driver,
    mileage:jsonResponse.mileage
  }
  console.log({singleTour});
  
  return  singleTour
}

export const addWarrant =async (data:addTravelWarrant):Promise<allTravelWarrants> => {


    const utcDate = utcToZonedTime(data.timeOfTour, "UTC");
    const response = await axios.post(
      "https://localhost:7206/api/TravelWarrants/CreateTour",
      {
        clientId: data.clientId,
        departure: data.departure,
        destination: data.destination,
        intermediateDestinations: data.intermediateDestinations,
        mileage: data.mileage,
        numberOfPassengers: data.numberOfPassengers,
        price: data.price,
        toll: data.toll,
        fuel: data.fuel,
        timeOfTour: format(utcDate, "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"),
        vehicleId: data.vehicleId,
        startMileage: data.startMileage,
        endMileage: data.endMileage,
        note: data.note,
        driverId: data.driverId,
        fuelPrice: data.fuelPrice,
        numberOfDays: data.numberOfDays,
      },
      {
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
      }
      )
      
      
      const newTour:allTravelWarrants ={
        id: response.data.id,
        client: response.data.clientName,
        departure: response.data.departure,
        destination: response.data.destination,
        interdestination: response.data.intermediateDestinations,
        mileage: response.data.mileage,
        date: format(new Date(response.data.timeOfTour), "dd/MM/yyyy HH:mm")
      }
        return newTour

  
}

export const editWarrant =async (id:number,data:addTravelWarrant):Promise<allTravelWarrants> => {
    const utcDate = utcToZonedTime(data.timeOfTour, "UTC");
    const response= await axios.put(
      `https://localhost:7206/api/TravelWarrants/EditTour/${id}`,
      {
        clientId: data.clientId,
        departure: data.departure,
        destination: data.destination,
        intermediateDestinations: data.intermediateDestinations,
        mileage: data.mileage,
        numberOfPassengers: data.numberOfPassengers,
        price: data.price,
        toll: data.toll,
        fuel: data.fuel,
        timeOfTour: format(utcDate, "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"),
        vehicleId: data.vehicleId,
        startMileage: data.startMileage,
        endMileage: data.endMileage,
        note: data.note,
        driverId: data.driverId,
        fuelPrice: data.fuelPrice,
        numberOfDays: data.numberOfDays,
      },
      {
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
      }
      )
      
    
      const updatedTour:allTravelWarrants ={

        id: response.data.id,
        client: response.data.clientName,
        departure: response.data.departure,
        destination: response.data.destination,
        interdestination: response.data.intermediateDestinations,
        mileage: response.data.mileage,
        date: format(new Date(response.data.timeOfTour), "dd/MM/yyyy HH:mm")
      }
      
    return updatedTour  
}


//payments

export const getPayments =async ():Promise<allPayments[]> => {
  const response=await axios.get("https://localhost:7206/api/Payments/GetPayments")
  const jsonResponse = response.data.map((x:any) =>({
    id: x.id,
    client:x.clientName,
    date:format(new Date(x.date), "dd/MM/yyyy"),
    amount:x.amount,
    basis:x.note
  }))
  return jsonResponse
}

export const getPaymentsById =async (id:number):Promise<addPayment> => {
  const response=await axios.get(`https://localhost:7206/api/Payments/GetPayment/${id}`)
  const jsonResponse = response.data
  const date = new Date(jsonResponse.date);
  const formattedDate = format(date, "yyyy-MM-dd'T'HH:mm");
  const singlePayment:addPayment ={
    clientId:jsonResponse.clientId,
    date:formattedDate,
    amount:jsonResponse.amount,
    basis:jsonResponse.note
  }
  
  return singlePayment
}

export const addPayments =async (data:addPayment):Promise<allPayments> => {
  
  const utcDate = utcToZonedTime(data.date, "UTC");
  
   const response = await axios.post("https://localhost:7206/api/Payments/NewPayment",{
    clientId:data.clientId,
    date:format(utcDate, "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"),
    amount:data.amount,
    note:data.basis
  },
{
    headers:{
      "Content-Type":"application/json",
      Accept:"application/json"
    }
  })

  const newPayment:allPayments ={

    id: response.data.id,
    date: format(new Date(response.data.date), "dd/MM/yyyy"),
    client: response.data.clientName,
    amount: response.data.amount,
    basis: response.data.note
  } 

  return newPayment
}

export const editPayments =async (id:number,data:addPayment):Promise<allPayments> => {

const utcDate = utcToZonedTime(data.date, "UTC");

   const response = await axios.put(`https://localhost:7206/api/Payments/EditPayment/${id}`,{
    clientId:data.clientId,
    date:format(utcDate, "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"),
    amount:data.amount,
    note:data.basis
  },
{
    headers:{
      "Content-Type":"application/json",
      Accept:"application/json"
    }
  })
  const updatedPayment:allPayments ={
    id: response.data.id,
    date: format(new Date(response.data.date), "dd/MM/yyyy"),
    client: response.data.clientName,
    amount: response.data.amount,
    basis: response.data.note
  } 

  return updatedPayment
}

//searches

export const getSearches =async ():Promise<allPayments[]> => {
  const response=await axios.get("https://localhost:7206/api/Searches/GetSearches")
  const jsonResponse = response.data.map((x:any) =>({
    id: x.id,
    client:x.clientName,
    date:format(new Date(x.date), "dd/MM/yyyy"),
    amount:x.amount,
    basis:x.note
  }))
  return jsonResponse
}

export const getSearchById = async (id:number):Promise<addPayment> => {
  const response = await axios.get(`https://localhost:7206/api/Searches/GetSearch/${id}`)
  const jsonResponse = response.data
  const date = new Date(jsonResponse.date);
  const formattedDate = format(date, "yyyy-MM-dd'T'HH:mm");

  const singlePayment:addPayment ={
    clientId:jsonResponse.clientId,
    date:formattedDate,
    amount:jsonResponse.amount,
    basis:jsonResponse.note
  }
  
  return singlePayment
}

export const addSearches =async (data:addPayment):Promise<allPayments> => {
  const utcDate = utcToZonedTime(data.date, "UTC");
   const response = await axios.post("https://localhost:7206/api/Searches/NewSearch",{
    clientId:data.clientId,
    date:format(utcDate, "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"),
    amount:data.amount,
    note:data.basis
  },
{
    headers:{
      "Content-Type":"application/json",
      Accept:"application/json"
    }
  })

  const newSearch:allPayments ={
    id: response.data.id,
    date: format(new Date(response.data.date), "dd/MM/yyyy"),
    client: response.data.clientName,
    amount: response.data.amount,
    basis: response.data.note
  } 
  return newSearch
}

export const editSearches =async (id:number,data:addPayment):Promise<allPayments> => {
  const utcDate = utcToZonedTime(data.date, "UTC");
   const response = await axios.put(`https://localhost:7206/api/Searches/EditSearch/${id}`,{
    clientId:data.clientId,
    date:format(utcDate, "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"),
    amount:data.amount,
    note:data.basis
  },
{
    headers:{
      "Content-Type":"application/json",
      Accept:"application/json"
    }
  })

  const updatedSearch:allPayments ={
    id: response.data.id,
    date: format(new Date(response.data.date), "dd/MM/yyyy"),
    client: response.data.clientName,
    amount: response.data.amount,
    basis: response.data.note
  } 
  
  return updatedSearch

}

//reports

export const getReportsPerClient =async (id:number):Promise<IReports[]> => {
   const response = await axios.get(`https://localhost:7206/api/TravelWarrantsReports/GetForClients/${id}`)
   const jsonResponse = await response.data.map((x:any) =>({
    date: format(new Date(x.dateAndTime), "dd/MM/yyyy HH:mm "),
    client: x.clientName,
    destination: x.destination,
    departure: x.departure,
    intermediateDestinations:x.intermediateDestinations,
    kilometers: x.mileage
   }))

   return jsonResponse
}

export const getReportsPerVehicle =async (id:number):Promise<IReports[]> => {
  const response = await axios.get(`https://localhost:7206/api/TravelWarrantsReports/GetForVehicles/${id}`)
  const jsonResponse = await response.data.map((x:any) =>({
   date: format(new Date(x.dateAndTime), "dd/MM/yyyy HH:mm "),
   client: x.clientName,
   destination: x.destination,
   departure: x.departure,
   intermediateDestinations:x.intermediateDestinations,
   kilometers: x.mileage
  }))

  
  return jsonResponse
}


export const getReportsPerDriver =async (id:number):Promise<IReports[]> => {
  const response = await axios.get(`https://localhost:7206/api/TravelWarrantsReports/GetForDrivers/${id}`)
  const jsonResponse = await response.data.map((x:any) =>({
   date: format(new Date(x.dateAndTime), "dd/MM/yyyy HH:mm "),
   client: x.clientName,
   destination: x.destination,
   departure: x.departure,
   intermediateDestinations:x.intermediateDestinations,
   kilometers: x.mileage
  }))

  return jsonResponse
}


export const getReportsPerPeriod = async (from: Date, to: Date): Promise<IReports[]> => {
    
    
    const response = await axios.get(`https://localhost:7206/api/TravelWarrantsReports/GetForPeriod`, {
      params: {
        from: from.toISOString(),
        to: to.toISOString(),
      },
    });

    const jsonResponse = response.data.map((x: any) => ({
      
      date: format(new Date(x.dateAndTime), "dd/MM/yyyy HH:mm "),
      client: x.clientName,
      destination: x.destination,
      departure: x.departure,
      intermediateDestinations: x.intermediateDestinations,
      kilometers: x.mileage,
    }));

    
    return jsonResponse;
  

};

export const  getReportsForDestination = async (dest:string):Promise<IReports[]> => {
  const response = await axios.get("https://localhost:7206/api/TravelWarrantsReports/GetForDestination",{
    params:{
      destination:dest
    }
  })
  const jsonResponse = response.data.map((x: any) => ({
    date: format(new Date(x.dateAndTime), "dd/MM/yyyy HH:mm "),
    client: x.clientName,
    destination: x.destination,
    departure: x.departure,
    intermediateDestinations: x.intermediateDestinations,
    kilometers: x.mileage,
  }));

  return jsonResponse;
}

export const getInoviceForClients =async (id:number,page?: number | null):Promise<Invoices[]> => {
  const response  = await axios.get("https://localhost:7206/api/TravelWarrantsReports/InoviceReportsForClient",
  {
    params:{
      clientId:id,
      page:page
    }
  })

  const jsonResponse = response.data.map((x:any)=>({
    year: x.year,
    number:x.number,
    amount:x.amount,
    clientName:x.clientName,
    id:x.id,
    date: format(new Date(x.date), "dd/MM/yyyy"),

 }))

 return jsonResponse
}
export const getInovicesForPeriod =async (from:Date,to:Date,page?:number):Promise<Invoices[]> => {
  const response = await axios.get(`https://localhost:7206/api/TravelWarrantsReports/InoviceReportsForPeriod`, {
      params: {
        from: from.toISOString(),
        to: to.toISOString(),
        page:page
      },
    });
    const jsonResponse = response.data.map((x:any)=>({
      year: x.year,
      number:x.number,
      amount:x.amount,
      clientName:x.clientName,
      id:x.id,
      date: format(new Date(x.date), "dd/MM/yyyy"),
  
   }))
  
   return jsonResponse
}

export const getInovicesByDescription =async (desc:string,page:number):Promise<Invoices[]> => {
  const response = await axios.get("https://localhost:7206/api/TravelWarrantsReports/InoviceReportsForDescription",{
    params:{
      description:desc,
      page:page
    }
  })
  const jsonResponse = response.data.map((x:any)=>({
    year: x.year,
    number:x.number,
    amount:x.amount,
    clientName:x.clientName,
    id:x.id,
    date: format(new Date(x.date), "dd/MM/yyyy"),

 }))

 return jsonResponse
}
export const getProformaInvoiceForClients =async (id:number,page?: number | null):Promise<Invoices[]> => {
  const response  = await axios.get("https://localhost:7206/api/TravelWarrantsReports/ProformaInvoiceReportsForClient",
  {
    params:{
      clientId:id,
      page:page
    }
  })

  const jsonResponse = response.data.map((x:any)=>({
    year: x.year,
    number:x.number,
    amount:x.amount,
    clientName:x.clientName,
    id:x.id,
    date: format(new Date(x.date), "dd/MM/yyyy"),

 }))

 return jsonResponse
}
export const getProformaInvoiceForPeriod =async (from:Date,to:Date,page?:number):Promise<Invoices[]> => {
  const response = await axios.get(`https://localhost:7206/api/TravelWarrantsReports/ProformaInvoiceReportsForPeriod`, {
      params: {
        from: from.toISOString(),
        to: to.toISOString(),
        page:page
      },
    });
    const jsonResponse = response.data.map((x:any)=>({
      year: x.year,
      number:x.number,
      amount:x.amount,
      clientName:x.clientName,
      id:x.id,
      date: format(new Date(x.date), "dd/MM/yyyy"),
  
   }))
  
   return jsonResponse
}
export const getProformaInvoiceByDescription =async (desc:string,page:number):Promise<Invoices[]> => {
  const response = await axios.get("https://localhost:7206/api/TravelWarrantsReports/ProformaInvoiceReportsForDescription",{
    params:{
      description:desc,
      page:page
    }
  })
  const jsonResponse = response.data.map((x:any)=>({
    year: x.year,
    number:x.number,
    amount:x.amount,
    clientName:x.clientName,
    id:x.id,
    date: format(new Date(x.date), "dd/MM/yyyy"),

 }))

 return jsonResponse
}
export const excursion = async (onOff: boolean): Promise<void> => {
  try {
    
    await axios.post(`https://localhost:7206/api/TravelWarrantsReports/Excursion?excursionOnOff=${onOff}`,
    {
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json"
      }
    });
    
  } catch (error) {
    console.error("Došlo je do greške prilikom slanja zahteva:", error);
  }
};




export const getReportsDepDest = async (departure: string, destination: string): Promise<IReports[]> => {
  
  const response = await axios.get(`https://localhost:7206/api/TravelWarrantsReports/GetForDepAndDes`, {
    params: {
      departure: departure,
      destination: destination,
    },
  });

  const jsonResponse = response.data.map((x: any) => ({
    date: format(new Date(x.dateAndTime), "dd/MM/yyyy HH:mm "),
    client: x.clientName,
    destination: x.destination,
    departure: x.departure,
    intermediateDestinations: x.intermediateDestinations,
    kilometers: x.mileage,
  }));

  return jsonResponse;


};
//Status

export const getStatuses =async ():Promise<Statuses[]> => {
  const response = await axios.get("https://localhost:7206/api/Statuses/GetStatuses")
  const jsonResponse = response.data.map((x:any) => ({
    id:x.id,
    client:x.client,
    search:x.search,
    deposit: x.deposit,
    balance: x.balance,
    clientId: x.clientId
  }))
  
  return jsonResponse
}

//Invoice

export const getInvoices =async (page?: number | null):Promise<Invoices[]> => {
   const response = await axios.get("https://localhost:7206/api/Invoices/Get",{
    params:{
      pageNumber:page
    }
   })

   const jsonResponse = response.data.map((x:any)=>({
      year: x.year,
      number:x.number,
      amount:x.amount,
      clientName:x.clientName,
      id:x.id,
      date: format(new Date(x.date), "dd/MM/yyyy"),

   }))

   return jsonResponse
}

export const newInvoice =async (data:IAddInvoice):Promise<Invoices> => {
  const utcDate = utcToZonedTime(data.date, "UTC");
  const response = await axios.post("https://localhost:7206/api/Invoices/NewInvoice",
  {
    clientId:data.clientId,
    documentDate:format(utcDate, "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"),
    paymentDeadline:data.paymentDeadline,
    priceWithoutVAT:data.priceWithoutVat,
    note:data.note,
    itemsOnInovice:data.itemsOnInovice
  },
  {
    headers:{
      "Content-Type":"application/json",
      Accept:"application/json"
    }
  })
console.log(response.data)
 const inoviceData:Invoices ={
  id:response.data.id,
  year:response.data.year,
  number:response.data.number,
  date:format(new Date(response.data.date), "dd/MM/yyyy"),
  amount:response.data.amount,
  clientName:response.data.clientName
 } 
 
 return inoviceData
}

export const invoiceToDelete =async (id:number):Promise<IDeleteInvoice> => {
  const response = await axios.get(`https://localhost:7206/api/Invoices/ToDelete/${id}`)
  const jsonResponse = response.data

  const inoviceToDelete:IDeleteInvoice={
    number:jsonResponse.number,
    clientName: jsonResponse.clientName,
    date:jsonResponse.date,
    amount:jsonResponse.amount
  }

  return inoviceToDelete
}

export const getInvoiceById =async (id:number):Promise<IGetInvoiceById> => {
  var response = await axios.get(`https://localhost:7206/api/Invoices/GetById/${id}`)
  const jsonResponse = response.data
  //console.log("Sa servera:" , jsonResponse);
  const date = new Date(jsonResponse.documentDate);
  const formattedDate = format(date, "yyyy-MM-dd'T'HH:mm");
  const singleInovice:IGetInvoiceById={
    
    //clientName: jsonResponse.clientName,
    date:formattedDate,
    clientId:jsonResponse.clientId,
    paymentDeadline:jsonResponse.paymentDeadline,
    note:jsonResponse.note,
    number:jsonResponse.number,
    priceWithoutVat:jsonResponse.priceWithoutVAT,
    itemsOnInovice: jsonResponse.itemsOnInovice.map((i:any) => {
      return{
        id: i.id,
        description:i.description,
        serviceId: i.serviceId,
        quantity: i.quantity,
        price: i.price,
        numberOfDays: i.numberOfDays
      } as IItemsEdit
    })
   
  }
  //console.log("Mapirani:" , singleInovice);
  return singleInovice
}

export const editInvoice =async (id:number,data:IEditInvoice):Promise<Invoices> => {
  const utcDate = utcToZonedTime(data.date, "UTC");
  const response = await axios.put(`https://localhost:7206/api/Invoices/EditInovice/${id}`,
  {
    clientId:data.clientId,
    documentDate:format(utcDate, "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"),
    paymentDeadline:data.paymentDeadline,
    priceWithoutVAT:data.priceWithoutVat,
    note:data.note,
    itemsOnInovice:data.itemsOnInovice,
    itemsToDeleteId: data.itemsToDelete
  },
  {
    headers:{
      "Content-Type":"application/json",
      Accept:"application/json"
    }
  })

const inoviceData:Invoices ={
  id:response.data.id,
  year:response.data.year,
  number:response.data.number,
  date:format(new Date(response.data.date), "dd/MM/yyyy"),
  amount:response.data.amount,
  clientName:response.data.clientName
 } 
 
 return inoviceData
 
}
export const downloadPdf = async (id: number, invoiceNumber: string) => {
  try {
    const response = await axios.get(`https://localhost:7206/api/Invoices/GeneratePdf/${id}`, {
      responseType: 'blob',
    });

    const blob = new Blob([response.data], { type: 'application/pdf' });
    const downloadUrl = URL.createObjectURL(blob);

    const link = document.createElement('a');
    link.href = downloadUrl;
    link.download = `Faktura_${invoiceNumber}.pdf`; 
    
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    URL.revokeObjectURL(downloadUrl);

    return true;
  } catch (error) {
    console.error('Došlo je do greške prilikom preuzimanja PDF-a', error);
    return false;
  }
};


//ProformaInvoices

export const uploadFileBuffering =async (file:File):Promise<number> => {
  if (!file) {
    throw new Error('Fajl je obavezan.');
  }

      const formData = new FormData();
      formData.append('file', file);  

      try {
        
        const response = await axios.post('https://localhost:7206/api/UploadFiles/buffering-upload', formData, {
            headers: {
                'Content-Type': 'multipart/form-data',
            },
        });

        
        const data = response.data;

        return data;  
        
    } catch (error) {
        console.error('Greska prilikom upload-a: ', error);
        throw error;  
    }
}

export const uploadfileStreaming = async (file: File): Promise<number> => {
  if (!file) {
    throw new Error('Fajl je obavezan.');
  }

  // Kreiranje CancelToken-a unutar funkcije
  const CancelToken = axios.CancelToken;
  const source = CancelToken.source();

  const formData = new FormData();
  formData.append('file', file);

  try {
    const response = await axios.post(
      'https://localhost:7206/api/UploadFiles/streaming-upload',
      formData,
      {
        headers: {
          'Content-Type': 'multipart/form-data',
        },
        cancelToken: source.token,  // Dodajte cancelToken ovde
      }
    );

    const data = response.data;
    return data;

  } catch (error) {
    if (axios.isCancel(error)) {
      console.log('Zahtev otkazan:', error.message);
    } else {
      console.error('Greška prilikom upload-a:', error);
    }
    throw error;
  }

  // Otkazivanje zahteva može se pozvati ovako
  // source.cancel('Operacija otkazana od strane korisnika.');
}
export const editProformaInvoice = async (id: number, data: IEditProformaInvoice): Promise<IProformaInvoices> => {
  try {
    const utcDate = utcToZonedTime(data.date, "UTC");
    const response = await axios.put(
      `https://localhost:7206/api/ProformaInvoice/EditProformaInvoice/${id}`,
      {
        clientId: data.clientId,
        documentDate: format(utcDate, "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"),
        paymentDeadline: data.paymentDeadline,
        priceWithoutVAT: data.priceWithoutVat,
        note: data.note,
        offerAccepted: data.offerAccepted,
        proinoviceWithoutVAT: data.proformaWithoutVat,
        itemsOnInovice: data.itemsOnInovice,
        itemsToDeleteId: data.itemsToDelete,
        routePlan: data.file 
      },
      {
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json"
        }
      }
    );

    const invoiceData: IProformaInvoices = {
      id: response.data.id,
      number: response.data.number,
      date: format(new Date(response.data.date), "dd/MM/yyyy"),
      amount: response.data.amount,
      clientName: response.data.clientName,
      accepted: response.data.offerAccepted
    };

    return invoiceData;
  } catch (error) {
    console.error('Error:', error);  
    throw error;
  }
};


export const newProformaInvoice = async (data: IAddProformaInvoice): Promise<IProformaInvoices> => {
  try {
    const utcDate = utcToZonedTime(data.date, "UTC");
    const response = await axios.post(
      "https://localhost:7206/api/ProformaInvoice/NewProformaInvoice",
      {
        clientId: data.clientId,
        documentDate: format(utcDate, "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"),
        paymentDeadline: data.paymentDeadline,
        priceWithoutVAT: data.priceWithoutVat,
        note: data.note,
        itemsOnInovice: data.itemsOnInovice,
        proinoviceWithoutVAT: data.proinvoiceWithoutVat,
        routePlan:data.file
      },
      {
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json"
        }
      }
    );
    
    const inoviceData: IProformaInvoices = {
      id: response.data.id,
      accepted: response.data.offerAccepted,
      number: response.data.number,
      date: format(new Date(response.data.date), "dd/MM/yyyy"),
      amount: response.data.amount,
      clientName: response.data.clientName
    };
    return inoviceData;
  } catch (error) {
    console.error('Error:', error);  
    throw error
  }
};

export const allProformaInvoices =async (page?:number):Promise<IProformaInvoices[]> => {
  const response = await axios.get("https://localhost:7206/api/ProformaInvoice/GetProformaInvoice",{
    params:{
      page:page
    }
   })
  const jsonResponse:IProformaInvoices[] = response.data.map((x:any)=>({
    
    number:x.number,
    amount:x.amount,
    clientName:x.clientName,
    id:x.id,
    accepted:x.offerAccepted,
    date: format(new Date(x.date), "dd/MM/yyyy"),

 }))

 
 return jsonResponse
}

export const downloadPdfProformaInvoice = async (id: number, invoiceNumber: string) => {
  try {
    const response = await axios.get(`https://localhost:7206/api/ProformaInvoice/GeneratePdf/${id}`, {
      responseType: 'blob',
    });

    const blob = new Blob([response.data], { type: 'application/pdf' });
    const downloadUrl = URL.createObjectURL(blob);

    const link = document.createElement('a');
    link.href = downloadUrl;
    link.download = `Profaktura_${invoiceNumber}.pdf`; 
    
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    URL.revokeObjectURL(downloadUrl);

    return true;
  } catch (error) {
    console.error('Došlo je do greške prilikom preuzimanja PDF-a', error);
    return false;
  }
};

export const proformaInvoiceToDelete =async (id:number):Promise<IDeleteInvoice> => {
  const response = await axios.get(`https://localhost:7206/api/ProformaInvoice/GetProformaInvoiceForDelete/${id}`)
  const jsonResponse = response.data

  const proformaInvoiceToDelete:IDeleteInvoice={
    number:jsonResponse.number,
    clientName: jsonResponse.clientName,
    date:jsonResponse.date,
    amount:jsonResponse.amount
  }

  return proformaInvoiceToDelete
}

export const getProformaInvoiceById =async (id:number):Promise<IGetProformaInvoiceById> => {
  var response = await axios.get(`https://localhost:7206/api/ProformaInvoice/GetProformaInvoiceById/${id}`)
  const jsonResponse = response.data
  
  const date = new Date(jsonResponse.documentDate);
  const formattedDate = format(date, "yyyy-MM-dd'T'HH:mm");

  const singleProformaInvoice:IGetProformaInvoiceById={
    date:formattedDate,
    clientId:jsonResponse.clientId,
    paymentDeadline:jsonResponse.paymentDeadline,
    note:jsonResponse.note,
    number:jsonResponse.number,
    priceWithoutVat:jsonResponse.priceWithoutVAT,
    offerAccepted:jsonResponse.offerAccepted,
    proformaWithoutVat:jsonResponse.proinvoiceWithoutVAT,
    fileName:jsonResponse.fileName,
    fileId:jsonResponse.fileId,
    itemsOnInovice: jsonResponse.itemsOnInovice.map((i:any) => {
      return{
        id: i.id,
        description:i.description,
        serviceId: i.serviceId,
        quantity: i.quantity,
        price: i.price,
        numberOfDays: i.numberOfDays
      } as IItemsEdit
    })
   
  }
  
  return singleProformaInvoice
}

export const downloadRoutePlan = async (id: number) => {
  try {
    var response = await axios.get(`https://localhost:7206/api/ProformaInvoice/DownloadRoutePlan/${id}`, {
        responseType: 'blob',
        headers: {
            'Accept': 'application/octet-stream'
        }
    });

    if (response.status === 200 && response.headers) {
      const headerName = Object.keys(response.headers).find(key => key.toLowerCase() === 'content-disposition');
      const contentDisposition = headerName ? response.headers[headerName] : '';
        var fileName = 'route-plan-file';
        if (contentDisposition) {
          //mora ovaj komplikovaniji regex
          const fileNameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
            const fileNameMatch = contentDisposition.match(fileNameRegex);
            if (fileNameMatch && fileNameMatch[1]) {
                fileName = fileNameMatch[1].replace(/['"]/g, '');
            }

        }

        var url = window.URL.createObjectURL(new Blob([response.data]));
        var link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', fileName);
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    } else {
        console.error(`Failed to download file: ${response.statusText}`);
    }
} catch (error) {
    console.error(`Error: ${error}`);
}
}





  export const deleteRoutePlan = async (id: number) => {
    try {
        var response = await axios.delete(`https://localhost:7206/api/ProformaInvoice/DeleteRoutePlan/${id}`);

        if (response.status === 200) {
            console.log('Route plan deleted successfully');
            return response.data;
        } else {
            console.error(`Failed to delete route plan: ${response.statusText}`);
            return null;
        }
    } catch (error) {
        console.error(`Error: ${error}`);
        return null;
    }
}

