using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Inovices;

namespace TravelWarrants.Interfaces
{
    public interface IInovicesService
    {
        Task<ResponseDTO<bool>> DeleteInovice(int inoviceId);
        Task<ResponseDTO<InoviceGetDTO>> EditInvoice(int invoiceId, InoviceEditDTO inoviceEditDTO);
        Task<ResponseDTO<IEnumerable<InoviceGetDTO>>> GetInovices(int? page);
        Task<ResponseDTO<InoviceGetDTO>> NewInovice(InoviceNewDTO inoviceSaveDTO);
    }
}
