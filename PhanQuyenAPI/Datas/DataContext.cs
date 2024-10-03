namespace PhanQuyenAPI.Datas
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> UserRoles { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<Role_Function> RoleFunctions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User_Role>().HasKey(x => new { x.UserId, x.RoleId});
            modelBuilder.Entity<User_Role>()
                .HasOne(u => u.Role)
                .WithMany(x => x.User_Roles)
                .HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<User_Role>()
                .HasOne(u => u.User)
                .WithMany(x => x.User_Roles)
                .HasForeignKey(u => u.UserId);
            modelBuilder.Entity<Role_Function>()
                .HasKey(x => new {x.RoleId, x.FunctionId});
            modelBuilder.Entity<Role_Function>()
                .HasOne(u => u.Function)
                .WithMany(x => x.Role_Functions)
                .HasForeignKey(x => x.FunctionId);
            modelBuilder.Entity<Role_Function>()
                .HasOne(u => u.Role)
                .WithMany(x => x.Role_Functions)
                .HasForeignKey(u => u.RoleId);
;
        }
    }
}
