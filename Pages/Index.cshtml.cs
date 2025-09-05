using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EstoqueApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        using (var context = new AppDataContext())
        {

            // CREATE

            //var escolar = new Category
            //{
            //    Name = "Escolar",
            //};

            //var lapis = new Product
            //{
            //    Name = "Lapis",
            //    UnitPrice = 1m,
            //    Description = "Lapis HB BIC",
            //    Sku = "XXXXXX",
            //    CurrentStock = 10,
            //    IsActive = true,
            //    Category = escolar,
            //};

            //var products = context.Products;
            //products.Add(lapis);
            //context.SaveChanges();




            // UPDATE

            //var products = context.Products;

            //var lapis = products.FirstOrDefault(p => p.Name == "Lapis");

            //if (lapis != null)
            //    lapis.CurrentStock = 100;

            //context.SaveChanges();



            //READ

            //var products = context.Products;

            //var lapis = products.FirstOrDefault(p => p.Name == "Lapis");

            //if (lapis != null)
            //    Console.WriteLine($"{lapis.Name}, Quantidade: {lapis.CurrentStock}");

            // DELETE

            //var products = context.Products;

            //var lapis = products.FirstOrDefault(p => p.Name == "Lapis");

            //if (lapis != null)
            //    products.Remove(lapis);

            //context.SaveChanges();

        }



    }
}
