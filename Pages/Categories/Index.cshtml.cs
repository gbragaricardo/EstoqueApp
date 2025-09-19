using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly AppDataContext _context;
        public IndexModel(AppDataContext context)
        => _context = context;


        public IList<Category> Categories { get; set; } = [];
        public async Task OnGetAsync()
        {
            Categories = await _context.Categories
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
