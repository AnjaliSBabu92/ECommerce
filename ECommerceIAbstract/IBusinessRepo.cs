using Azure.Core;
using ECommerceShared.CoreModels;

namespace ECommerceIAbstract
{
    public interface IBusinessRepo
    {
        Task<CustomerResponseModel?> GetLatestOrder(CustomerRequestModel request);
    }
}
