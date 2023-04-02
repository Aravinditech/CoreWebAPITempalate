using System;

namespace CoreTempalate.Models.EntityModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
    }

    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
