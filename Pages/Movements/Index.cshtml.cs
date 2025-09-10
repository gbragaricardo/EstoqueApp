using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Pages.Movements
{
    public class IndexModel : PageModel
    {
        private readonly AppDataContext _context;
        public IndexModel(AppDataContext context)
            =>  _context = context;

        [BindProperty]
        public IList<StockMovement> Movements { get; set; } = [];
        public async Task OnGetAsync()
        {
            Movements = await _context.StockMovements
                .Include(m => m.Product)
                .Include(m => m.CostCenter)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
