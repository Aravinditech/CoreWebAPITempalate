using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using Microsoft.Data.SqlClient;
using CoreTempalate.DBContext;

namespace CoreTempalate.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly string _role;

        public AuthorizationFilter(string role)
        {
            _role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Get the user ID from the header
            var userId = context.HttpContext.Request.Headers["UserId"].FirstOrDefault();
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Content = "Missing user ID in request header."
                };
                return;
            }

            // Get the user's role from the database
            var dbContext = context.HttpContext.RequestServices.GetService<ApplicationDbContext>();
            var userRole = dbContext.Users.Where(u => u.Id == new Guid(userId)).Select(u => u.Role.Name).FirstOrDefault();
            if (userRole == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Forbidden,
                    Content = "User is not authorized to access this resource."
                };
                return;
            }

            // Check if the user has the required role
            if (!string.Equals(userRole, _role, StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.Forbidden,
                    Content = "User is not authorized to access this resource."
                };
                return;
            }
        }
    }
}
