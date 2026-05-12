using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebShopLogica.DataObjects;
using WebShopLogica.Managers;

namespace ExamenWebontwikkelingSkiWebshop
{
    public partial class Huren : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"];
            if (type == "ski")
            {
                lblTitel.Text = "Alpieneski's huren";
                VulTypeSkiMateriaalddl();
            }
            else if (type == "langlauf")
            {
                lblTitel.Text = "Langlaufski's huren";
            }


            if (!IsPostBack)
            {
                divFout.Visible = false;
                divJuist.Visible = false;
            }

            VulSkiMerkddl();
        }



        public void VulTypeSkiMateriaalddl()
        {
            List<TypeMateriaalObject> typemateriaalList = Materiaal.GetTypeSkiMateriaal();

            foreach (TypeMateriaalObject type in typemateriaalList)
            {
                ddlTypeMateriaal.Items.Add(type.Naam);
            }
        }

        public void VulSkiMerkddl()
        {
            MateriaalObject matobject = new MateriaalObject
            {
                TypeMateriaalId = Convert.ToInt32(ddlTypeMateriaal.SelectedValue)
            };
            List<MerkObject> merkList = Materiaal.GetJuisteTypeMerk(matobject);
            foreach (MerkObject merk in merkList)
            {
                ddlTypeMateriaal.Items.Add(merk.Naam);
            }

        }
    }
}