namespace SysprotecBack.Core.Entities
{
    public partial class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}