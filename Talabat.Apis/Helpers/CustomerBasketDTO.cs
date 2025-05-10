using Talabat.Core.Entities;

namespace Talabat.Apis.Helpers
{
    public class CustomerBasketDTO
    {

        public string Id { get; set; }

        public List<BasketItemsDTO> Items { get; set; }
    }
}
