using System;

namespace Models.DTOs
{
    public class UserModel : IDto
    {
        public DateTime? Deactivated { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Id { get; set; }
        public DateTime Created { get; set; }
    }
}