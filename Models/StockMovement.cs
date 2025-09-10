using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EstoqueApp.Models
{
    public class StockMovement
    {

        //Gerado automaticamente
        public int Id { get; set; }

        // Enum para facilitar a seleção do tipo In/Out
        public required MovementType Type { get; set; }

        public required decimal UnitCost { get; set; }   // decimal(18,2)

        public required int Quantity { get; set; }

        //Criado no momento em que é instanciado
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        //Nulable para que seja opcional
        public string? Notes { get; set; }

        //Produto(Id) que está sendo movimentado
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        // pode ser null (para entradas ou saídas sem Centro de custo)
        public int? CostCenterId { get; set; }        
        public CostCenter? CostCenter { get; set; }
    }

    // Enum para facilitar a seleção do tipo In/Out
    public enum MovementType
    {
        [Display(Name = "Entrada")]
        In = 1,
        [Display(Name = "Saída")]
        Out = 2
    }
}
