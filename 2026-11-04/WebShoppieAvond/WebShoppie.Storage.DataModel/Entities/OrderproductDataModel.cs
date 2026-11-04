using System;
using System.Collections.Generic;

namespace WebShoppie.Storage.DataModel.Entities;

public partial class OrderproductDataModel
{
    public int Orderproductid { get; set; }

    public int Orderid { get; set; }

    public int Productid { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual OrderDataModel OrderDataModel { get; set; } = null!;

    public virtual ProductDataModel ProductDataModel { get; set; } = null!;
}
