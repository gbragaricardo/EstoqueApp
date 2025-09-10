using EstoqueApp.Data;
using EstoqueApp.Extensions;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EstoqueApp.Pages.Movements
{
    public class AddMovementModel : PageModel
    {
        private readonly AppDataContext _context;
        public AddMovementModel(AppDataContext context)
            => _context = context;

        [BindProperty]
        public StockMovement Movement { get; set; } = null!;

        public SelectList Products { get; set; } = null!;
        public SelectList CostCenters { get; set; } = null!;
        public SelectList Types { get; set; } = null!;

        public void OnGet()
        {
            Products = new SelectList(_context.Products, "Id", "Name");
            CostCenters = new SelectList(_context.CostCenters, "Id", "Name");
            Types = EnumExtensions.ToSelectList<MovementType>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {
                Products = new SelectList(_context.Products, "Id", "Name");
                CostCenters = new SelectList(_context.CostCenters, "Id", "Name");
                return Page();
            }

            _context.StockMovements.Add(Movement);
            await _context.SaveChangesAsync();
            return RedirectToPage("index");
        }
    }
}
