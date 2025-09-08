using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly AppDataContext _context;
        public IndexModel(AppDataContext context)
        => _context = context;

        public IList<Product> Products { get; set; } = [];

        public async Task OnGetAsync()
        {
            Products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }
    }
}
