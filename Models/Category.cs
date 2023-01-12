namespace FreeStaticPages.Models
{
    public class Category
    {
        public int Id { get; set; }
        public Link? Link { get; set; } = new Link();
        public List<Item>? Items { get; set; }
        public string? Name { get; set; }
    }
}