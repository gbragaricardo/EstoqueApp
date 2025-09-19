using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Pages.Products
{
    public class EditProductModel : PageModel
    {
        private readonly AppDataContext _context;

        public EditProductModel(AppDataContext context)
            => _context = context;

        [BindProperty]
        public Product Product { get; set; } = null;
        public SelectList Categories { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (Product == null)
                return NotFound();

            Categories = new SelectList(_context.Categories, "Id", "Name", Product.CategoryId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var productInDb = await _context.Products.FindAsync(Product.Id);

            if (productInDb == null)
                return NotFound();

            // Atualiza só os campos editáveis
            productInDb.Name = Product.Name;
            productInDb.Sku = Product.Sku;
            productInDb.UnitPrice = Product.UnitPrice;
            productInDb.CurrentStock = Product.CurrentStock;
            productInDb.CategoryId = Product.CategoryId;
            productInDb.IsActive = Product.IsActive;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
