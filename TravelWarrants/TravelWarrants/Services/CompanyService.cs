using Microsoft.EntityFrameworkCore;
using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Company;
using TravelWarrants.Interfaces;

namespace TravelWarrants.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly TravelWarrantsContext _context;
        public CompanyService(TravelWarrantsContext context)
        {
            _context = context;
        }
        public async Task<ResponseDTO<IEnumerable<CompanyDTO>>> Get()
        {
            var company = await _context.Companies.Select(x => new CompanyDTO
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Address = x.Address,
                PTT = x.PTT,
                Place = x.Place,
                Telephone = x.Telephone,
                Fax = x.Fax,
                MobilePhone = x.MobilePhone,
                TIN = x.TIN,
                VAT = x.VAT
            }).ToListAsync();

            return new ResponseDTO<IEnumerable<CompanyDTO>> { Message = company, IsSucced = true };
        }

        public async Task NewCompany(CompanyDTOSave companyDTO)
        {
            var companyDb = await _context.Companies.FirstOrDefaultAsync();

            if (companyDb == null)
            {

                var newcompany = new Company
                {

                    Name = companyDTO.Name,
                    Description = companyDTO.Description,
                    Address = companyDTO.Address,
                    PTT = companyDTO.PTT,
                    Place = companyDTO.Place,
                    Telephone = companyDTO.Telephone,
                    Fax = companyDTO.Fax,
                    MobilePhone = companyDTO.MobilePhone,
                    TIN = companyDTO.TIN,
                    VAT = companyDTO.VAT
                };
                _context.Companies.Add(newcompany);
            }
            else
            {
                companyDb.Name = companyDTO.Name;
                companyDb.Description = companyDTO.Description;
                companyDb.Address = companyDTO.Address;
                companyDb.PTT = companyDTO.PTT;
                companyDb.Place = companyDTO.Place;
                companyDb.Telephone = companyDTO.Telephone;
                companyDb.Fax = companyDTO.Fax;
                companyDb.MobilePhone = companyDTO.MobilePhone;
                companyDb.TIN = companyDTO.TIN;
                companyDb.VAT = companyDTO.VAT;

            }



            await _context.SaveChangesAsync();


            
        }
    }
}
