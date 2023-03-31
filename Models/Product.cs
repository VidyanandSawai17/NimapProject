namespace NimapProject.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
