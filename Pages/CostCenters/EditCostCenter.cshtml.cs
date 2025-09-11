using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Pages.CostCenters
{
    public class EditCostCenterModel : PageModel
    {
        public readonly AppDataContext _context;
        public EditCostCenterModel(AppDataContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CostCenter CostCenter { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            CostCenter = await _context.CostCenters.FirstOrDefaultAsync(c => c.Id == id);

            if (CostCenter == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var costCenterInDb = await _context.CostCenters.FindAsync(CostCenter.Id);

            if (costCenterInDb == null)
                return NotFound();

            costCenterInDb.Name = CostCenter.Name;
            costCenterInDb.Code = CostCenter.Code;

            await _context.SaveChangesAsync();
            return RedirectToPage("index");
        }
    }
}
