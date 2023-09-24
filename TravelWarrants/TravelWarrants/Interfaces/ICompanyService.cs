using Microsoft.AspNetCore.Mvc;
using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Company;

namespace TravelWarrants.Interfaces
{
    public interface ICompanyService
    {
        Task<ResponseDTO<IEnumerable<CompanyDTO>>> Get();
        Task NewCompany(CompanyDTOSave companyDTO);
    }
}
