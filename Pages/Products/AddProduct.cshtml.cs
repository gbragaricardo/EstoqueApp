using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EstoqueApp.Pages.Products
{
    public class AddProductModel : PageModel
    {
        private readonly AppDataContext _context;
        public AddProductModel(AppDataContext context)
        => _context = context;

        [BindProperty]
        public Product Product { get; set; } = new Product { Name = null, Sku = null};

        public SelectList Categories { get; set; } = null!;

        public void OnGet()
        {
            Categories = new SelectList(_context.Categories, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = new SelectList(_context.Categories, "Id", "Name");
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
