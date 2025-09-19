using EstoqueApp.Data;
using EstoqueApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApp.Pages;

public class IndexModel : PageModel
{
    //private readonly ILogger<IndexModel> _logger;

    private readonly AppDataContext _context;

    public int TotalProducts { get; set; }

    public int LowStockTotal { get; set; }

    public int OutOfStock { get; set; }

    public decimal TotalValue { get; set; }

    //TESTE PARA GRAFICO 
    public List<LowStockViewModel> LowStockProducts { get; set; } = new();

    //TESTE PARA ALERTAS
    public List<StockAlertViewModel> Alerts { get; set; } = new();



    public IndexModel(AppDataContext context/*, ILogger<IndexModel> logger*/)
    {
        //_logger = logger;
        _context = context;
    }

    public void OnGet()
    {
        var products = _context.Products
            .AsNoTracking()
            .ToList();

        TotalProducts = products.Count();

        LowStockTotal = 0;
        OutOfStock = 0;
        TotalValue = 0;

        foreach (var product in products)
        {
            if ((product.MinStock * 1.5m) >= product.CurrentStock)
                LowStockTotal++;

            if (product.CurrentStock <= 0)
                OutOfStock++;

            TotalValue += (product.UnitPrice * product.CurrentStock);
        }

        //TESTE PARA GRAFICO
        LowStockProducts = _context.Products
        .Where(p => (p.MinStock * 1.5m) >= p.CurrentStock)
        .Select(p => new LowStockViewModel
        {
            Name = p.Name,
            CurrentStock = p.CurrentStock,
            MinStock = p.MinStock
        })
        .ToList();

        //TESTE PARA ALERTAS
        Alerts = _context.Products
            .Where(p => p.CurrentStock <= p.MinStock || p.CurrentStock == 0)
            .Select(p => new StockAlertViewModel
            {
                ProductName = p.Name,
                Message = p.CurrentStock == 0
                    ? "Produto sem estoque disponível"
                    : $"Estoque abaixo do mínimo ({p.CurrentStock} unidades)",
                Level = p.CurrentStock == 0 ? "high" : "medium"
            })
            .ToList();

    }
}


//TESTE PARA GRAFICO
public class LowStockViewModel
{
    public string Name { get; set; }
    public decimal CurrentStock { get; set; }
    public decimal MinStock { get; set; }
}

//TESTE PARA ALERTAS
public class StockAlertViewModel
{
    public string ProductName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Level { get; set; } = "low"; // "high", "medium", "low"
}
