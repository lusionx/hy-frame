using HY.Frame.Core.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HY.Auth
{
    public class AuthedUser : ConfigBase
    {
        protected override string ConfigFilePath
        {
            get
            {
                return System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Auth.xml");
            }
        }

        public IList<string> Roles { get; set; }

        public AuthedUser(IEnumerable<string> roles)
        {
            Roles = roles.Select(a => a.Trim()).ToList();
        }

        public AuthedUser(string role)
        {
            Roles = role.Split(';', ',', ' ').Where(a => !string.IsNullOrEmpty(a)).ToList();
        }

        public List<LinkNode> GetNodes()
        {
            return null;
        }
    }
}
