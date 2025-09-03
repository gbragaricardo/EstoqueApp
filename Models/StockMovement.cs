namespace EstoqueApp.Models
{
    public class StockMovement
    {

        //Gerado automaticamente
        public int Id { get; set; }

        public int ProductId { get; set; }

        // Enum para facilitar a seleção do tipo In/Out
        public MovementType Type { get; set; }

        public int Quantity { get; set; }

        //Criado no momento em que é instanciado
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //Nulable para que seja opcional
        public string? Notes { get; set; }

    }

    // Enum para facilitar a seleção do tipo In/Out
    public enum MovementType
    {
        In = 1,
        Out = 2
    }
}
