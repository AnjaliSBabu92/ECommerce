using ECommerceIAbstract;
using ECommerceShared.CoreModels;
using Microsoft.Extensions.Logging;

namespace ECommerceBusinessRepository
{
    public class ECommerceBusinessRepo : IBusinessRepo
    {
        //Task<CustomerResponseModel?> IBusinessRepo.GetLatestOrder(CustomerRequestModel request)
        //{
        //    throw new NotImplementedException();
        //}

        private readonly IDatalayerRepo _dataLayerRepo;
        private readonly ILogger<ECommerceBusinessRepo> _logger;

        public ECommerceBusinessRepo(IDatalayerRepo dataLayerRepo, ILogger<ECommerceBusinessRepo> logger)
        {
            _dataLayerRepo = dataLayerRepo;
            _logger = logger;
        }

        public async Task<CustomerResponseModel?> GetLatestOrder(CustomerRequestModel request)
        {
            try
            {
                (CustomerDetails customerDetails, OrderDetailsModel orderDetails) = await _dataLayerRepo.GetLatestOrder(request.CustomerId, request.User);

                if (customerDetails == null)
                {
                    return null; // Customer not found or email mismatch
                }

                // Create the response object
                return new CustomerResponseModel
                {
                    Customer = new CustomerDetails
                    {
                        FirstName = customerDetails.FirstName,
                        LastName = customerDetails.LastName,
                    },
                    Order = orderDetails != null ? new OrderDetailsModel
                    {
                        OrderNumber = orderDetails.OrderNumber,
                        OrderDate = orderDetails.OrderDate,
                        DeliveryAddress = orderDetails.DeliveryAddress,
                        OrderItems = orderDetails.OrderItems,
                        DeliveryExpected = orderDetails.DeliveryExpected
                    } : null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the latest order for customer {CustomerId}", request.CustomerId);
                throw;
            }
        }

    }
}
