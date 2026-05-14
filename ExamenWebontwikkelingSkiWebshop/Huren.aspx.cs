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
            else if(type == null)
            {
                Response.Redirect("Default.aspx");
            }



            if (!IsPostBack)
            {
                divFout.Visible = false;
                divJuist.Visible = false;
                VulTypeMateriaal();
                VulMerk();
                VulMateriaal();
                VulMatenddl();
            }

        }


        public void VulTypeMateriaal()
        {
            ddlTypeMateriaal.Items.Clear();

            int typeSportId = Materiaal.GetSportTypeById(Convert.ToInt32(Request.QueryString["type"]));
            List<TypeMateriaalObject> typemateriaalList = Materiaal.GetTypeMateriaal(typeSportId);
            foreach (TypeMateriaalObject type in typemateriaalList)
            {
                ddlTypeMateriaal.Items.Add(new ListItem(type.Naam, type.Id.ToString()));
            }
        }

        public void VulMerk()
        {
            ddlMerk.Items.Clear();

            int typeMateriaalId = Convert.ToInt32(ddlTypeMateriaal.SelectedValue);
            List<MerkObject> merkList = Materiaal.GetMerk(typeMateriaalId);
            foreach (MerkObject merk in merkList)
            {
                ddlMerk.Items.Add(new ListItem(merk.Naam, merk.Id.ToString()));
            }
        }

        public void VulMateriaal()
        {
            ddlMateriaal.Items.Clear();
            int typeMateriaalId = Convert.ToInt32(ddlTypeMateriaal.SelectedValue);
            int merkId = Convert.ToInt32(ddlMerk.SelectedValue);
            MateriaalObject materiaal = new MateriaalObject();
            materiaal.TypeMateriaalId = typeMateriaalId;
            materiaal.MerkId = merkId;
            List<MateriaalObject> materiaalList = Materiaal.GetGeselecteerdeTypeMateriaal(materiaal);
            foreach (MateriaalObject mat in materiaalList)
            {
                ddlMateriaal.Items.Add(new ListItem(mat.Naam, mat.Id.ToString()));
            }
        }



        public void VulMatenddl()
        {
            ddlMaten.Items.Clear();
            int materiaalId = Convert.ToInt32(ddlMateriaal.SelectedValue);

            List<Maat> maatList = Materiaal.GetMaat(materiaalId);

            foreach(Maat a in maatList)
            {
                ddlMaten.Items.Add(new ListItem(a.Naam, a.Id.ToString()));
            }
        }

 



        public void BeginDatum()
        {
                if (txtBeginDatum.Text == "")
                {
                    divFout.Visible = true;
                    lblFoutboodschap.Text = "Je moet een begindatum kiezen.";
                    return;
                }

                DateTime nu = DateTime.Now.Date;
                DateTime begintijd = DateTime.Parse(txtBeginDatum.Text);

                if (begintijd < nu)
                {
                    divFout.Visible = true;
                    lblFoutboodschap.Text = "Je moet een datum in de toekomst kiezen.";
                    txtBeginDatum.Text = "";
                    return;
                }

                if(txtEindDatum.Text != "")
                {
                    DateTime eindtijd = DateTime.Parse(txtEindDatum.Text);
                    if (begintijd > eindtijd)
                    {
                        txtEindDatum.Text = begintijd.AddDays(1).ToString("yyyy-MM-dd");
                    }
                }


                if (begintijd >= nu)
                {
                    if(txtEindDatum.Text != "")
                    {
                        NogBeschikbaar();
                    }
                    divFout.Visible = false;
                    lblFoutboodschap.Text = "";
                    return;
                }
            }
        

        public void EindDatum()
        {
            if(txtBeginDatum.Text == "")
            {
                divFout.Visible = true;
                lblFoutboodschap.Text = "Je moet eerst een begindatum kiezen.";
                txtEindDatum.Text = "";
                return;
            }
            if(txtEindDatum.Text == "")
            {
                divFout.Visible = true;
                lblFoutboodschap.Text = "Je moet een einddatum kiezen.";
                return;
            }

            DateTime begindatum = DateTime.Parse(txtBeginDatum.Text);
            DateTime eindtijd = DateTime.Parse(txtEindDatum.Text);
            if (eindtijd < begindatum)
            {
                divFout.Visible = true;
                lblFoutboodschap.Text = "Je moet een datum na op op de begindatum kiezen kiezen.";
                txtEindDatum.Text = "";
                return;
            }


            if (eindtijd >= begindatum)
            {
                divFout.Visible = false;
                lblFoutboodschap.Text = "";
                NogBeschikbaar();
            }
        }

        public void NogBeschikbaar()
        {
            int maxaantal = 0;
            int gehuurdaantal = 0;
            DateTime begindatum = DateTime.Parse(txtBeginDatum.Text);
            DateTime einddatum = DateTime.Parse(txtEindDatum.Text);
            int materiaalId = Convert.ToInt32(ddlMateriaal.SelectedValue);
            int maatId = Convert.ToInt32(ddlMaten.SelectedValue);
            maxaantal = Materiaal.MaximumHoeveelheidMateriaal(materiaalId, maatId);

            gehuurdaantal = Materiaal.VerhuurdMateriaal(begindatum, einddatum, maatId, materiaalId);

            int resultaat = maxaantal - gehuurdaantal;



            txtMaxAantal.Text = resultaat.ToString();
        }


        protected void btnToevoegenAanWinkelMand_Click(object sender, EventArgs e)
        {
            if()
        }

        //------------------------------------------------------------------------------------------------------------//



        protected void ddlMaten_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtBeginDatum.Text != "" && txtEindDatum.Text != "")
            {
                NogBeschikbaar();
            }
        }


        protected void ddlTypeMateriaal_SelectedIndexChanged(object sender, EventArgs e)
        {
            VulMerk();
            VulMateriaal();
            VulMatenddl();

            if (txtBeginDatum.Text != "" && txtEindDatum.Text != "")
            {
                NogBeschikbaar();
            }
        }

        protected void ddlMerk_SelectedIndexChanged(object sender, EventArgs e)
        {
            VulMateriaal();
            VulMatenddl();

            if (txtBeginDatum.Text != "" && txtEindDatum.Text != "")
            {
                NogBeschikbaar();
            }
        }

        protected void ddlMateriaal_SelectedIndexChanged(object sender, EventArgs e)
        {
            VulMatenddl();

            if (txtBeginDatum.Text != "" && txtEindDatum.Text != "")
            {
                NogBeschikbaar();
            }
        }

        protected void txtBeginDatum_TextChanged(object sender, EventArgs e)
        {
            BeginDatum();
        }

        protected void txtEindDatum_TextChanged(object sender, EventArgs e)
        {
            EindDatum();
        }
    }
}