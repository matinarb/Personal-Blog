using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Areas.Admin;

[Area("Admin")]
[Authorize(Policy ="AdminPolicy")]
public class AdminControllerBase : Controller
{

}