namespace FreeStaticPages.Models
{
    public class StaticPage
    {
        public int Id { get; set; }
        // public int LinkId { get; set; }
        public Link Link { get; set; } = new Link();
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<Image>? Images { get; set; } = new List<Image>();

        public override string ToString()
        {
            string page = "Static Page: \n\n";

            page += "Id:    " + Id.ToString() + "\n";
            page += "Name:  " + Name + "\n";
            page += "Path:  " + Link.Path + "\n";
            page += "Images: \n";
            if(Images is not null)
            {
                foreach(Image image in Images)
                {
                    page += "  Image Id:   " + image.Id + "\n";
                    page += "  Image Path: " + image.Link.Path + "\n";
                }
            }
            else {
                page += "Images not found";
            }

            return page;
        }
    }
}
