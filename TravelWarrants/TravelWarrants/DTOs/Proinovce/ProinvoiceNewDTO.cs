﻿using TravelWarrants.DTOs.Inovices;

namespace TravelWarrants.DTOs.Proinovce
{
    public class ProinvoiceNewDTO
    {
        public int ClientId { get; set; }
        public int PaymentDeadline { get; set; }
        public string Note { get; set; }
        public DateTime DocumentDate { get; set; }

        public List<ItemsOnInvoice> ItemsOnInovice { get; set; } = new List<ItemsOnInvoice>();

        public bool PriceWithoutVAT { get; set; } = false;
        public bool? ProinoviceWithoutVAT { get; set; }

        public int? RoutePlan { get; set; }

    }
}
