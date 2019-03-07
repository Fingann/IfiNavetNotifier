using System;
using IfiNavet.Core.Entities;
using IfiNavet.Core.Entities.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IfiNavet.Persistence.Configurations
{
    public class IfiEventConfiguration: IEntityTypeConfiguration<IfiEvent>
    {
        public void Configure(EntityTypeBuilder<IfiEvent> builder)
        {
            builder.HasKey(x => x.Link);

        }
    }
}