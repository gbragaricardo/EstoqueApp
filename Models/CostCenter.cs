using EstoqueApp.Models;

public class CostCenter
{
    public int Id { get; set; }
    public required string Name { get; set; }

    // Se quiser, código único para relatórios/ERP
    public string? Code { get; set; }

    public List<StockMovement> Movements { get; set; } = new();
}