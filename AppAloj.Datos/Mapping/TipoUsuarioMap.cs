using AppAloj.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAloj.Datos.Mapping
{
    class TipoUsuarioMap : IEntityTypeConfiguration<TipoUsuario>
    {
        public void Configure(EntityTypeBuilder<TipoUsuario> builder)
        {
            builder.ToTable("TIPOUSUARIO").HasKey(c => c.idtipousuario);
        }
    }
}
