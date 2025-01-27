using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Repositories.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, O => O.WithOwner());

            builder.Property(O => O.OrderStatus)
                .HasConversion(OS => OS.ToString(),OS => (OrderStatus)Enum.Parse(typeof(OrderStatus),OS));

            builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");
            builder.HasOne(i => i.deliveryMethod).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
