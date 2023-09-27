using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Inovices;

namespace TravelWarrants.Interfaces
{
    public interface IInovicesService
    {
        Task<ResponseDTO<bool>> DeleteInovice(int inoviceId);
        Task<ResponseDTO<InoviceGetDTO>> EditInvoice(int invoiceId, InoviceEditDTO inoviceEditDTO);
        Task<ResponseDTO<InoviceGetByIdDTO>> GetById(int inoviceId);
        Task<ResponseDTO<InoviceGetByIdDeleteDTO>> GetForDelete(int inoviceId);
        Task<ResponseDTO<IEnumerable<InoviceGetDTO>>> GetInovices(int? page);
        Task<ResponseDTO<InoviceGetDTO>> NewInovice(InoviceNewDTO inoviceSaveDTO);
        Task<byte[]> GeneratePdf(int id);
    }
}
