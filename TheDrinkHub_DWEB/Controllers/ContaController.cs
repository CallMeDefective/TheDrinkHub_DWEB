public class ContaController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult EditarConta()
    {
        return View();
    }

    public IActionResult Logout()
    {
        // l�gica de logout aqui mais tarde
        return RedirectToAction("Index", "Home");
    }
}
