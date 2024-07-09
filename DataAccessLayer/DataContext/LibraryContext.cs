using Microsoft.EntityFrameworkCore;
using Model.ModelForEF;

namespace DataAccessLayer.DataContext;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BooksStaging> BooksStagings { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersLog> UsersLogs { get; set; }

    public virtual DbSet<VReservationsHistory> VReservationsHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207DF194745");

            entity.HasIndex(e => e.AuthorName, "IX_Books_AuthorName");

            entity.HasIndex(e => e.AuthorSurname, "IX_Books_AuthorSurname");

            entity.HasIndex(e => e.PublishingHouse, "IX_Books_PublishingHouse");

            entity.HasIndex(e => e.Title, "IX_Books_Title");

            entity.HasIndex(e => new { e.Title, e.AuthorName, e.AuthorSurname, e.PublishingHouse }, "UNQ_Books").IsUnique();

            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AuthorSurname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PublishingHouse)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BooksStaging>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books_st__3DE0C20786870DB8");

            entity.ToTable("Books_staging");

            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AuthorSurname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PublishingHouse)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__B7EE5F24130E0A38");

            entity.HasIndex(e => e.EndDate, "IX_Reservations_EndDate");

            entity.HasIndex(e => e.StartDate, "IX_Reservations_StartDate");

            entity.Property(e => e.EndDate)
                .HasDefaultValueSql("(dateadd(day,(30),getdate()))")
                .HasColumnType("date");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");

            entity.HasOne(d => d.Book).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_Books");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reservations_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C25425A30");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("TRG_Users_UPDATE_password");
                    tb.HasTrigger("TRG_Users_UPDATE_role");
                });

            entity.HasIndex(e => e.Role, "IX_Users_Role");

            entity.HasIndex(e => e.Username, "IX_Users_Username");

            entity.HasIndex(e => e.Username, "UNQ_Users").IsUnique();

            entity.Property(e => e.Password)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasConversion<string>()
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UsersLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK_LogId");

            entity.Property(e => e.OperationDate).HasColumnType("datetime");
            entity.Property(e => e.OperationDescription)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VReservationsHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vReservationsHistory");

            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.StartDate).HasColumnType("date");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
