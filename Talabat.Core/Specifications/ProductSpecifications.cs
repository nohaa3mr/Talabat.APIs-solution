using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product>
    {
        public ProductSpecifications(string? sort):base()
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "PriceAcs": AddOrderByAsc(P => P.Price);
                            break;
                    case "PriceDesc":AddOrderByDesc(P => P.Price);
                        break;
                        default:AddOrderByAsc(P => P.Name);
                        break;


                }
            }
        }

        public ProductSpecifications(int id): base(p => p.Id ==id)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }
    }
}
