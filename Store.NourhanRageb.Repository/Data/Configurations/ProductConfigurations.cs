using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.NourhanRageb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.NourhanRageb.Repository.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).IsRequired().HasMaxLength(200);

            builder.Property(P => P.PictureUrl).IsRequired(true);

            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
          
            builder.HasOne(P => P.Brand)
                   .WithMany()
                   .HasForeignKey(P => P.BrandId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(P => P.type)
                .WithMany()
                .HasForeignKey(P => P.TypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(P => P.BrandId).IsRequired(false);
            builder.Property(P => P.TypeId).IsRequired(false);
        }
    }
}
