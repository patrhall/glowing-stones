using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Ruby.Models
{
    public class Product
    {
        private string _currency = "usd";
        private float _rate = 1;
        private double _discount = 1;

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
        public byte[] ThumbImage { get; set; }
        [NotMapped]
        public string Type { get { return Name + " " + Color; } }
        [NotMapped]
        public double? ConvertedPrice
        {
            get
            {
                if (Price != null)
                {
                    var val = (Price.Value * _rate);
                    var dis = _discount == 0 ? 0 : val * (_discount/100);
                    var sum = val - dis;
                    return Math.Round(sum, 2);
                }
                else
                {
                    return null;
                }
            }
        }
        [NotMapped]
        public string Currency
        {
            get
            {
                return "(" + _currency.ToUpper() + ")";
            }
        }
        public void SetConvertedPriceAndCurrency(string currency = "usd", float rate = 1, double discount = 0)
        {
            _currency = currency;
            _rate = rate;
            _discount = discount;
        }
        [NotMapped]
        public string ImageFile { get { return ProductGetImageFroByte(Image); } }
        [NotMapped]
        public string ThumbImageFile { get { return ProductGetImageFroByte(ThumbImage); } }
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