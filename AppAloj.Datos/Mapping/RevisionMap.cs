using AppAloj.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AppAloj.Datos.Mapping
{
    public class RevisionMap : IEntityTypeConfiguration<Revision>
    {
        public void Configure(EntityTypeBuilder<Revision> builder)
        {
            builder.ToTable("REVISION").HasKey(c => c.IdRevision);
        }
    }
}
