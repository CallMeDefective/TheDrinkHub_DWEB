using Microsoft.AspNetCore.Mvc;

public class CarrinhoController : Controller
{
    public IActionResult Index()
    {
        return View(); // mostra itens no carrinho
    }

    public IActionResult Checkout()
    {
        return View(); // formulário de pagamento
    }
}
