using UniversityManagementSystem.Domain.Enums;

namespace UniversityManagementSystem.Domain.Entities
{
    public class InventoryMovement : IEntity
    {
        public int Id { get; set; }
        public int InventoryItemId { get; set; }
        public MovementType MovementType { get; set; }
        public int Quantity { get; set; }
        public string Reason { get; set; }
        public DateTime MovementDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get ; set ; }
        public DateTime ModifiedAt { get ; set ; }
        public string ModifiedBy { get ; set ; }
        public string DeletedBy { get ; set ; }
    }
}
