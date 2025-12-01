//using System.Reflection;

namespace NorthWind.Sales.Backend.DataContext.EFCore.DataContexts;

internal class NorthWindContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer("Server=pc07; DataBase=NorthWindDB; User=sa; Password=sa;");
        optionsBuilder.UseSqlServer("Server=DESKTOP-3L362SI;DataBase=NorthWindDB;Integrated Security=True;TrustServerCertificate=True;");
        //base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Repositories.Entities.OrderDetail> OrderDetails { get; set; }

    // Permite a las herramientas del EntityFramework Core aplicar la configuración de la entidades
    // es decir migrar las clases en tablas
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //base.OnModelCreating(modelBuilder);
    }
}
