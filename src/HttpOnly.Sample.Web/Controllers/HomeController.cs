using Microsoft.AspNetCore.Mvc;

namespace HttpOnly.Sample.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var setCookie = false;

        // If we don't have a cookie, set it to en-US
        if (!HttpContext.Request.Cookies.TryGetValue("lang", out var lang))
        {
            lang = "en-US";
            setCookie = true;
        }

        if (setCookie)
        {
            // Set the HttpOnly cookie if it was missing so that it can be sent back to the server on subsequent requests
            HttpContext.Response.Cookies.Append("lang", lang, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict
            });
        }

        // And set the language in the viewbag so that we can display it
        ViewBag.Lang = lang;

        return View();
    }
}
