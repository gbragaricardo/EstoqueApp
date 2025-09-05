namespace EstoqueApp.Models
{
    public class CostCenter
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        // Se quiser, código único para relatórios/ERP
        public required string Code { get; set; }

        public List<StockMovement> Movements { get; set; } = new();
    }

}