namespace Domain.Entities
{
    public class ProductImage
    {
        public long Id { get; set; }

        public long ProductId { get; set; }

        public string Format {  get; set; } = string.Empty;

        public byte[] Image { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;

        public string? GetImage()
        {
            if (Image == null)
                return "/NotFoundImage.jpg";

            string img = Convert.ToBase64String(Image, 0, Image.Length);

            return $"data:{this.Format};base64,{img}";
        }
    }
}
