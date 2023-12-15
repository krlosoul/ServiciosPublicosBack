namespace SysprotecBack.Infrastructure.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using SysprotecBack.Core.Entities;
    using SysprotecBack.Infrastructure.Common.Constants;
    using SysprotecBack.Infrastructure.Common.Dtos;

    public partial class SysprotecContext : DbContext
    {
        #region Properties
        private readonly IConfiguration _configuration;
        private DataBaseDto? _dataBaseDto;
        #endregion

        public SysprotecContext(DbContextOptions<SysprotecContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        #region Entities
        public virtual DbSet<Report> Reports { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Service> Services { get; set; }

        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<User> Users { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            DataBaseDto instance = _dataBaseDto = new DataBaseDto();
            _configuration.Bind(DataBaseConstant.DataBase, instance);
            var connectionString = _dataBaseDto.ConnectionString;
            if (!string.IsNullOrEmpty(connectionString)) optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Pk_Report_Id");

                entity.ToTable("Report");

                entity.Property(e => e.CreationOn).HasColumnType("datetime");
                entity.Property(e => e.Observation)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Service).WithMany(p => p.Reports)
                    .HasForeignKey(d => d.IdService)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pk_Report_Service");

                entity.HasOne(d => d.Status).WithMany(p => p.Reports)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pk_Report_Status");

                entity.HasOne(d => d.User).WithMany(p => p.Reports)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Pk_Report_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Pk_Role_Id");

                entity.ToTable("Role");

                entity.Property(e => e.Name)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Pk_Service_Id");

                entity.ToTable("Service");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Pk_Status_Id");

                entity.ToTable("Status");

                entity.Property(e => e.Name)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Pk_User_Id");

                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.IdUser, ur.IdRole }).HasName("Pk_UserRole_Id");
                entity.ToTable("UserRole");
                entity.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.IdUser)
                    .HasConstraintName("Fk_UserRole_User");
                entity.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.IdRole)
                    .HasConstraintName("Fk_UserRole_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}