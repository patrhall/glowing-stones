using Ruby.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace Ruby.Extensions
{
    public static class ProductExtension
    {
        public static IQueryable<Product> AndCurrencyRate(this IQueryable<Product> products, string currency, float rate, double discount)
        {
            products.ForEach(p => p.SetConvertedPriceAndCurrency(currency, rate, discount));
            return products;
        }

        public static Product SetByDisplayName(this Product product, string name, string value)
        {
            PropertyInfo[] properties = typeof(Product).GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                object[] attrs = prop.GetCustomAttributes(true);

                foreach (object attr in attrs)
                {
                    DisplayNameAttribute displayName = attr as DisplayNameAttribute;
                    if (displayName != null)
                    {
                        if (name == displayName.DisplayName)
                        {
                            object propValue = !string.IsNullOrWhiteSpace(value) ? value : null;
                            if(prop.PropertyType == typeof(int?))
                            {
                                if (!string.IsNullOrWhiteSpace(value))
                                {
                                    propValue = int.Parse(value);
                                }
                            }
                            if (prop.PropertyType == typeof(double?))
                            {
                                if (!string.IsNullOrWhiteSpace(value))
                                {
                                    propValue = Double.Parse(value);
                                }
                            }

                            prop.SetValue(product, propValue, null);
                            return product;
                        }
                    }
                }
            }

            return product;
        }
    }
}