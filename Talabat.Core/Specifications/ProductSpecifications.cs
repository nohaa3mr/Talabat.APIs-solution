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
        public ProductSpecifications(ProductSpecParams Params) :base(P => (!Params.BrandId.HasValue || P.ProductBrandId== Params.BrandId)&& (!Params.TypeId.HasValue || P.ProductTypeId == Params.TypeId))
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);

            if (!string.IsNullOrEmpty(Params.Sort))
            {
                switch (Params.Sort)
                {
                    case "PriceAcs": AddOrderByAsc(P => P.Price);
                            break;
                    case "PriceDesc":AddOrderByDesc(P => P.Price);
                        break;
                        default:AddOrderByAsc(P => P.Name);
                        break;


                }
            }
            ApplyPagination(Params.Pagesize * (Params.PageIndex - 1), Params.Pagesize);
        }

        public ProductSpecifications(int id): base(p => p.Id ==id)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }
    }
}
