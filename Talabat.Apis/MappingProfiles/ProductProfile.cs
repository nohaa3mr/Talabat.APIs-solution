using AutoMapper;
using Talabat.Apis.Helpers;
using Talabat.Core.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order;

namespace Talabat.Apis.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductToReturnDto>().
                ForMember(p=>p.ProductBrand , options => options.MapFrom(S=>S.ProductBrand.Name)).
                ForMember(p=> p.ProductType , options => options.MapFrom(S=>S.ProductType.Name)).
                ForMember(p=>p.PictureURL , options =>options.MapFrom<PictureUrlResolver>());

            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<CustomerBasketDTO, CustomerBasket>().ReverseMap();
            CreateMap<BasketItems, BasketItemsDTO>().ReverseMap();
            CreateMap< AddressDTO , OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(o => o.deliveryMethod, O => O.MapFrom(x => x.deliveryMethod.ShortName))
                .ForMember(O => O.deliveryMethodCost, options => options.MapFrom(O => O.deliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(x => x.ProductId, Options => Options.MapFrom(p => p.ProductItemOrdered.ProductId))
                .ForMember(x => x.ProductName, Options => Options.MapFrom(p => p.ProductItemOrdered.ProductName))
                .ForMember(x => x.PictureUrl, Options => Options.MapFrom(p => p.ProductItemOrdered.PictureUrl))
                .ForMember(p => p.PictureUrl, options => options.MapFrom<OrderItemPictureResolver>());





        }
    }
}
