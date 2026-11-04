using WebShoppie.Api.Contracts;
using WebShoppie.Api.Contracts.Products;
using WebShoppie.Storage.DataModel;
using WebShoppie.Storage.DataModel.Entities;

namespace WebShoppie.Domain.Services.Products;

public static class MappingExtensions
{
    public static ProductResponseContract AsContract(this ProductDataModel dataModel)
    {
        return new ProductResponseContract
        {
            ProductId = dataModel.Productid,
            Title = dataModel.Title,
            Description = dataModel.Description,
            Price = dataModel.Price,
            StockCount = dataModel.Stockcount
        };
    }

    public static ProductDataModel AsDataModel(this ProductRequestContract contract)
    {
        return new ProductDataModel
        {
            Title = contract.Title,
            Description = contract.Description,
            Price = contract.Price,
            Stockcount = contract.StockCount
        };
    }
}