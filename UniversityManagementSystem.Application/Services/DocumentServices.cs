using UniversityManagementSystem.Application.DTOs;
using UniversityManagementSystem.Application.Interfaces;
using UniversityManagementSystem.Domain.Entities.UniversityManagementSystem.Core.Entities;
using UniversityManagementSystem.Domain.Entities;
using AutoMapper;
using UniversityManagementSystem.Domain.Common.Interfaces;
using UniversityManagementSystem.Domain.Interfaces;
using QuestPDF.Infrastructure;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using UniversityManagementSystem.Application.Abstractions;
using UniversityManagementSystem.Domain.Enums;
using UniversityManagementSystem.Application.Models;
namespace UniversityManagementSystem.Application.Services
{
    public class DocumentServices : Injectable, IDocumentServices
    {
        private readonly IRepository<OfficialDocument> _documentRepository;
        private readonly IRepository<DocumentSignature> _signatureRepository;
        private readonly IRepository<DocumentTemplate> _templateRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DocumentServices(
            IRepository<OfficialDocument> documentRepository,
            IRepository<DocumentSignature> signatureRepository,
            IRepository<DocumentTemplate> templateRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _documentRepository = documentRepository;
            _signatureRepository = signatureRepository;
            _templateRepository = templateRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<OfficialDocumentDto> GetDocumentByIdAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id,
                d => d.DocumentSignatures,
                d => d.DocumentFields);

            if (document == null)
                throw new Exception($"Document with ID {id} not found");

            return _mapper.Map<OfficialDocumentDto>(document);
        }

        public async Task<List<OfficialDocumentDto>> GetAllDocumentsAsync()
        {
            var documents = await _documentRepository.GetAllAsync(
                d => d.DocumentSignatures,
                d => d.DocumentFields);

            return _mapper.Map<List<OfficialDocumentDto>>(documents);
        }

        public async Task<PaginatedResult<OfficialDocumentDto>> GetDocumentsPagedAsync(int page, int pageSize, string searchTerm, string documentType, DocumentStatus status)
        {
            // الحصول على جميع الوثائق
            var query = await _documentRepository.GetAllAsync(
                d => d.DocumentSignatures,
                d => d.DocumentFields);

            // تطبيق الفلاتر
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(d =>
                    d.Title.Contains(searchTerm) ||
                    d.DocumentNumber.Contains(searchTerm)).ToList();
            }

            if (!string.IsNullOrEmpty(documentType))
            {
                query = query.Where(d => d.DocumentType == documentType).ToList();
            }

            if (status != DocumentStatus.All)
            {
                var statusString = status switch
                {
                    DocumentStatus.Draft => "مسودة",
                    DocumentStatus.Issued => "صادرة",
                    DocumentStatus.Signed => "موقعة",
                    _ => ""
                };
                query = query.Where(d => d.Status == statusString).ToList();
            }

            // تطبيق الترقيم
            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            var pagedData = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedResult<OfficialDocumentDto>
            {
                Data = _mapper.Map<List<OfficialDocumentDto>>(pagedData),
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                CurrentPage = page
            };
        }

        public async Task<OfficialDocumentDto> CreateDocumentAsync(OfficialDocumentDto documentDto)
        {
            var document = _mapper.Map<OfficialDocument>(documentDto);

            // تعيين القيم الافتراضية
            document.CreatedDate = DateTime.Now;
            document.Status = document.Status ?? "مسودة";
            document.DocumentNumber = document.DocumentNumber ?? GenerateDocumentNumber();

            await _documentRepository.AddAsync(document);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<OfficialDocumentDto>(document);
        }

        public async Task UpdateDocumentAsync(OfficialDocumentDto documentDto)
        {
            var existingDocument = await _documentRepository.GetByIdAsync(documentDto.Id);
            if (existingDocument == null)
                throw new Exception($"Document with ID {documentDto.Id} not found");

            _mapper.Map(documentDto, existingDocument);
            existingDocument.ModifiedAt = DateTime.Now;

            _documentRepository.Update(existingDocument);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteDocumentAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document == null)
                throw new Exception($"Document with ID {id} not found");

            document.DeletedAt = DateTime.Now;
            _documentRepository.Update(document);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> DocumentExistsAsync(int id)
        {
            return await _documentRepository.ExistsAsync(d => d.Id == id && d.DeletedAt == null);
        }

        // التوقيع الإلكتروني
        public async Task<bool> SignDocumentAsync(int documentId, string signatureData, string signerName, string signerPosition)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);
            if (document == null)
                return false;

            var signature = new DocumentSignature
            {
                DocumentId = documentId,
                SignerName = signerName,
                SignerPosition = signerPosition,
                SignatureData = signatureData,
                SignedDate = DateTime.Now,
                SignatureType = "إلكترونية",
                CreatedAt = DateTime.Now
            };

            await _signatureRepository.AddAsync(signature);

            // تحديث حالة الوثيقة
            document.Status = "موقعة";
            document.ModifiedAt = DateTime.Now;

            _documentRepository.Update(document);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<List<DocumentSignatureDto>> GetDocumentSignaturesAsync(int documentId)
        {
            var signatures = await _signatureRepository.FindAsync(
                s => s.DocumentId == documentId && s.DeletedAt == null);

            return _mapper.Map<List<DocumentSignatureDto>>(signatures);
        }

        // الإحصائيات
        public async Task<int> GetTotalDocumentsCountAsync()
        {
            return await _documentRepository.CountAsync(d => d.DeletedAt == null);
        }

        public async Task<int> GetIssuedDocumentsCountAsync()
        {
            return await _documentRepository.CountAsync(d => d.Status == "صادرة" && d.DeletedAt == null);
        }

        public async Task<int> GetDraftDocumentsCountAsync()
        {
            return await _documentRepository.CountAsync(d => d.Status == "مسودة" && d.DeletedAt == null);
        }

        public async Task<int> GetSignedDocumentsCountAsync()
        {
            return await _documentRepository.CountAsync(d => d.Status == "موقعة" && d.DeletedAt == null);
        }

        // الفلترة والحالة
        public async Task<List<OfficialDocumentDto>> GetDocumentsByStatusAsync(string status)
        {
            var documents = await _documentRepository.FindAsync(
                d => d.Status == status && d.DeletedAt == null,
                d => d.DocumentSignatures,
                d => d.DocumentFields);

            return _mapper.Map<List<OfficialDocumentDto>>(documents);
        }

        public async Task<List<OfficialDocumentDto>> GetDocumentsByTypeAsync(string documentType)
        {
            var documents = await _documentRepository.FindAsync(
                d => d.DocumentType == documentType && d.DeletedAt == null,
                d => d.DocumentSignatures,
                d => d.DocumentFields);

            return _mapper.Map<List<OfficialDocumentDto>>(documents);
        }

        public async Task<bool> IssueDocumentAsync(int documentId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);
            if (document == null)
                return false;

            document.Status = "صادرة";
            document.IssuedDate = DateTime.Now;
            document.ModifiedAt = DateTime.Now;

            _documentRepository.Update(document);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<List<OfficialDocumentDto>> GetDocumentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var documents = await _documentRepository.FindAsync(
                d => d.CreatedDate >= startDate && d.CreatedDate <= endDate && d.DeletedAt == null,
                d => d.DocumentSignatures,
                d => d.DocumentFields);

            return _mapper.Map<List<OfficialDocumentDto>>(documents);
        }

        // التصدير والطباعة
        public async Task<string> GenerateDocumentPdfAsync(int documentId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);
            if (document == null)
                throw new Exception($"Document with ID {documentId} not found");

            // هنا يجب تنفيذ منطق إنشاء ملف PDF
            // يمكن استخدام مكتبات مثل iTextSharp أو QuestPDF
            var fileName = $"Document_{documentId}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            var filePath = Path.Combine("wwwroot", "documents", fileName);

            // إنشاء المجلد إذا لم يكن موجوداً
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            // تنفيذ إنشاء PDF (هذا مثال بسيط)
            // في التطبيق الفعلي، يجب تنفيذ منطق تحويل المحتوى إلى PDF

            document.FilePath = $"/documents/{fileName}";
            document.ModifiedAt = DateTime.Now;

            _documentRepository.Update(document);
            await _unitOfWork.CommitAsync();

            return document.FilePath;
        }

        public async Task<byte[]> ExportDocumentsToExcelAsync(List<OfficialDocumentDto> documents)
        {
            // هنا يجب تنفيذ منطق تصدير البيانات إلى Excel
            // يمكن استخدام مكتبات مثل EPPlus أو ClosedXML

            // مثال بسيط لإرجاع مصفوفة بايت فارغة
            // يجب تنفيذ المنطق الفعلي لإنشاء ملف Excel
            return new byte[0];
        }

        // قوالب الوثائق
        public async Task<List<DocumentTemplateDto>> GetAllTemplatesAsync()
        {
            var templates = await _templateRepository.GetAllAsync();
            return _mapper.Map<List<DocumentTemplateDto>>(templates);
        }

        public async Task<DocumentTemplateDto> GetTemplateByIdAsync(int templateId)
        {
            var template = await _templateRepository.GetByIdAsync(templateId);
            if (template == null)
                throw new Exception($"Template with ID {templateId} not found");

            return _mapper.Map<DocumentTemplateDto>(template);
        }
        public async Task<DocumentTemplateDto> CreateTemplateAsync(DocumentTemplateDto templateDto)
        {
            var template = _mapper.Map<DocumentTemplate>(templateDto);

            // تعيين القيم الافتراضية
            template.CreatedAt = DateTime.Now;
            template.CreatedBy = templateDto.CreatedBy ?? "System";

            await _templateRepository.AddAsync(template);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<DocumentTemplateDto>(template);
        }

        // تحديث قالب موجود
        public async Task UpdateTemplateAsync(DocumentTemplateDto templateDto)
        {
            var existingTemplate = await _templateRepository.GetByIdAsync(templateDto.Id);
            if (existingTemplate == null)
                throw new Exception($"Template with ID {templateDto.Id} not found");

            _mapper.Map(templateDto, existingTemplate);
            existingTemplate.ModifiedAt = DateTime.Now;
            existingTemplate.ModifiedBy = templateDto.ModifiedBy ?? "System";

            _templateRepository.Update(existingTemplate);
            await _unitOfWork.CommitAsync();
        }

        // حذف قالب (soft delete)
        public async Task DeleteTemplateAsync(int templateId)
        {
            var template = await _templateRepository.GetByIdAsync(templateId);
            if (template == null)
                throw new Exception($"Template with ID {templateId} not found");

            template.DeletedAt = DateTime.Now;
            template.DeletedBy = "System"; // يمكن استبداله باسم المستخدم الحالي

            _templateRepository.Update(template);
            await _unitOfWork.CommitAsync();
        }
        // دوال مساعدة
        private string GenerateDocumentNumber()
        {
            return $"DOC-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}";
        }
    }
}


