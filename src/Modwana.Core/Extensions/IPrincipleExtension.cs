using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Modwana.Core.Extensions
{
    public static class IPrincipleExtension
    {
        public static string GetUserId(this IPrincipal principal)
        {
            if (principal == null)
                return null;

            if (principal is ClaimsPrincipal identity)
            {
                var found = identity.FindFirst(a => a.Type == ClaimTypes.NameIdentifier);

                return found?.Value;
            }

            return null;
        }

        public static string GetEmail(this IPrincipal principal)
        {
            if (principal == null)
                return null;

            if (principal is ClaimsPrincipal identity)
            {
                var found = identity.FindFirst(a => a.Type == ClaimTypes.Email);

                return found?.Value;
            }

            return null;
        }
    }
}
