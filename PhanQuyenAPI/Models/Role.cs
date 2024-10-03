using System.Text.Json.Serialization;

namespace PhanQuyenAPI.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<User_Role> User_Roles { get; set; } 
        public ICollection<Role_Function> Role_Functions { get; set; }
    }
}
