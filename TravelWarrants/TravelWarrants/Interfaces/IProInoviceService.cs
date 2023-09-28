using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Proinovce;

namespace TravelWarrants.Interfaces
{
    public interface IProInoviceService
    {
        Task<ResponseDTO<ProinvoiceGetDTO>> NewProinvoice(ProinvoiceNewDTO proinvoiceNewDTO);
    }
}
