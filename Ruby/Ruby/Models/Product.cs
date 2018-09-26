using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ruby.Models
{
    public class Product
    {
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Price")]
        public double? Price { get; set; }
        [DisplayName("Lot number")]
        public string InternalId { get; set; }
        [DisplayName("Color")]
        public string Color { get; set; }
        [DisplayName("Weight (carat)")]
        public double? Weight { get; set; }
        [DisplayName("Size")]
        public string Size { get; set; }
        [DisplayName("Clarity")]
        public string Clean { get; set; }
        [DisplayName("Pieces")]
        public int? Pieces { get; set; }
        [DisplayName("Shape")]
        public string Shape { get; set; }
        [DisplayName("Cut")]
        public string Cut { get; set; }
        [DisplayName("Treatment")]
        public string Treatment { get; set; }
        [DisplayName("Origin")]
        public string Origin { get; set; }
        [DisplayName("Comment")]
        public string Comment { get; set; }
        [NotMapped]
        public string Type { get { return Name + " " + Color; }}
        [NotMapped]
        public string Description { get { return ProductDescription(this); } }

        private string ProductDescription(Product product)
        {
            var description = "";
            if(!string.IsNullOrWhiteSpace(product.Weight.ToString()))
            {
                description += $"<label>{product.Weight.ToString()}</label><br/>";
            }
            if (!string.IsNullOrWhiteSpace(product.Size))
            {
                description += $"<label>{product.Size}</label><br/>";
            }
            if (!string.IsNullOrWhiteSpace(product.Clean))
            {
                description += $"<label>{product.Clean}</label><br/>";
            }
            //if show price
            if (!string.IsNullOrWhiteSpace(product.Price.ToString()))
            {
                description += $"<label>{product.Price.ToString()}</label><br/>";
            }

            return description;
        }
    }
}