using Authentication.API.DomainModels.Entities.Identity;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Authentication.API.Infrastructure.Configuration
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int, Authentication.API.DomainModels.Entities.Identity.UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IPersistedGrantDbContext
    {
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        Task<int> IPersistedGrantDbContext.SaveChangesAsync() => base.SaveChangesAsync();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
            modelBuilder.Entity<UserRole>(b =>
            {
                b.HasKey(ur => new { ur.UserId, ur.RoleId, ur.CompanyId });
            });

            modelBuilder.Entity<UserRole>()
              .HasOne(ur => ur.Company)
              .WithMany(c => c.UserRoles)
              .HasForeignKey(ur => ur.CompanyId);

            modelBuilder.Entity<UserRole>()
              .HasOne(ur => ur.Role)
              .WithMany(c => c.UserRoles)
              .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<UserRole>()
              .HasOne(ur => ur.User)
              .WithMany(c => c.UserRoles)
              .HasForeignKey(ur => ur.UserId);
        }
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }
    }
}
