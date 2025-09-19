using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Pages.Products
{
    public class OldIndexModel : PageModel
    {
        private readonly AppDataContext _context;
        public OldIndexModel(AppDataContext context)
        => _context = context;

        public IList<Product> Products { get; set; } = [];
        public SelectList Categories { get; set; } = null!;


        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? IsActive { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public int? MinStock { get; set; }

        public async Task OnGetAsync(int skip = 5, int take = 5)
        {
            // Base da busca
            var query = _context.Products
                .Include(p => p.Category)
                .AsQueryable();

            //Filtro: TextBox
            if (string.IsNullOrEmpty(SearchTerm) == false)
                query = query.Where(p => p.Name.Contains(SearchTerm));
            

            // Filtro: categoria
            if (CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == CategoryId.Value);

            // Filtro: ativo/inativo
            if (IsActive.HasValue)
                query = query.Where(p => p.IsActive == IsActive.Value);

            //// Filtro: estoque mínimo
            //if (MinStock.HasValue)
            //{
            //    query = query.Where(p => p.CurrentStock >= MinStock.Value);
            //}

            Products = await query
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            // Preenche lista de categorias para o filtro
            Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
