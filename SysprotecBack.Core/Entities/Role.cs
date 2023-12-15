﻿namespace SysprotecBack.Core.Entities
{
    public partial class Role
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}