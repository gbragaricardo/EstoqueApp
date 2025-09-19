namespace EstoqueApp.Models
{
    public class CostCenter
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        // Se quiser, código único para relatórios/ERP
        public required string Code { get; set; }

        public List<StockByCostCenter> StocksByCostCenter { get; set; } = new();

        public List<StockMovement> Movements { get; set; } = new();
    }

}
