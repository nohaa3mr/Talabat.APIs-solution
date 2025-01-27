using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.Helpers
{
    public class BasketItemsDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string PictureUrl { get; set; }
        [Required ]
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        [Required]
        [Range(0.0 , double.MaxValue)]
        public decimal Price { get; set; }
        [Required]

        public string Type { get; set; }
        [Required]
        public string Brand { get; set; }
    }
}
