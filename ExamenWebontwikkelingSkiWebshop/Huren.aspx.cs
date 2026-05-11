using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExamenWebontwikkelingSkiWebshop
{
    public partial class Huren : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];
            if (type == "ski")
            {
                lblTitel.Text = "Alpineski's huren";

            }
            else if (type == "langlauf")
            {
                lblTitel.Text = "Langlaufmateriaal huren";
            }
            ddlType.Items.Add("Alpineski's");
            ddlType.Items.Add("Langlaufmateriaal");
        }
    }
}