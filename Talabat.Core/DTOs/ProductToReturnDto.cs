using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
namespace Talabat.Core.DTOs
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Discription { get; set; }
        public string PictureURL { get; set; }
        public string ProductBrand { get; set; }
        public int ProductBrandId { get; set; }
        public string ProductType { get; set; }
        public int ProductTypeId { get; set; }
    }
}
