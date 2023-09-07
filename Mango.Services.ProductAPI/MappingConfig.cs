using AutoMapper;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI
{
    // Файл класса настройки AutoMapper
    public class MappingConfig
    {
        // Метод возвращает конфигурацию сопоставления для AutoMapper
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<ProductDto, Product>().ReverseMap();
                //config.CreateMap<Product, ProductDto>();
            }) ;

            return mappingConfig;
        }
    }
}
