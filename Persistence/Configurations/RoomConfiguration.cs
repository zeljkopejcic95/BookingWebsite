using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Number)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(r => r.Capacity)
            .IsRequired();

        builder.Property(r => r.PriceOneNight)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.HasOne(r => r.Hotel)
            .WithMany(c => c.Rooms)
            .HasForeignKey(r => r.HotelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Reservations)
            .WithOne(res => res.Room)
            .HasForeignKey(res => res.RoomId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
