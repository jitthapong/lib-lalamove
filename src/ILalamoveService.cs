using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lalamove
{
    public interface ILalamoveService
    {
        Task<QuotationResult> GetQuotationAsync(Quotation quotation);

        Task<OrderResult> PlaceOrderAsync(Quotation quotation);

        Task<OrderStatusResult> PlaceOrderWithStatusAsync(Quotation quotation, int timeoutSeconds = 60);

        Task<OrderDriverDetail> PlaceOrderWithDriverDetailAsync(Quotation quotation, int timeoutSeconds = 60);

        Task<OrderStatusResult> GetOrderStatusAsync(string orderId);

        Task CancleOrderAsync(string orderId);

        Task<DriverDetail> GetDriverDetailAsync(string orderId, string driverId);

        Task<DeliverLocation> GetDriverLocationAsync(string orderId, string driverId);
    }
}
