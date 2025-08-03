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
namespace UniversityManagementSystem.Application.Services
{
    public class DocumentService : Injectable, IDocumentService
    {
        private readonly IRepository<OfficialDocument> _documentRepository;
        private readonly IRepository<DocumentTemplate> _templateRepository;
        private readonly IRepository<DocumentField> _fieldRepository;
        private readonly IRepository<DocumentSignature> _signatureRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DocumentService(
            IRepository<OfficialDocument> documentRepository,
            IRepository<DocumentTemplate> templateRepository,
            IRepository<DocumentField> fieldRepository,
            IRepository<DocumentSignature> signatureRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _documentRepository = documentRepository;
            _templateRepository = templateRepository;
            _fieldRepository = fieldRepository;
            _signatureRepository = signatureRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        // Document methods
        public async Task<List<OfficialDocumentDto>> GetAllDocumentsAsync()
        {
            var documents = await _documentRepository.GetAllAsync(
                d => d.DocumentFields,
                d => d.DocumentSignatures);

            return _mapper.Map<List<OfficialDocumentDto>>(documents);
        }

        public async Task<List<OfficialDocumentDto>> GetDocumentsByTypeAsync(string documentType)
        {
            var documents = await _documentRepository.GetAllAsync(
                d => d.DocumentType == documentType,
                d => d.DocumentFields,
                d => d.DocumentSignatures);

            return _mapper.Map<List<OfficialDocumentDto>>(documents);
        }

        public async Task<OfficialDocumentDto> GetDocumentByIdAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id,
                d => d.DocumentFields,
                d => d.DocumentSignatures);

            return document != null ? _mapper.Map<OfficialDocumentDto>(document) : null;
        }

        public async Task<OfficialDocumentDto> CreateDocumentAsync(OfficialDocumentDto documentDto)
        {
            var document = _mapper.Map<OfficialDocument>(documentDto);
            document.DocumentNumber = GenerateDocumentNumber(document.DocumentType);

            await _documentRepository.AddAsync(document);
            await _unitOfWork.CommitAsync();

            // إنشاء الحقول المرتبطة
            foreach (var fieldDto in documentDto.DocumentFields)
            {
                var field = _mapper.Map<DocumentField>(fieldDto);
                field.DocumentId = document.Id;
                await _fieldRepository.AddAsync(field);
            }

            await _unitOfWork.CommitAsync();
            return _mapper.Map<OfficialDocumentDto>(document);
        }

        public async Task UpdateDocumentAsync(int id, OfficialDocumentDto documentDto)
        {
            var existingDocument = await _documentRepository.GetByIdAsync(id);
            if (existingDocument == null)
                throw new Exception($"Document with ID {id} not found");

            _mapper.Map(documentDto, existingDocument);
            _documentRepository.Update(existingDocument);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteDocumentAsync(int id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document == null)
                throw new Exception($"Document with ID {id} not found");

            _documentRepository.Delete(document);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> DocumentExistsAsync(int id)
        {
            return await _documentRepository.ExistsAsync(d => d.Id == id);
        }

        // Template methods
        public async Task<List<DocumentTemplateDto>> GetAllTemplatesAsync()
        {
            var templates = await _templateRepository.GetAllAsync();
            return _mapper.Map<List<DocumentTemplateDto>>(templates);
        }

        public async Task<DocumentTemplateDto?> GetTemplateByIdAsync(int id)
        {
            var template = await _templateRepository.GetByIdAsync(id);
            return template != null ? _mapper.Map<DocumentTemplateDto>(template) : null;
        }

        public async Task<DocumentTemplateDto> CreateTemplateAsync(DocumentTemplateDto templateDto)
        {
            var template = _mapper.Map<DocumentTemplate>(templateDto);
            await _templateRepository.AddAsync(template);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<DocumentTemplateDto>(template);
        }

        public async Task<DocumentTemplateDto> UpdateTemplateAsync(int id, DocumentTemplateDto templateDto)
        {
            var existingTemplate = await _templateRepository.GetByIdAsync(id);
            if (existingTemplate == null)
                throw new Exception($"Template with ID {id} not found");

            _mapper.Map(templateDto, existingTemplate);
            _templateRepository.Update(existingTemplate);
            await _unitOfWork.CommitAsync();
            return templateDto;
        }

        public async Task DeleteTemplateAsync(int id)
        {
            var template = await _templateRepository.GetByIdAsync(id);
            if (template == null)
                throw new Exception($"Template with ID {id} not found");

            _templateRepository.Delete(template);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> TemplateExistsAsync(int id)
        {
            return await _templateRepository.ExistsAsync(t => t.Id == id);
        }

        // Business logic methods
        public async Task<string> GenerateDocumentPdfAsync(int documentId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId,
                d => d.DocumentFields,
                d => d.DocumentSignatures);

            if (document == null)
                throw new Exception("Document not found");

            var fileName = $"Document_{document.DocumentNumber}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            var filePath = Path.Combine("wwwroot", "documents", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text($"جامعة الذكية - {document.Title}")
                        .SemiBold().FontSize(16).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Item().Text(document.Content);

                            // إضافة الحقول
                            if (document.DocumentFields.Any())
                            {
                                x.Item().PaddingTop(20).Text("المعلومات الإضافية:").Bold();
                                foreach (var field in document.DocumentFields)
                                {
                                    x.Item().Text($"{field.FieldName}: {field.FieldValue}");
                                }
                            }

                            // إضافة التوقيعات
                            if (document.DocumentSignatures.Any())
                            {
                                x.Item().PaddingTop(40).Text("التوقيعات:").Bold();
                                foreach (var signature in document.DocumentSignatures)
                                {
                                    x.Item().PaddingTop(10).Text($"{signature.SignerName} - {signature.SignerPosition}");
                                    x.Item().Text($"التاريخ: {signature.SignatureDate:yyyy-MM-dd HH:mm}");
                                }
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text($"رقم الوثيقة: {document.DocumentNumber} | التاريخ: {DateTime.Now:yyyy-MM-dd}")
                        .FontSize(10);
                });
            })
            .GeneratePdf(filePath);

            document.FilePath = $"/documents/{fileName}";
            document.Status = "مطبوعة";

            _documentRepository.Update(document);
            await _unitOfWork.CommitAsync();

            return document.FilePath;
        }

        public async Task<string> SignDocumentAsync(int documentId, string signatureData, string signerName, string signerPosition)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);
            if (document == null)
                throw new Exception("Document not found");

            var signature = new DocumentSignature
            {
                SignerName = signerName,
                SignerPosition = signerPosition,
                SignatureData = signatureData,
                SignatureDate = DateTime.Now,
                DocumentId = documentId
            };

            await _signatureRepository.AddAsync(signature);
            document.SignatureData = signatureData;
            document.Status = "موقعة";
            document.IssuedDate = DateTime.Now;

            _documentRepository.Update(document);
            await _unitOfWork.CommitAsync();

            return "تم التوقيع بنجاح";
        }

        // Document-specific methods
        public async Task<List<DocumentFieldDto>> GetDocumentFieldsAsync(int documentId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId, d => d.DocumentFields);
            return _mapper.Map<List<DocumentFieldDto>>(document?.DocumentFields ?? new List<DocumentField>());
        }

        public async Task<List<DocumentSignatureDto>> GetDocumentSignaturesAsync(int documentId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId, d => d.DocumentSignatures);
            return _mapper.Map<List<DocumentSignatureDto>>(document?.DocumentSignatures ?? new List<DocumentSignature>());
        }

        public async Task UpdateDocumentStatusAsync(int documentId, string status)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);
            if (document == null)
                throw new Exception($"Document with ID {documentId} not found");

            document.Status = status;
            _documentRepository.Update(document);
            await _unitOfWork.CommitAsync();
        }

        public async Task<string> GetDocumentFilePathAsync(int documentId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);
            return document?.FilePath ?? "";
        }

        // Private helper methods
        private string GenerateDocumentNumber(string documentType)
        {
            var prefix = documentType switch
            {
                "شهادة" => "CERT",
                "قرار" => "DEC",
                "خطاب" => "LET",
                "عقد" => "CONT",
                _ => "DOC"
            };
            return $"{prefix}-{DateTime.Now:yyyy}-{new Random().Next(1000, 9999)}";
        }
        public async Task<string> GenerateAndSaveDocumentPdfAsync(int documentId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId,
                d => d.DocumentFields,
                d => d.DocumentSignatures);

            if (document == null)
                throw new Exception("Document not found");

            // إنشاء اسم ملف فريد
            var fileName = $"Document_{document.DocumentNumber}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            var folderPath = Path.Combine("wwwroot", "documents");
            var filePath = Path.Combine(folderPath, fileName);
            var relativePath = $"/documents/{fileName}";

            // التأكد من وجود المجلد
            Directory.CreateDirectory(folderPath);

            // إنشاء ملف PDF
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text(document.Title)
                        .SemiBold().FontSize(16).FontColor(Colors.Blue.Medium).AlignCenter();

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            // محتوى الوثيقة
                            x.Item().Text(document.Content);

                            // إضافة الحقول
                            if (document.DocumentFields.Any())
                            {
                                x.Item().PaddingTop(20).Text("المعلومات الإضافية:").Bold();
                                foreach (var field in document.DocumentFields)
                                {
                                    x.Item().Text($"{field.FieldName}: {field.FieldValue}");
                                }
                            }

                            // إضافة التوقيعات
                            if (document.DocumentSignatures.Any())
                            {
                                x.Item().PaddingTop(40).Text("التوقيعات:").Bold();
                                foreach (var signature in document.DocumentSignatures)
                                {
                                    x.Item().PaddingTop(10).Text($"{signature.SignerName} - {signature.SignerPosition}");
                                    x.Item().Text($"التاريخ: {signature.SignatureDate:yyyy-MM-dd HH:mm}");
                                }
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text($"رقم الوثيقة: {document.DocumentNumber} | التاريخ: {DateTime.Now:yyyy-MM-dd HH:mm}")
                        .FontSize(10);
                });
            })
            .GeneratePdf(filePath);

            // تحديث مسار الملف في قاعدة البيانات
            document.FilePath = relativePath;
            document.Status = "مطبوعة";
            _documentRepository.Update(document);
            await _unitOfWork.CommitAsync();

            return relativePath;
        }
    }
}

