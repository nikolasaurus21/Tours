﻿namespace TravelWarrants.DTOs.Inovices
{
    public class InvoicePdf
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Service { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPlace { get; set; }
        public string Email { get; set; }
        public string ClientPTT { get; set; }
        public List<ItemsOnInvoicePdf> ItemsOnInovice { get; set; } = new List<ItemsOnInvoicePdf>();
        public decimal Total { get; set; }
        public decimal PriceWithoutVat { get; set; }
        public decimal Vat { get; set; }



    }
}
