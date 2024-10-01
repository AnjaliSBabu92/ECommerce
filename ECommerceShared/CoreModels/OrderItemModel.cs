using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShared.CoreModels
{
    public  class OrderItemModel
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal PriceEach { get; set; }
    }
}
