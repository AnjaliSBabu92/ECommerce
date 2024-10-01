using ECommerceIAbstract;
using ECommerceShared.CoreModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System.Data;
using System.Data.SqlClient;

namespace ECommerceDataRepository
{
    public class ECommerceData : IDatalayerRepo
    {
        //private readonly string _connectionString;

        //public ECommerceData(IConfiguration configuration)
        //{
        //    _connectionString = configuration.GetConnectionString("DefaultConnection");
        //}
        private readonly IConfiguration _configuration;
        private readonly ILogger<ECommerceData> _logger;

        public ECommerceData(IConfiguration configuration, ILogger<ECommerceData> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<(CustomerDetails customerDetails, OrderDetailsModel orderDetails)> GetLatestOrder(string customerId, string userEmail)
        {
            CustomerDetails customerDetails = null;
            OrderDetailsModel orderDetails = null;
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("HRMS_GetCustomerLatestOrder", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@User", userEmail);
                        command.Parameters.AddWithValue("@CustomerId", customerId);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {
                                customerDetails = new CustomerDetails
                                {
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString()
                                };

                                orderDetails = new OrderDetailsModel
                                {
                                    OrderNumber = (int)reader["OrderNumber"],
                                    OrderDate = (DateTime)reader["OrderDate"],
                                    DeliveryAddress = reader["DeliveryAddress"].ToString(),
                                    DeliveryExpected = (DateTime)reader["DeliveryExpected"]
                                };

                                // Assuming order items are returned as a separate result set
                                var orderItems = new List<OrderItemModel>();
                                if (await reader.NextResultAsync())
                                {
                                    while (reader.Read())
                                    {
                                        orderItems.Add(new OrderItemModel
                                        {
                                            Product = reader["Product"].ToString(),
                                            Quantity = (int)reader["Quantity"],
                                            PriceEach = (decimal)reader["PriceEach"]
                                        });
                                    }
                                }
                                orderDetails.OrderItems = orderItems;
                            }
                        }
                    }
                }

                return (customerDetails, orderDetails);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the latest order for customer {CustomerId}", customerId);
                throw; // Re-throw the exception if needed, or handle it as required.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw;
            }
        }
    }
}