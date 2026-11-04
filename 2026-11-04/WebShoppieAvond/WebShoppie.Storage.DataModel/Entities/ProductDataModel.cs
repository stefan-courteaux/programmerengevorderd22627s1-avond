using System;
using System.Collections.Generic;

namespace WebShoppie.Storage.DataModel.Entities;

public partial class ProductDataModel
{
    public int Productid { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stockcount { get; set; }

    public virtual ICollection<OrderproductDataModel> Orderproducts { get; set; } = new List<OrderproductDataModel>();
}
