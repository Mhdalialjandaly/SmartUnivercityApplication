using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.Domain.Entities;

namespace UniversityManagementSystem.Application.DTOs
{
    public class TunnelDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TypeOfkinship { get; set; }
        public string FirstPart { get; set; }
        public string SecoundPart { get; set; }
        public IList<StudentDto> Students { get; set; }
    }
}
