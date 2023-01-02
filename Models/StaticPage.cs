namespace FreeStaticPages.Models
{
    public class StaticPage
    {
        public int Id { get; set; }
        public Link? Link { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Content { get; set; } = string.Empty;
        public List<Image>? Images { get; set; } = new List<Image>();

        public override string ToString()
        {
            string page = "";

            page += Id.ToString() + "\r\n";
            page += Name + "\r\n";
            page += Link?.Path + "\r\n";

            return page;
        }
    }
}
