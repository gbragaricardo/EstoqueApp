namespace EstoqueApp.Models
{
    public class StockMovement
    {

        //Gerado automaticamente
        public int Id { get; set; }

        //Produto(Id) que está sendo movimentado
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public decimal? UnitCost { get; set; }   // decimal(18,2)

        // Enum para facilitar a seleção do tipo In/Out
        public required MovementType Type { get; set; }

        public int Quantity { get; set; } = 0;

        //Criado no momento em que é instanciado
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //Nulable para que seja opcional
        public string? Notes { get; set; }

        // pode ser null (para entradas ou saídas sem Centro de custo)
        public int? CostCenterId { get; set; }        
        public CostCenter? CostCenter { get; set; }

    }

    // Enum para facilitar a seleção do tipo In/Out
    public enum MovementType
    {
        In = 1,
        Out = 2
    }
}
