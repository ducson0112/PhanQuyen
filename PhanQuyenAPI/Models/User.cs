using System.Text.Json.Serialization;

namespace PhanQuyenAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<User_Role> User_Roles { get; set; }
    }
}
