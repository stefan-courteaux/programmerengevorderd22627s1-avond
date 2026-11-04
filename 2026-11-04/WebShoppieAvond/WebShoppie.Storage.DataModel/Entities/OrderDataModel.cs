using System;
using System.Collections.Generic;

namespace WebShoppie.Storage.DataModel.Entities;

public partial class OrderDataModel
{
    public int Orderid { get; set; }

    public DateTime Orderdate { get; set; }

    public int Customerid { get; set; }

    public decimal Totalprice { get; set; }

    public virtual CustomerDataModel CustomerDataModel { get; set; } = null!;

    public virtual ICollection<OrderproductDataModel> Orderproducts { get; set; } = new List<OrderproductDataModel>();
}
