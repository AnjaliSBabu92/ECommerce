using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShared.CoreModels
{
    public  class OrderDetailsModel
    {
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public List<OrderItemModel> OrderItems { get; set; } = new List<OrderItemModel>();
        public DateTime DeliveryExpected { get; set; }
    }
}
