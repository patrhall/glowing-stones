namespace Ruby.Models
{
    public class RateObject
    {
        public Rates rates { get; set; }
        public int code { get; set; }
    }

    public class Rates
    {
        public USDEUR USDEUR { get; set; }
        public USDSEK USDSEK { get; set; }
    }

    public class USDEUR
    {
        public float rate { get; set; }
        public long timestamp { get; set; }
    }

    public class USDSEK
    {
        public float rate { get; set; }
        public long timestamp { get; set; }
    }

}