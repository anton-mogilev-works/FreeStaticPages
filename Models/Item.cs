namespace FreeStaticPages.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? Name {get; set; }
        public Link? Link { get; set; } = new Link();
        public List<Image> Images { get; set; } = new List<Image>();
        public decimal Price { get; set; } = decimal.Zero;
        public string? Description { get; set; }

        public override string ToString()
        {
            string item = "Item: \n";
            item += "Id:   " + Id.ToString() + "\n";
            item += "Name: " + Name;

            return item;
        }

    }
}