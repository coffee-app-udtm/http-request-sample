using HttpRequestSample.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestSample.Request
{
    internal class ProductRequest
    {
        public async Task<List<Product>> getProductsAsync()
        {
            string url = Constant.API_URL + "/products";

            try
            {
                HttpClient client = new HttpClient();

                string response = await client.GetStringAsync(url);

                // "Project" -> "Manage NuGet packages" -> "Search for "newtonsoft json". -> click "install".


                // Convert JSON ( response ) to List
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(response);

                return products;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> createProductAsync(Product product)
        {
            try
            {
                HttpClient client = new HttpClient();

                

                string json = JsonConvert.SerializeObject(product);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(Constant.API_URL + "/products", content);

                return response.IsSuccessStatusCode;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> updateProductAsync(Product product)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = Constant.API_URL + "/products/" + product.Id.ToString();
                string json = JsonConvert.SerializeObject(product);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(url, content);

                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> deleteProductAsync(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = Constant.API_URL + "/products/" + id.ToString();

                HttpResponseMessage response = await client.DeleteAsync(url);

                return response.IsSuccessStatusCode;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
