using AppAloj.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAloj.Datos.Mapping
{
    class CoworkMap : IEntityTypeConfiguration<Cowork>
    {
        public void Configure(EntityTypeBuilder<Cowork> builder)
        {
            builder.ToTable("COWORK").HasKey(c => c.idcowork);
        }
    }
}
