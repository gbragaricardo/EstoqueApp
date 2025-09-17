namespace EstoqueApp.Models
{
    public class UnitOfMeasure
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public required string Abbreviation { get; set; }

        public List<Product> Products { get; set; } = new ();
    }

}
