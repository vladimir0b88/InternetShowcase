namespace Domain.Entities
{
    public class ProductImage
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public string Format {  get; set; } = string.Empty;

        public byte[] Image { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;


        public static string GetImage(ProductImage? image)
        {
            if (image is null || image.Image is null)
                return "/img/emptyImage.jpg";


            string img = Convert.ToBase64String(image.Image, 0, image.Image.Length);

            return $"data:{image.Format};base64,{img}";
        }

        public string? GetImage()
        {
            if (Image == null)
                return "/NotFoundImage.jpg";

            string img = Convert.ToBase64String(Image, 0, Image.Length);

            return $"data:{this.Format};base64,{img}";
        }
    }
}
