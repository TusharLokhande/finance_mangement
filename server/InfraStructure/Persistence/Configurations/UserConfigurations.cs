using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfraStructure.Persistence.Configurations
{
    internal class UserConfigurations : BaseConfiguration<Users>
    {
        public override void Configure(EntityTypeBuilder<Users> builder)
        {
            base.Configure(builder);
            builder.ToTable("Users");
            builder.Property(k => k.EmailId).HasMaxLength(255);
            builder.Property(k => k.Name).HasMaxLength(255);
        }
    }
}
