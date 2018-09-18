using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ruby.Models
{
    public class ProductType
    {
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}