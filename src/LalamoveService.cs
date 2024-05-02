using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lalamove
{
    public class LalamoveService : ILalamoveService
    {
        LalamoveConfig _config;
        JsonSerializerSettings _serializerSettings;

        public LalamoveService(LalamoveConfig config)
        {
            _config = config;

            _serializerSettings = new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public async Task<QuotationResult> GetQuotationAsync(Quotation quotation)
        {
            var path = "/v2/quotations";
            var uri = new UriBuilder($"{_config.BaseUrl}{path}");

            var httpClient = GetHttpClient();
            SetRequestHeader(quotation, path, "POST", httpClient);

            var content = CreateStringContent(quotation);
            var response = await httpClient.PostAsync(uri.ToString(), content);
            var result = await GetResponse<QuotationResult>(response);
            quotation.QuotedTotalFee = new Pricing()
            {
                Amount = result.TotalFee,
                Currency = result.TotalFeeCurrency
            };
            return result;
        }

        public async Task<OrderResult> PlaceOrderAsync(Quotation quotation)
        {
            var path = "/v2/orders";
            var uri = new UriBuilder($"{_config.BaseUrl}{path}");

            var httpClient = GetHttpClient();
            SetRequestHeader(quotation, path, "POST", httpClient);

            var content = CreateStringContent(quotation);
            var response = await httpClient.PostAsync(uri.ToString(), content);
            var result = await GetResponse<OrderResult>(response);
            return result;
        }

        public async Task<OrderStatusResult> GetOrderStatusAsync(string orderId)
        {
            var path = $"/v2/orders/{orderId}";
            var uri = new UriBuilder($"{_config.BaseUrl}{path}");

            var httpClient = GetHttpClient();
            object emptyBody = null;
            SetRequestHeader(emptyBody, path, "GET", httpClient);

            var response = await httpClient.GetAsync(uri.ToString());
            var result = await GetResponse<OrderStatusResult>(response);
            return result;
        }

        public async Task CancleOrderAsync(string orderId)
        {
            var path = $"/v2/orders/{orderId}/cancel";
            var uri = new UriBuilder($"{_config.BaseUrl}{path}");

            var httpClient = GetHttpClient();
            object emptyBody = null;
            SetRequestHeader(emptyBody, path, "PUT", httpClient);

            await httpClient.PutAsync(uri.ToString(), null);
        }

        public async Task<DriverDetail> GetDriverDetailAsync(string orderId, string driverId)
        {
            var path = $"/v2/orders/{orderId}/drivers/{driverId}";
            var uri = new UriBuilder($"{_config.BaseUrl}{path}");

            var httpClient = GetHttpClient();
            object emptyBody = null;
            SetRequestHeader(emptyBody, path, "GET", httpClient);

            var response = await httpClient.GetAsync(uri.ToString());
            var result = await GetResponse<DriverDetail>(response);
            return result;
        }

        public async Task<DeliverLocation> GetDriverLocationAsync(string orderId, string driverId)
        {
            var path = $"/v2/orders/{orderId}/drivers/{driverId}/location";
            var uri = new UriBuilder($"{_config.BaseUrl}{path}");

            var httpClient = GetHttpClient();
            object emptyBody = null;
            SetRequestHeader(emptyBody, path, "GET", httpClient);

            var response = await httpClient.GetAsync(uri.ToString());
            var result = await GetResponse<DeliverLocation>(response);
            return result;
        }

        private void SetRequestHeader<T>(T payload, string path, string method, HttpClient httpClient)
        {
            var body = "";
            if (payload != null)
                body = JsonConvert.SerializeObject(payload, _serializerSettings);
            var authorize = CreateAuthorizeHeader(path, method, body);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorize);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-LLM-Country", _config.CountryCode);
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Request-ID", Guid.NewGuid().ToString());
        }

        public string CreateAuthorizeHeader(string path, string method, string body)
        {
            var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            var rawSignature = $"{timestamp}\r\n{method}\r\n{path}\r\n\r\n{body}";

            HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_config.Secret));
            hmac.Initialize();
            byte[] buffer = Encoding.UTF8.GetBytes(rawSignature);
            string signature = BitConverter.ToString(hmac.ComputeHash(buffer)).Replace("-", "").ToLower();

            var authorization = $"hmac {_config.ApiKey}:{timestamp}:{signature}";
            return authorization;
        }

        async Task<T> GetResponse<T>(HttpResponseMessage response)
        {
            await HandleResponse(response);
            var result = await response.Content.ReadAsStringAsync();
            return await Task.Run(() =>
                JsonConvert.DeserializeObject<T>(result, _serializerSettings));
        }

        StringContent CreateStringContent<T>(T data)
        {
            StringContent content = null;
            if (data != null)
            {
                var json = JsonConvert.SerializeObject(data, _serializerSettings);
                content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            return content;
        }

        HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.Timeout = TimeSpan.FromSeconds(60);
            return httpClient;
        }

        async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(content))
                    content = response.ReasonPhrase;
                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    throw new HttpRequestExceptionEx(response.StatusCode, JsonConvert.DeserializeObject<ApiError>(content).Message);
                }
                throw new HttpRequestExceptionEx(response.StatusCode, content);
            }
        }

        public async Task<OrderStatusResult> PlaceOrderWithStatusAsync(Quotation quotation, int timeoutSeconds)
        {
            var path = "/v2/orders";
            var uri = new UriBuilder($"{_config.BaseUrl}{path}");

            var httpClient = GetHttpClient();
            SetRequestHeader(quotation, path, "POST", httpClient);

            var content = CreateStringContent(quotation);
            var response = await httpClient.PostAsync(uri.ToString(), content);
            var orderResult = await GetResponse<OrderResult>(response);

            DateTime timeToStop = DateTime.Now;
            timeToStop = timeToStop.AddSeconds(timeoutSeconds);

            OrderStatusResult statusResult = new OrderStatusResult()
            {
                CustomerOrderId = orderResult.CustomerOrderId,
                OrderRef = orderResult.OrderRef
            };

            while (true)
            {
                var timeout = timeToStop.Subtract(DateTime.Now);
                if ((int)timeout.TotalSeconds <= 0)
                {
                    break;
                }

                statusResult = await GetOrderStatusAsync(orderResult.OrderRef);
                if (!statusResult.Status.Equals("ASSIGNING_DRIVER"))
                {
                    break;
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
            return statusResult;
        }

        public async Task<OrderDriverDetail> PlaceOrderWithDriverDetailAsync(Quotation quotation, int timeoutSeconds = 60)
        {
            var path = "/v2/orders";
            var uri = new UriBuilder($"{_config.BaseUrl}{path}");

            var httpClient = GetHttpClient();
            SetRequestHeader(quotation, path, "POST", httpClient);

            var content = CreateStringContent(quotation);
            var response = await httpClient.PostAsync(uri.ToString(), content);
            var orderResult = await GetResponse<OrderResult>(response);

            DateTime timeToStop = DateTime.Now;
            timeToStop = timeToStop.AddSeconds(timeoutSeconds);

            OrderDriverDetail orderDriverDetail = new OrderDriverDetail()
            {
                OrderRef = orderResult.OrderRef,
                CustomerOrderId = orderResult.CustomerOrderId
            };
            while (true)
            {
                var timeout = timeToStop.Subtract(DateTime.Now);
                if ((int)timeout.TotalSeconds <= 0)
                {
                    break;
                }

                var orderStatus = await GetOrderStatusAsync(orderResult.OrderRef);
                orderDriverDetail.Status = orderStatus.Status;
                orderDriverDetail.DriverId = orderStatus.DriverId;

                if (!orderStatus.Status.Equals("ASSIGNING_DRIVER"))
                {
                    if (orderStatus.Status.Equals("ON_GOING"))
                    {
                        var driverDetail = await GetDriverDetailAsync(orderResult.OrderRef, orderStatus.DriverId);
                        orderDriverDetail.Name = driverDetail.Name;
                        orderDriverDetail.PlateNumber = driverDetail.PlateNumber;
                        orderDriverDetail.Phone = driverDetail.Phone;
                        orderDriverDetail.Photo = driverDetail.Photo;
                    }
                    break;
                }
                else
                {
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
            return orderDriverDetail;
        }
    }
}
