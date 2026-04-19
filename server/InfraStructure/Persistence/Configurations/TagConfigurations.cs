using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfraStructure.Persistence.Configurations
{
    internal class TagConfigurations: BaseConfiguration<Tags>
    {
        public override void Configure(EntityTypeBuilder<Tags> builder)
        {
            base.Configure(builder);
            builder.ToTable("Tags");
            builder.Property(col => col.TagName).HasMaxLength(255);
            builder.HasIndex(col => col.TagName);
        }
    }
}
