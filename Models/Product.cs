namespace EstoqueApp.Models
{
    public class Product
    {
        //Gerado automaticamente
        public int Id { get; set; }

        public required string Name { get; set; }

        public decimal UnitPrice { get; set; }

        //Nulable para que seja opcional
        public string? Description { get; set; }

        //String para que possa conter letras
        public required string Sku { get; set; }

        //Estoque minimo para alerta
        public decimal MinStock { get; set; }

        //Estoque consolidado atual
        public decimal CurrentStock { get; set; }

        //Para que seja possivel desativar sem precisar excluir
        public bool IsActive { get; set; } = true;

        //Categoria do produto, *caso possa haver varias deverá ser uma lista*
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int UnitOfMeasureId { get; set; }
        public UnitOfMeasure? UnitOfMeasure { get; set; }

        public int CostCenterId { get; set; }
        public CostCenter? CostCenter { get; set; }

        public List<StockMovement> Movements { get; set; } = new ();
    }
}
