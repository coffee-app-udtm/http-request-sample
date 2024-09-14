using HttpRequestSample.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestSample.Request
{
    internal class CoffeeProductRequest
    {
        public async Task<List<CoffeeProduct>> getCoffeeProductsAsync()
        {
            string url = "http://localhost:5500/product";

            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetStringAsync(url);

                JObject jsonResponse = JObject.Parse(response);
                JArray dataArray = (JArray)jsonResponse["data"];

                List<CoffeeProduct> products = dataArray.ToObject<List<CoffeeProduct>>();


                return products;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
