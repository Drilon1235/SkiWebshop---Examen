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
            if (type == "1")
            {
                lblTitel.Text = "Alpieneski's huren";
            }
            else if (type == "2")
            {
                lblTitel.Text = "Langlaufski's huren";
            }


            if (!IsPostBack)
            {
                divFout.Visible = false;
                divJuist.Visible = false;
                VulTypeMateriaal();
                VulMerk();
            }

        }



        //public void VulTypeSkiMateriaalddl()
        //{
        //    ddlTypeMateriaal.Items.Clear();
        //    List<TypeMateriaalObject> typemateriaalList = Materiaal.GetTypeSkiMateriaal();

        //    foreach (TypeMateriaalObject type in typemateriaalList)
        //    {
        //        ddlTypeMateriaal.Items.Add(new ListItem(type.Naam, type.Id.ToString()));
        //    }
        //} 

        //public void VulSkiMerkddl()
        //{
        //    ddlMerk.Items.Clear();

        //    if (ddlTypeMateriaal.SelectedValue == "")
        //    {
        //        return;
        //    }

        //    int typeMateriaalId = Convert.ToInt32(ddlTypeMateriaal.SelectedValue);

        //    List<MerkObject> merkList = Materiaal.GetJuisteTypeMerk(typeMateriaalId);

        //    foreach (MerkObject merk in merkList)
        //    {
        //        ddlMerk.Items.Add(merk.Naam);
        //    }

        //}

        protected void ddlTypeMateriaal_SelectedIndexChanged(object sender, EventArgs e)
        {
            VulMerk();
        }


        public void VulTypeMateriaal()
        {
            ddlTypeMateriaal.Items.Clear();

            int typeSportId = Materiaal.GetSportTypeById(Convert.ToInt32(Request.QueryString["type"]));
            List<TypeMateriaalObject> typemateriaalList = Materiaal.GetTypeMateriaal(typeSportId);
            foreach (TypeMateriaalObject type in typemateriaalList)
            {
                ddlTypeMateriaal.Items.Add(type.Naam.ToString());
            }
        }

        public void VulMerk()
        {
            ddlMerk.Items.Clear();

            int typeMateriaalId = ddlTypeMateriaal.SelectedIndex + 1;
            List<MerkObject> merkList = Materiaal.GetMerk(typeMateriaalId);
            foreach (MerkObject merk in merkList)
            {
                ddlMerk.Items.Add(merk.Naam);
            }
        }
    }
}