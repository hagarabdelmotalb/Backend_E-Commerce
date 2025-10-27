﻿namespace Shared.Dtos
{
    public class ProductResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal price { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public string TypeName { get; set; } = string.Empty;
        public string BrandName { get; set; } = string.Empty;
    }
}
