using EstoqueApp.Data;
using EstoqueApp.Extensions;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Pages.Movements
{
    public class EditMovementModel : PageModel
    {
        private readonly AppDataContext _context;

        public EditMovementModel(AppDataContext context)
            => _context = context;

        [BindProperty]
        public StockMovement Movement { get; set; } = null;
        public SelectList Products { get; set; } = null!;
        public SelectList CostCenters { get; set; } = null!;
        public SelectList Types { get; set; } = null!;


        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movement = await _context.StockMovements.FirstOrDefaultAsync(m => m.Id == id);

            if (Movement == null)
                return NotFound();

            Products = new SelectList(_context.Products, "Id", "Name", Movement.ProductId);
            CostCenters = new SelectList(_context.CostCenters, "Id", "Name", Movement.CostCenterId);
            Types = EnumExtensions.ToSelectList<MovementType>();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Products = new SelectList(_context.StockMovements, "Id", "Name", Movement.ProductId);
                CostCenters = new SelectList(_context.CostCenters, "Id", "Name", Movement.CostCenterId);
                Types = EnumExtensions.ToSelectList<MovementType>();

                return Page();
            }


            var movementInDb = await _context.StockMovements.FindAsync(Movement.Id);

            if (movementInDb == null)
                return NotFound();

            // Atualiza só os campos editáveis
            movementInDb.Type = Movement.Type;
            movementInDb.UnitCost = Movement.UnitCost;
            movementInDb.Quantity = Movement.Quantity;
            movementInDb.Notes = Movement.Notes;
            movementInDb.ProductId = Movement.ProductId;
            movementInDb.CostCenterId = Movement.CostCenterId;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
