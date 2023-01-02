namespace FreeStaticPages.Models
{
    public class Item
    {
        public int Id { get; set; }
        public Category? Category { get; set; }
        public string? Name {get; set; }
        public Link? Link { get; set; }
        public string? Description { get; set; }

    }
}