﻿using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Reports;

namespace TravelWarrants.Interfaces
{
    public interface IReportsService
    {
        Task<ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>>> GetForClients(int id);
        Task<ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>>> GetForDestination(string destination);

        Task<ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>>> GetForDepAndDes(string departure, string destination);
        Task<ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>>> GetForVehicles(int id);
        Task<ResponseDTO<IEnumerable<TravelWarrantsReportsDTO>>> GetForDrivers(int id);
        Task<ResponseDTO<IEnumerable<TravelWarrantReportsPeriod>>> GetForPeriod(DateTime from, DateTime to);
        bool Excursion(bool? excursionOnOff);
    }
}
