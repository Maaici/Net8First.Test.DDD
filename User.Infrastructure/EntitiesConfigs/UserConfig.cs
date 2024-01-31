using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain;
using User.Domain.Entities;
using User.Domain.ValueObjects;

namespace User.Infrastructure.EntitiesConfigs
{
    public class UserConfig : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.ToTable("T_Users");
            builder.OwnsOne(x => x.PhoneNumber, nb =>
            {
                nb.Property(b => b.Number).HasMaxLength(20).IsUnicode(false);
                nb.Property(b => b.RegionNumber).IsUnicode(false);
            });
            builder.HasOne(x => x.UserAccessFail).WithOne(f => f.User).HasForeignKey<UserAccessFail>(f => f.UserId);
            builder.Property("passwordHash").HasMaxLength(100).IsUnicode(false);
        }
    }
}
