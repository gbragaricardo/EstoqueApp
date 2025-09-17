using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EstoqueApp.Models
{
    public class StockMovement
    {

        //Gerado automaticamente
        public int Id { get; set; }

        // Enum para facilitar a seleção do tipo In/Out
        public MovementType Type { get; set; }

        public decimal Quantity { get; set; }

        //Criado no momento em que é instanciado
        public DateTime Date { get; set; } = DateTime.UtcNow;

        //Nulable para que seja opcional
        public string? Description { get; set; }

        //Produto(Id) que está sendo movimentado
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        // Origem da movimentação (null em Entrada)
        public int? OriginCostCenterId { get; set; }
        public CostCenter? OriginCostCenter { get; set; }

        // Destino da movimentação (null em Saída)
        public int? DestinationCostCenterId { get; set; }
        public CostCenter? DestinationCostCenter { get; set; }
    }

    // Enum para facilitar a seleção do tipo In/Out
    public enum MovementType
    {
        [Display(Name = "Entrada")]
        Entry = 1,

        [Display(Name = "Saída")]
        Exit = 2,

        [Display(Name = "Transferência")]
        Transfer = 3,
    }
}
