using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace task3.Infrastructure.Authentication
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            return Roles.Any(r => role.Contains(r));
        }

        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }

        public int UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
