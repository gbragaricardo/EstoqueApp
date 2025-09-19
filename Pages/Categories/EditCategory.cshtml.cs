using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EstoqueApp.Pages.Categories
{
    public class EditCategoryModel : PageModel
    {
        public readonly AppDataContext _context;
        public EditCategoryModel(AppDataContext context)
            => _context = context;

        [BindProperty]
        public Category Category { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (Category == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var categoryInDb = await _context.Categories.FindAsync(Category.Id);

            if (categoryInDb == null)
                return NotFound();

            categoryInDb.Name = Category.Name;
            await _context.SaveChangesAsync();

            return RedirectToPage("index");
        }
    }
}
