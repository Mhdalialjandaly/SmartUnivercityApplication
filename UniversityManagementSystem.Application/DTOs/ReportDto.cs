using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReportDate { get; set; }
        public string Semester { get; set; }
        public ReportStatus Status { get; set; }
        public ReportType Type { get; set; }
        public int CourseId { get; set; }
        public int DepartmentId { get; set; }
        public string GeneratedBy { get; set; }
        public string Description { get; set; }
    }
}

