namespace FreeStaticPages.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string? Mime { get; set ; } = string.Empty;
        public Link Link { get; set; } = new Link();

    }
}