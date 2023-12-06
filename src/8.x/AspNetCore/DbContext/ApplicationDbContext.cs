namespace DbContext;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Define DbSet para cada entidad que deseas mapear a la base de datos
    //public DbSet<User> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuración adicional del modelo, como restricciones y relaciones
        /*modelBuilder.Entity<User>()
            .Property(u => u.Nombre)
            .IsRequired();*/

        base.OnModelCreating(modelBuilder);
    }
}
