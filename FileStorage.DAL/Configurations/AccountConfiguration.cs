using FileStorage.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileStorage.DAL.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .Property(a => a.Files)
                .HasDefaultValue(0);
            builder
                .Property(a => a.UsedSpace)
                .HasDefaultValue(0);
            builder
                .HasOne(u => u.User)
                .WithOne(a => a.Account)
                .HasForeignKey<User>(x => x.AccountId);
        }
    }
}
