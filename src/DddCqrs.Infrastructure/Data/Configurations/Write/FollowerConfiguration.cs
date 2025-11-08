using DddCqrs.Domain.Followers;
using DddCqrs.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddCqrs.Infrastructure.Data.Configurations.Write;

internal sealed class FollowerConfiguration : IEntityTypeConfiguration<Follower>
{
    public void Configure(EntityTypeBuilder<Follower> builder)
    {
        builder.HasKey(f => new { f.UserId, f.FollowedId });


        builder.HasIndex(f => new { f.FollowedId, f.UserId });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(f => f.FollowedId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Ignore(f => f.Id);
    }
}
