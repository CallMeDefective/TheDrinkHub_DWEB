using Microsoft.AspNetCore.Mvc;

public class ProdutosController : Controller
{
    public IActionResult Index()
    {
        return View(); // catálogo geral
    }

    public IActionResult Categoria(string nome)
    {
        // Ex: /Produtos/Categoria?nome=Vinho
        ViewBag.Categoria = nome;
        return View();
    }

    public IActionResult Detalhes(int id)
    {
        // Ver detalhes do produto por ID
        return View();
    }
}
