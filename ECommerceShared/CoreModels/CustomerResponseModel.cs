using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShared.CoreModels
{
    public  class CustomerResponseModel
    {
        public CustomerDetails Customer { get; set; }
        public OrderDetailsModel Order { get; set; }
    }
}
