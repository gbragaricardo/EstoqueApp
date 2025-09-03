namespace EstoqueApp.Models
{
    public class Category
    {
        //Gerado automaticamente
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}
