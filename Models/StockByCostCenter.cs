namespace EstoqueApp.Models
{
    public class StockByCostCenter
    {
        public int Id { get; set; }

        // Relacionamento com Product
        public int? ProductId { get; set; }
        public Product? Product { get; set; }

        // Relacionamento com CostCenter
        public int? CostCenterId { get; set; }
        public CostCenter? CostCenter { get; set; }

        // Quantidade de estoque do produto nesse setor
        public decimal Quantity { get; set; }

        // (Opcional) Última atualização, se quiser ter histórico básico
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
