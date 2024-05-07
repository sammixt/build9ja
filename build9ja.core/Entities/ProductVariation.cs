namespace build9ja.core.Entities
{
    public class ProductVariation : BaseEntity
    {
        public ProductVariation()
        {
            
        }
        public string Variation { get; set; }
        public string SKU { get; set; }
        public string IMEI { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public Nullable<DateTime> SaleStartDate {get; set; }
        public Nullable<DateTime> SaleEndDate {get; set; }

        public Nullable<long> ProductId { get; set; }
        public virtual Product Product { get; set; }
       
    }
}