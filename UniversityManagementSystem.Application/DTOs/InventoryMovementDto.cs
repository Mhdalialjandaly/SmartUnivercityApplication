

using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Application.DTOs
{
    public class InventoryMovementDto
    {
        public int Id { get; set; }
        public int InventoryItemId { get; set; }
        public string ItemName { get; set; }
        public MovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; }
        public DateTime MovementDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
