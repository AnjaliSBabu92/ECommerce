using ECommerceShared.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceIAbstract
{
    public interface IDatalayerRepo
    {
      
    Task<(CustomerDetails customerDetails, OrderDetailsModel orderDetails)> GetLatestOrder(string customerId, string userEmail);
    }
}
