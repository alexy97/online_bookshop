using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_bookshop_API.Model.Requests
{
    public class BookRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Summary { get; set; }
        public string Isbn { get; set; }
        public byte[] Image { get; set; }
        public string Author { get; set; }
        public DateTime Year { get; set; }
        public decimal Price { get; set; }
        public int NumPages { get; set; }
        public int Quantity { get; set; }
        public int NumPurchases { get; set; }
    }
}
