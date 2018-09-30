using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Ruby.Models
{
    public class Product
    {
        private static readonly ImageConverter _imageConverter = new ImageConverter();
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
        public byte[] Image { get; set; }
        [NotMapped]
        public string Type { get { return Name + " " + Color; }}
        [NotMapped]
        public string ImageFile { get { return ProductGetImageFroByte(Image); } }

        private string ProductGetImageFroByte(byte[] image)
        {
            if (image != null)
            {
                var base64 = Convert.ToBase64String(image);
                var imgSrc = string.Format("data:image/jpg;base64,{0}", base64);
                return imgSrc;
            }
            else
            {
                return null;
            }
        }
    }
}