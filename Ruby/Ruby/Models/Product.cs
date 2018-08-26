using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Intern Beteckning")]
        public string InternalId { get; set; }
        [DisplayName("Color")]
        public string Color { get; set; }
        [DisplayName("Weight (carat)")]
        public double? Weight { get; set; }
        [DisplayName("Size")]
        public string Size { get; set; }
        [DisplayName("Clean")]
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
        public byte Image { get; set; }
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get;set; }
    }
}