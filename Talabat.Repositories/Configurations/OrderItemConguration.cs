﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order;

namespace Talabat.Repositories.Configurations
{
    internal class OrderItemConguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(O => O.ProductItemOrdered).WithOwner();
            builder.Property(O => O.Price).HasColumnType("decimal(18,2)");

        }
    }
}
