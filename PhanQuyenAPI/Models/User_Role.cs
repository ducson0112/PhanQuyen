namespace PhanQuyenAPI.Models
{
    public class User_Role
    {
        public User User  { get; set; }
        public int UserId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
    }
}
