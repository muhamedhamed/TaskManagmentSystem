using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagmentSystem.Domain.Entities;

namespace TaskManagmentSystem.Infrastructure.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
         // Primary Key configuration
        builder.HasKey(u => u.TaskId);

        // Auto-generation of User Id
        builder.Property(u => u.TaskId)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("NEWID()")
            .IsUnicode(false)
            .HasMaxLength(36); // Assuming Guid as string has max length of 36

        builder.Property(u => u.Title)
            .IsRequired()
            .HasMaxLength(255); // Adjust the length as needed

        builder.Property(u => u.Description)
            .IsRequired()
            .HasMaxLength(255); // Adjust the length as needed
    }
}
