using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Models;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IDocumentServices
    {
        // العمليات الأساسية
        Task<OfficialDocumentDto> GetDocumentByIdAsync(int id);
        Task<List<OfficialDocumentDto>> GetAllDocumentsAsync();
        Task<PaginatedResult<OfficialDocumentDto>> GetDocumentsPagedAsync(int page, int pageSize, string searchTerm, string documentType, DocumentStatus status);
        Task<OfficialDocumentDto> CreateDocumentAsync(OfficialDocumentDto documentDto);
        Task UpdateDocumentAsync(OfficialDocumentDto documentDto);
        Task DeleteDocumentAsync(int id);
        Task<bool> DocumentExistsAsync(int id);

        // التوقيع الإلكتروني
        Task<bool> SignDocumentAsync(int documentId, string signatureData, string signerName, string signerPosition);
        Task<List<DocumentSignatureDto>> GetDocumentSignaturesAsync(int documentId);

        // الإحصائيات
        Task<int> GetTotalDocumentsCountAsync();
        Task<int> GetIssuedDocumentsCountAsync();
        Task<int> GetDraftDocumentsCountAsync();
        Task<int> GetSignedDocumentsCountAsync();

        // الفلترة والحالة
        Task<List<OfficialDocumentDto>> GetDocumentsByStatusAsync(string status);
        Task<List<OfficialDocumentDto>> GetDocumentsByTypeAsync(string documentType);
        Task<bool> IssueDocumentAsync(int documentId);
        Task<List<OfficialDocumentDto>> GetDocumentsByDateRangeAsync(DateTime startDate, DateTime endDate);

        // التصدير والطباعة
        Task<string> GenerateDocumentPdfAsync(int documentId);
        Task<byte[]> ExportDocumentsToExcelAsync(List<OfficialDocumentDto> documents);

        // قوالب الوثائق
        Task<List<DocumentTemplateDto>> GetAllTemplatesAsync();
        Task<DocumentTemplateDto> GetTemplateByIdAsync(int templateId);
        Task<DocumentTemplateDto> CreateTemplateAsync(DocumentTemplateDto templateDto); 
        Task UpdateTemplateAsync(DocumentTemplateDto templateDto);
        Task DeleteTemplateAsync(int templateId);
    }
}

