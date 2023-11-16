using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EntityConfigurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("contacts");

        builder.HasKey(c => c.Id).HasName("PRIMARY");
        builder.HasIndex(c => c.Id, "contact_id_UNIQUE").IsUnique();
        builder.Property(c => c.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

        builder.Property(c => c.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
        builder.Property(c => c.MobilePhone).HasColumnName("phone").IsRequired().HasMaxLength(20);
        builder.Property(c => c.JobTitle).HasColumnName("job_title").HasMaxLength(50);
        builder.Property(c => c.BirthDate).HasColumnName("birth_date").HasColumnType("date");
    }
}