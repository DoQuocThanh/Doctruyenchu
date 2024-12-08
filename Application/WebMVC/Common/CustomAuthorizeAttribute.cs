using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebMVC.Common
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasToken = context.HttpContext.Request.Cookies.ContainsKey("JwtToken");

            if (!hasToken)
            {
                // Lấy dịch vụ TempDataProvider
                var tempDataProvider = context.HttpContext.RequestServices.GetService<ITempDataProvider>();
                if (tempDataProvider != null)
                {
                    var tempData = tempDataProvider.LoadTempData(context.HttpContext);
                    tempData["Message"] = "Bạn cần đăng nhập để truy cập trang này.";
                    tempDataProvider.SaveTempData(context.HttpContext, tempData);
                }

                // Chuyển hướng tới trang login
                context.Result = new RedirectToActionResult("Login", "User", null);
            }
        }
    }

}
