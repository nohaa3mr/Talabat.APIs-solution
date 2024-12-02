using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Discription { get; set; }
        public string PictureURL { get; set; }
        public ProductBrand ProductBrand{ get; set; }
        public int ProductBrandId { get; set; }
        public ProductType ProductType { get; set; }
        public int ProductTypeId { get; set; }


    }
}
