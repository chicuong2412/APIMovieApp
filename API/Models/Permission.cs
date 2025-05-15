namespace API.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        // Navigation properties
        public virtual ICollection<Role> RolePermissions { get; set; } = new List<Role>();
    }
}
