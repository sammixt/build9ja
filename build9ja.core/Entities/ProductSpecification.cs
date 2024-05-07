namespace build9ja.core.Entities
{
    public class ProductSpecification : BaseEntity
    {
        public ProductSpecification()
        {
            
        }
        public string Dimension {get; set;}
        public int Weight {get; set;}
        public string Model {get; set;}
        public string ProductType {get; set;}
        public string MainMaterial {get; set;}
        public string ColorFamily {get; set;}

        public virtual Product Product {get; set;}
        public long ProductId {get; set;}
    }
}