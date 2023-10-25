using TravelWarrants.DTOs;
using TravelWarrants.DTOs.Inovices;
using TravelWarrants.DTOs.Proinovce;

namespace TravelWarrants.Interfaces
{
    public interface IProInoviceService
    {
        Task<bool> DeleteProInovice(int inoviceId);
        Task<bool> DeleteRoutePlan(int invoiceId);
        Task<ResponseDTO<ProinvoiceGetDTO>> EditProinvoice(int invoiceId, ProinvoiceEditDTO proinvoiceEditDTO);
        Task<(byte[], string)> GeneratePdf(int id);
        Task<ResponseDTO<InvoiceGetByIdDeleteDTO>> GetForDeleteProinvoice(int inoviceId);
        Task<ResponseDTO<IEnumerable<ProinvoiceGetDTO>>> GetProformaInvoices(int? page);
        Task<ResponseDTO<ProinvoiceGetByIdDTO>> GetProformaInvoiceById(int invoiceId);
        Task<FileData> GetRoutePlanFile(int invoiceId);
        Task<ResponseDTO<ProinvoiceGetDTO>> NewProinvoice(ProinvoiceNewDTO proinvoiceNewDTO);
    }
}
