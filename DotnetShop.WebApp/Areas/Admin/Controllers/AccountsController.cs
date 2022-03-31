using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotnetShop.WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class AccountsController : Controller
    {

    }
}
