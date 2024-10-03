namespace PhanQuyenAPI.Models
{
    public class Function
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Role_Function> Role_Functions { get; set; }
    }
}
