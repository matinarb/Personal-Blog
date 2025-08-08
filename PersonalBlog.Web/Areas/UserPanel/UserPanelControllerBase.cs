using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Areas.UserPanel;

[Area("UserPanel")]
[Authorize(Policy ="UserPolicy")]
public class UserPanelControllerBase : Controller
{

}