using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Pages.CostCenters
{
    public class OldIndexModel : PageModel
    {
        private readonly AppDataContext _context;
        public OldIndexModel(AppDataContext context)
        => _context = context;


        public IList<CostCenter> CostCenters { get; set; } = [];
        public async Task OnGetAsync()
        {
            CostCenters = await _context.CostCenters
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
