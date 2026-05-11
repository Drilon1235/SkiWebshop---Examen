using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using WebShopLogica.Managers;
using WebShopLogica.Objects;

namespace ExamenWebontwikkelingSkiWebshop
{
    public partial class Aanmelden : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                divFout.Visible = false;
            }



        }


        public void ControleerGebruiker()
        {
            AanmeldenObject gebruiker = new AanmeldenObject
            {
                Gebruikersnaam = txtGebruikersnaam.Text,
                Wachtwoord = txtWachtwoord.Text
            };

            List<AanmeldenObject> gebruikers = AanmeldenManager.ControleerGebruiker(gebruiker);

            if (gebruikers.Count > 0)
            {
                Response.Redirect("DefaultAangemeld.aspx");
            }
            else
            {
                lblFoutboodschap.Text = "Deze combinatie gebruikersnaam - wachtwoord is niet gekend.\n";
                divFout.Visible = true;
            }

        }

        protected void btnAanmelden_Click(object sender, EventArgs e)
        {
            ControleerGebruiker();
        }
    }
}