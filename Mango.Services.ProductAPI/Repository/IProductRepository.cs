using Mango.Services.ProductAPI.Models.Dto;

namespace Mango.Services.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetProducts(); //Возвращаем список товаров
        Task<ProductDto> GetProductById(int productId); //Возвращаем товар по Id
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto); // Создаем новый или обновляем существующий товар
        Task<bool> DeleteProduct(int productId); // Удаляем существующий товар если таковой имеется.
    }
}
