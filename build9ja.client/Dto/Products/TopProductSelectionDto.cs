namespace build9ja.client.Dto.Products
{
    public class TopProductSelectionDto {
        public TopProductSelectionDto()
        {
            TopSelection = new List<ProductDto>();
            NewArrival = new List<ProductDto>();
        }
        public List<ProductDto> TopSelection {get; set;}
        public List<ProductDto> NewArrival {get; set;}
    }

    public class FeaturedProductDto {
        public FeaturedProductDto()
        {
            FeatureA = new List<ProductDto>();
            FeatureB = new List<ProductDto>();
            FeatureC = new List<ProductDto>();
            FeatureD = new List<ProductDto>();
        }
        public List<ProductDto> FeatureA {get; set;}
        public List<ProductDto> FeatureB {get; set;}
        public List<ProductDto> FeatureC {get; set;}
        public List<ProductDto> FeatureD {get; set;}
    }


}