using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repositories.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.ProductBrand).WithMany().HasForeignKey(P => P.ProductBrandId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(P => P.ProductType).WithMany().HasForeignKey(P => P.ProductTypeId).OnDelete(DeleteBehavior.Cascade);
            builder.Property(P => P.Name).IsRequired();
            builder.Property(P => P.PictureURL).IsRequired();
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");

        }
    }
}
