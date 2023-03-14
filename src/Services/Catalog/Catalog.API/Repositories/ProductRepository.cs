using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task Add(Product product)
        {
            product.Id = ObjectId.GenerateNewId().ToString();
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Id, id);
            var updateResult = await _context.Products.DeleteOneAsync(filter);

            return updateResult.IsAcknowledged && updateResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAll() => await _context.Products.Find(p => true).ToListAsync();

        public async Task<IEnumerable<Product>> GetByCategoryName(string categoryName)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Category, categoryName);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<Product> GetById(string id) => await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> Update(Product product)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Id, product.Id);
            var updateResult = await _context.Products.ReplaceOneAsync(filter, product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
