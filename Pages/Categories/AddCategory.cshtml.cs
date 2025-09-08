using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Pages.Categories
{
    public class AddCategoryModel : PageModel
    {
        private readonly AppDataContext _context;
        public AddCategoryModel(AppDataContext context)
        => _context = context;


        [BindProperty]
        public Category Category { get; set; } = new Category { Name = null};
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
