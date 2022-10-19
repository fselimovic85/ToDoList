using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TO_DO_LIST.Models;
using TO_DO_LIST.Service.UserPassHashSaltService;
using TO_DO_LIST.Service.UserServices;

namespace TO_DO_LIST.Data
{
    public class DataContext : DbContext
    {
        private readonly IUserPassHashSaltService _createHashSalt;
        public DataContext(DbContextOptions<DataContext> options, IUserPassHashSaltService createHashSalt) : base(options)
        {
            _createHashSalt = createHashSalt;
        }
       
        public DbSet<User> User { get; set; }
        public DbSet<DailyList> DailyList { get; set; }
        public DbSet<Tasks> Task { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
            .HasData(
                //Treba mi dependensi injection da uradim za kreiranje pasvorda, tj da kreiram poseban servis za to
                new User { Id = 1, Username = "edo", PasswordHash = _createHashSalt.CreateOnlyPasswordHash("98745fgsd", out byte[] passwordHash1), PasswordSalt = _createHashSalt.CreateOnlyPasswordSalt(out byte[] passwordSalt1), TimeZone = 4, Email="test@gmail.com"},
                new User { Id = 2, Username = "fikro", PasswordHash = _createHashSalt.CreateOnlyPasswordHash("456dfsgdf", out byte[] passwordHash2), PasswordSalt = _createHashSalt.CreateOnlyPasswordSalt(out byte[] passwordSalt2), TimeZone = 3, Email = "test@gmail.com" },
                new User { Id = 3, Username = "hus", PasswordHash = _createHashSalt.CreateOnlyPasswordHash("rewr45erwt", out byte[] passwordHash3), PasswordSalt = _createHashSalt.CreateOnlyPasswordSalt(out byte[] passwordSalt3), TimeZone = 2, Email = "test@gmail.com" }
            );
        }
    }
}
