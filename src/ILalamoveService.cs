using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lalamove
{
    public interface ILalamoveService
    {
        Task<QuotationResult> GetQuotationAsync(Quotation quotation);

        Task<DriverDetail> PlaceOrderWithDriverDetailAsync(OrderRequest order, int timeoutSeconds = 60);

        Task<OrderResult> GetOrderStatusAsync(string orderId);

        Task CancleOrderAsync(string orderId);

        Task<DriverDetail> GetDriverDetailAsync(string orderId, string driverId);
    }
}
