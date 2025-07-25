using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.DTOs
{
    public class UniversityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int UniversityId { get; set; }
        public UniversityDto University { get; set; }
        public IList<StudentDto> Students { get; set; }
    }
}
