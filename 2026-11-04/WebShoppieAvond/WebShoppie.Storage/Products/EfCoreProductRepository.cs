using WebShoppie.Storage.DataModel;
using WebShoppie.Storage.DataModel.Entities;

namespace WebShoppie.Storage.Products;

public class EfCoreProductRepository(WebShoppieDbContext _dbContext) : IProductRepository
{
    public ProductDataModel Create(ProductDataModel product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
        return product;
    }

    public ProductDataModel? GetById(int id)
    {
        return _dbContext.Products.Find(id);
    }

    public IEnumerable<ProductDataModel> GetAll()
    {
        return _dbContext.Products;
    }

    public bool Delete(int productId)
    {
        var existingProduct = _dbContext.Products.Find(productId);
        if (existingProduct == null) return false;
        _dbContext.Products.Remove(existingProduct);
        _dbContext.SaveChanges();
        return true;
    }

    public ProductDataModel Update(ProductDataModel product)
    {
        _dbContext.Products.Update(product);
        _dbContext.SaveChanges();
        return product;
    }
}