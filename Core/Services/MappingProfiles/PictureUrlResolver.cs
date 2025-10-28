﻿using AutoMapper;
using Domain.Entities.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.Dtos;
using System.Buffers.Text;

namespace Services.MappingProfiles
{
    public class PictureUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl)) return string.Empty;
            return $"{_configuration.GetSection("URLS")["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
