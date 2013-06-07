using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HY.Frame.Web
{
    public partial class BS : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SetAuthCookie("lxing guid", true);

            var a = new HY.Auth.AuthedUser("user").GetNodes();
        }
    }
}