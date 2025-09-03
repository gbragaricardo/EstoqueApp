namespace EstoqueApp.Models
{
    public class Product
    {

        //Gerado automaticamente
        public int Id { get; set; }

        public string Name { get; set; }

        //Nulable para que seja opcional
        public string? Description { get; set; }

        //String para que possa conter letras
        public string Sku { get; set; }

        //Estoque consolidado atual
        public int CurrentStock { get; set; }

        //Para que seja possivel desativar sem precisar excluir
        public bool IsActive { get; set; }

        //Categoria do produto, *caso possa haver varias deverá ser uma lista*
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<StockMovement>  Movements { get; set; }
    }
}
