using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Inovices;

namespace TravelWarrants.Interfaces
{
    public interface IInovicesService
    {
        Task<ResponseDTO<bool>> DeleteInovice(int invoiceId);
        Task<ResponseDTO<InvoiceGetDTO>> EditInvoice(int invoiceId, InvoiceEditDTO invoiceEditDTO);
        Task<ResponseDTO<InvoiceGetByIdDTO>> GetById(int inoviceId);
        Task<ResponseDTO<InvoiceGetByIdDeleteDTO>> GetForDelete(int inoviceId);
        Task<ResponseDTO<IEnumerable<InvoiceGetDTO>>> GetInovices(int? page);
        Task<ResponseDTO<InvoiceGetDTO>> NewInovice(InvoiceNewDTO invoiceSaveDTO);
        Task<(byte[], string)> GeneratePdf(int id);
    }
}
