﻿namespace TravelWarrants.Models
{
    public class CompanyBankAccount
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Bank { get; set; }
        public string AccountNumber { get; set; }
        public virtual Company Company { get; set; }
    }
}
