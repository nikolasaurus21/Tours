﻿namespace TravelWarrants.Models
{
    public class ProformaInvoice
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Year { get; set; }
        public int ClientId { get; set; }
        public decimal NoVAT { get; set; }
        public decimal VAT { get; set; }
        public decimal Total { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime CurrencyDate { get; set; }
        public string Note { get; set; }
        public bool? PriceWithoutVAT { get; set; }
        public bool? ProinoviceWithoutVAT { get; set; }
        
        public bool? OfferAccepted { get; set; }
        public  Client Client { get; set; }
        public  ICollection<InoviceService> InoviceService { get; set; }
        public  ICollection<Account> Account { get; set; }
        public int? UploadedFileId { get; set; }
        public UploadedFiles UploadedFiles { get; set; }
        
    }
}
