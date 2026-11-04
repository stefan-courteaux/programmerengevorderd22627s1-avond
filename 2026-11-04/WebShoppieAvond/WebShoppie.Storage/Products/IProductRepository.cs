using WebShoppie.Storage.DataModel;
using WebShoppie.Storage.DataModel.Entities;

namespace WebShoppie.Storage.Products;

public interface IProductRepository
{
    ProductDataModel Create(ProductDataModel product);
    ProductDataModel? GetById(int id);
    IEnumerable<ProductDataModel> GetAll();
    bool Delete(int productId);
    ProductDataModel Update(ProductDataModel product);
}