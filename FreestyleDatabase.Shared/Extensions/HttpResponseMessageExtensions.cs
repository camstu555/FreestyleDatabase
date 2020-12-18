using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreestyleDatabase.Shared.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task CaptureFailedOperation(this HttpResponseMessage response)
        {
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch
            {
                var content = await response.Content.ReadAsStringAsync();

                throw new InvalidOperationException(content);
            }
        }
    }
}