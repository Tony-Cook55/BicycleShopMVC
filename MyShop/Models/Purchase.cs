namespace MyShop.Models
{
    public class Purchase
    {

        // Primary Key
        public int Id { get; set; }

        // Foreign Key
        public int BicycleId { get; set; }

        // Navigation Property
        public Bicycle Bicycle { get; set;}

        public int? Quantity { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public decimal? Total {  get; set; }

    }
}
