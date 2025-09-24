using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Pages.CostCenters
{
    public class IndexModel : PageModel
    {
        private readonly AppDataContext _context;

        [BindProperty (SupportsGet = true)]
        public int SelectedCostCenterId { get; set; }
        public SelectList CostCenters { get; set; }
        public IList<Product> Products{ get; set; } = new List<Product>();
        public IList<StockByCostCenter> StockByCCs { get; set; } = new List<StockByCostCenter>();

        public IndexModel(AppDataContext context)
            => _context = context;


        public async Task OnGetAsync(int skip = 0, int take = 5)
        {
            var query = _context.StockByCostCenters
                .Where(s => s.CostCenterId == SelectedCostCenterId)
                .Include(s => s.Product)
                .AsQueryable();

            StockByCCs = query
                .Skip(skip)
                .Take(take)
                .ToList();

            CostCenters = new SelectList(await _context.CostCenters.ToListAsync(), "Id", "Name");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var costCenter = await _context.CostCenters.FindAsync(id);

            if (costCenter != null)
            {
                _context.CostCenters.Remove(costCenter);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
