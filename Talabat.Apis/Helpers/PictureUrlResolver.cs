﻿using AutoMapper;
using Talabat.Core.DTOs;
using Talabat.Core.Entities;

namespace Talabat.Apis.Helpers
{
    public class PictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
           _configuration = configuration;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureURL))
                return $"{_configuration["ApiUrl"]}{source.PictureURL}" ;
            return string.Empty;
        }
    }
}
