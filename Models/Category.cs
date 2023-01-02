namespace FreeStaticPages.Models
{
    public class Category
    {
        public int Id { get; set; }
        public Link? Link { get; set; } = new Link();
        public string? Name { get; set; }
    }
}