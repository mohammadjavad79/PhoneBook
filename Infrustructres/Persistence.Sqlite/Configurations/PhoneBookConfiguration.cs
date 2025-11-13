using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Sqlite.Configurations
{
    public class PhoneBookConfiguration : IEntityTypeConfiguration<PhoneBook>
    {
        public void Configure(EntityTypeBuilder<PhoneBook> builder)
        {
            builder.ToTable("PhoneBooks");

            builder.HasKey(pb => pb.Id);

            builder.Property(pb => pb.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(pb => pb.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Ignore(pb => pb.Rows);

            builder.HasMany<Row>("_rows")
                .WithOne()
                .HasForeignKey(r => r.PhoneBookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Navigation("_rows").UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
