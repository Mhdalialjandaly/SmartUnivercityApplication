using UniversityManagementSystem.Application.DTOs;

namespace UniversityManagementSystem.Application.Interfaces
{
    public interface IDocumentService
    {
        // Document methods
        Task<List<OfficialDocumentDto>> GetAllDocumentsAsync();
        Task<List<OfficialDocumentDto>> GetDocumentsByTypeAsync(string documentType);
        Task<OfficialDocumentDto> GetDocumentByIdAsync(int id);
        Task<OfficialDocumentDto> CreateDocumentAsync(OfficialDocumentDto documentDto);
        Task UpdateDocumentAsync(int id, OfficialDocumentDto documentDto);
        Task DeleteDocumentAsync(int id);
        Task<bool> DocumentExistsAsync(int id);

        // Template methods
        Task<List<DocumentTemplateDto>> GetAllTemplatesAsync();
        Task<DocumentTemplateDto> GetTemplateByIdAsync(int id);
        Task<DocumentTemplateDto> CreateTemplateAsync(DocumentTemplateDto templateDto);
        Task<DocumentTemplateDto> UpdateTemplateAsync(int id, DocumentTemplateDto templateDto);
        Task DeleteTemplateAsync(int id);
        Task<bool> TemplateExistsAsync(int id);

        // Business logic methods
        Task<string> GenerateDocumentPdfAsync(int documentId);
        Task<string> SignDocumentAsync(int documentId, string signatureData, string signerName, string signerPosition);

        // Document-specific methods
        Task<List<DocumentFieldDto>> GetDocumentFieldsAsync(int documentId);
        Task<List<DocumentSignatureDto>> GetDocumentSignaturesAsync(int documentId);
        Task UpdateDocumentStatusAsync(int documentId, string status);
        Task<string> GetDocumentFilePathAsync(int documentId);
        Task<string> GenerateAndSaveDocumentPdfAsync(int documentId);
    }
}

