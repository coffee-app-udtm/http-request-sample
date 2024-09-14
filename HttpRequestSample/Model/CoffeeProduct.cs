using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestSample.Request
{
    public class CoffeeProduct
    {
        public string id { get; set; }
        public string name { get; set; }
        public double price { get; set; }

        public string description { get; set; }

        public int status { get; set; }
        public string image { get; set; }
        public int category_id { get; set; }

        public string category_name { get; set; }
    }
}
