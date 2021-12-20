using System.Collections.Generic;

namespace Web_Core_Auth_BaseRoles.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
