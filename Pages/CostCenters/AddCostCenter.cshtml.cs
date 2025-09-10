using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EstoqueApp.Pages.CostCenters
{
    public class AddCostCenterModel : PageModel
    {
        private readonly AppDataContext _context;
        public AddCostCenterModel(AppDataContext context)
        => _context = context;


        [BindProperty]
        public CostCenter CostCenter { get; set; } = new CostCenter { Name = null, Code = null };
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CostCenters.Add(CostCenter);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
