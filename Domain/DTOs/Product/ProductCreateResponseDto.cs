using System;
using Core.Domain.DTOs;

namespace Domain.DTOs.Product
{
    public class ProductCreateResponseDto:IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}

