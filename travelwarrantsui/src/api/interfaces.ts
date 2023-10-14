export interface allClients{
    name: string,
    address:string,
    place:string
    id:number
}

export interface addClient {
    name: string;
    address: string;
    place: string;
    tinurn: string;
    fax: string;
    phone: string;
    email: string;
    vat: number;
    excursion: boolean;
  }

 export interface allVehicles{
    id:number
    registration:string
    name:string
    note:string
    numberOfSeats:string
    fuel:string
    mileage:number
 }

 export interface addVehicle{
    registration:string
    name:string
    note:string
    numberOfSeats:string
    fuel:string
    mileage:number
 }

 export interface allDrivers{
   id: number
   name:string
 }
 export interface addDriver{
   name:string
   phone: string
 }

 export interface ICompany {
  name: string;
  description: string;
  address: string;
  ptt: string;
  place: string;
  telephone: string;
  fax: string;
  mobilePhone: string;
  tin: string;
  vat: string;
 }

 export interface allGiroAccounts{
  id:number
  bank:string
  account:string
 }

 export interface addGiroAccounts{
  
  bank:string
  account:string
 }

 export interface allServices{
  id:number
  description:string
  vat:number
 }

 export interface addService{
  name:string
  vat:number
 }

 export interface allTravelWarrants{
  id: number;
  client: string;
  departure: string;
  destination: string;
  interdestination: string;
  mileage: number;
  date: string;
 }

 export interface addTravelWarrant{
  departure: string;
  destination: string;
  mileage: number;
  numberOfPassengers: number;
  price: number;
  toll: number;
  fuel: number;
  timeOfTour: string;
  startMileage: number;
  endMileage: number;
  note: string;
  intermediateDestinations: string;
  driverId: number;
  vehicleId: number;
  clientId: number;
  fuelPrice: number;
  numberOfDays: number;
 }

export interface ListItem {
  id: number;
  name: string;
}
export interface ListItemVehicle {
  id: number;
  name: string;
  mileage:number
}

export interface allPayments{
  id: number;
  date: string;
  client: string;
  amount: number;
  basis: string;
}
export interface addPayment{
  clientId: number;
  date: string;
  amount: number;
  basis: string;
}

 export interface IReports {
  date: string;
  client: string;
  destination: string;
  departure: string;
  intermediateDestinations: string;
  kilometers: number;
}

export interface Statuses{
  id:number
  client:string;
  search:number
  deposit:number
  balance : number
  clientId:number
}

export interface deleteTour{
  datetime:string
  client:string
  departure:string
  destination:string
  registration:string
  driver:string
  mileage:number
}

export interface Inovices{
  id:number,
  number:number,
  year:number,
  amount:number,
  clientName:number
  date:string
}

export interface IAddItem{
  
  description?:string
  serviceId:number
  quantity:number
  price:number
  numberOfDays:string

}
export interface IAddInovice{
  clientId: number
  paymentDeadline:number
  note:string
  date:string
  priceWithoutVat:boolean
  itemsOnInovice:IAddItem[]
}

export interface IDeleteInovice{
  number:string
  date:string
  clientName:string
  amount:number
}

export interface IItemsEdit{
  id?:number
  description?:string
  serviceId:number
  quantity:number
  price:number
  numberOfDays:string
}

export interface IEditInovice{
  clientId: number
  paymentDeadline:number
  note:string
  date:string
  priceWithoutVat:boolean
  itemsOnInovice:IItemsEdit[]
  itemsToDelete?: number[]
}

export interface IGetInoviceById{
  clientId: number
  //clientName:string
  paymentDeadline:number
  note:string
  date:string
  priceWithoutVat:boolean
  number:string
  itemsOnInovice:IItemsEdit[]
}

export interface IProformaInvoices{
  id:number,
  number:string,
  amount:number,
  clientName:number
  date:string
  accepted:boolean
}

export interface IAddProformaInvoice{
  clientId: number
  paymentDeadline:number
  note:string
  date:string
  priceWithoutVat:boolean
  itemsOnInovice:IAddItem[]
  proinvoiceWithoutVat:boolean
  file: number | null;
}

export interface IGetProformaInvoiceById{
  clientId: number
  paymentDeadline:number
  note:string
  date:string
  priceWithoutVat:boolean
  number:string
  itemsOnInovice:IItemsEdit[]
  offerAccepted:boolean
  proformaWithoutVat:boolean
  fileName:string
}

export interface IEditProformaInvoice{
  clientId: number
  paymentDeadline:number
  note:string
  date:string
  priceWithoutVat:boolean
  itemsOnInovice:IItemsEdit[]
  itemsToDelete?: number[]
  offerAccepted:boolean
  proformaWithoutVat:boolean
  file?: File | null;
}