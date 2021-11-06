using AppAloj.Datos.Mapping;
using AppAloj.Entidades;
using Microsoft.EntityFrameworkCore;

namespace AppAloj.Datos
{
    public class AppAlojDbContext : DbContext
    {
        public AppAlojDbContext(DbContextOptions<AppAlojDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new CoworkMap());
            modelBuilder.ApplyConfiguration(new ReservaMap());
            modelBuilder.ApplyConfiguration(new RevisionMap());
            modelBuilder.ApplyConfiguration(new TipoUsuarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cowork> Coworks { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Revision> Revisions { get; set; }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
