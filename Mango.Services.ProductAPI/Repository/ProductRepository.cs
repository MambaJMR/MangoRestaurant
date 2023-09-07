using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public ProductRepository(ApplicationDbContext db, IMapper mapper) // Dependecy Injection
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto,Product>(productDto); // Преобразуем сущность ProductDto в Product с помощью automapper и его метода Map.
            if (product.ProductId > 0) // проверям если такой товар существует, тогда нам необходимо его обновить
            {
                _db.Products.Update(product); // обновляем существующий товар
            }
            else
            {
                _db.Products.Add(product); // создаем новую запись в базе данных
            }

            await _db.SaveChangesAsync();

            return _mapper.Map<Product, ProductDto>(product); // Преобразовываем обратно из Product в ProductDto с полученым результатом работы метода
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product product = await _db.Products.FirstOrDefaultAsync(u => u.ProductId == productId); // Получаем товар по Id из базы данных
                if (product == null) // проверяем существует ли такой товар
                {
                    return false;
                }
                _db.Products.Remove(product); // удаляем найденый товар 
                await _db.SaveChangesAsync(); // сохраняем изменения в базу данных
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await _db.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();   // получаем товар по Id из базы данных
            return _mapper.Map<ProductDto>(product); // преобразовываем товар с помощью маппера в ProductDto
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List < Product > productList = await _db.Products.ToListAsync();   // получаем список товаров из базы данных
            return _mapper.Map<List<ProductDto>>(productList); // преобразовываем товары с помощью маппера в ProductDto
        }
    }

}
