using FileStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileStorage.DAL.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder
                .HasKey(d => d.Id);
            builder
                .Property(d => d.Id)
                .ValueGeneratedOnAdd();
            builder
                .HasOne(d => d.User)
                .WithMany(u => u.Documents)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
