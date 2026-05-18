using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebShopLogica.DataObjects;
using WebShopLogica.Managers;
using System.Net;
using System.Net.Mail;

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
            else if (type == null)
            {
                Response.Redirect("Default.aspx");
            }


            if (!IsPostBack)
            {
                divFout.Visible = false;
                divJuist.Visible = false;
                divModalFout.Visible = false;
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

            foreach (Maat a in maatList)
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
                txtMaxAantal.Text = "";
                txtHuren.Text = "";
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

            if (txtEindDatum.Text != "")
            {
                DateTime eindtijd = DateTime.Parse(txtEindDatum.Text);
                if (begintijd > eindtijd)
                {
                    txtEindDatum.Text = begintijd.AddDays(1).ToString("yyyy-MM-dd");
                }
            }


            if (begintijd >= nu)
            {
                if (txtEindDatum.Text != "")
                {
                    NogBeschikbaar();
                }
                else
                {
                    divFout.Visible = false;
                    lblFoutboodschap.Text = "";
                }

            }
        }


        public void EindDatum()
        {

            if (txtBeginDatum.Text == "")
            {
                divFout.Visible = true;
                lblFoutboodschap.Text = "Je moet eerst een begindatum kiezen.";
                txtEindDatum.Text = "";
                return;
            }

            if (txtEindDatum.Text == "")
            {
                divFout.Visible = true;
                lblFoutboodschap.Text = "Je moet een einddatum kiezen.";
                txtHuren.Text = "";
                txtMaxAantal.Text = "";
                return;
            }

            DateTime begindatum = DateTime.Parse(txtBeginDatum.Text);
            DateTime eindtijd = DateTime.Parse(txtEindDatum.Text);
            if (eindtijd < begindatum)
            {
                divFout.Visible = true;
                lblFoutboodschap.Text = "Je moet een datum na de begindatum kiezen.";
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
            int winkelmandAantal = 0;

            DateTime begindatum = DateTime.Parse(txtBeginDatum.Text);
            DateTime einddatum = DateTime.Parse(txtEindDatum.Text);

            int materiaalId = Convert.ToInt32(ddlMateriaal.SelectedValue);
            int maatId = Convert.ToInt32(ddlMaten.SelectedValue);

            maxaantal = Materiaal.MaximumHoeveelheidMateriaal(materiaalId, maatId);
            gehuurdaantal = Materiaal.VerhuurdMateriaal(begindatum, einddatum, maatId, materiaalId);

            if (Session["Winkelmand"] != null)
            {
                List<WinkelmandItems> winkelmand = (List<WinkelmandItems>)Session["Winkelmand"];

                foreach (WinkelmandItems item in winkelmand)
                {


                    bool zelfdeMateriaal = item.MateriaalId == materiaalId;
                    bool zelfdeMaat = item.MaatId == maatId;
                    bool overlapt = item.Beginperiode <= einddatum && item.Eindperiode >= begindatum;

                    if (zelfdeMateriaal && zelfdeMaat && overlapt)
                    {
                        winkelmandAantal += item.Aantal;
                    }
                }
            }

            int resultaat = maxaantal - gehuurdaantal - winkelmandAantal;

            txtMaxAantal.Text = resultaat.ToString();
        }


        protected void btnToevoegenAanWinkelMand_Click(object sender, EventArgs e)
        {
            if (txtMaxAantal.Text == "")
            {
                divFout.Visible = true;
                lblFoutboodschap.Text = "Gelieve 2 datums in te vullen.";
                return;
            }

            if (txtHuren.Text == "")
            {
                divFout.Visible = true;
                lblFoutboodschap.Text = "Gelieve de hoeveelheid die je wilt huren in te vullen.";
                return;
            }

            if (int.TryParse(txtHuren.Text, out int nummer))
            {
                int nogbeschikbaar = Convert.ToInt32(txtMaxAantal.Text);

                if (nummer < 1)
                {
                    divFout.Visible = true;
                    lblFoutboodschap.Text = "Je kan niet minder dan 1 hoeveelheid huren. Het is nu op 1 gezet.";
                    txtHuren.Text = "1";
                }
                else if (nummer > nogbeschikbaar)
                {
                    divFout.Visible = true;
                    lblFoutboodschap.Text = "Je kan niet meer dan de beschikbare hoeveelheid huren. Het is nu op het maximale gezet.";
                    txtHuren.Text = nogbeschikbaar.ToString();
                }
                else //AI
                {
                    divFout.Visible = false;
                    lblFoutboodschap.Text = "";
                    WinkelmandItems item = new WinkelmandItems
                    {
                        Merk = ddlMerk.SelectedItem.Text,
                        Materiaal = ddlMateriaal.SelectedItem.Text,
                        Maat = Convert.ToInt32(ddlMaten.SelectedItem.Text),
                        Aantal = Convert.ToInt32(txtHuren.Text),
                        Beginperiode = Convert.ToDateTime(txtBeginDatum.Text),
                        Eindperiode = Convert.ToDateTime(txtEindDatum.Text),
                        MateriaalId = Convert.ToInt32(ddlMateriaal.SelectedValue),
                        MaatId = Convert.ToInt32(ddlMaten.SelectedValue)


                    };

                    List<WinkelmandItems> winkelmand;

                    if (Session["Winkelmand"] != null)
                    {
                        winkelmand = (List<WinkelmandItems>)Session["Winkelmand"];
                    }
                    else
                    {
                        winkelmand = new List<WinkelmandItems>();
                    }

                    winkelmand.Add(item);

                    Session["Winkelmand"] = winkelmand;
                    NogBeschikbaar();

                    divJuist.Visible = true;
                    lblJuistboodschap.Text = "Toegevoegd aan winkelmand!";
                }
            }
            else
            {
                divFout.Visible = true;
                lblFoutboodschap.Text = "Gelieve een geldig nummer in te vullen bij de hoeveelheid die je wilt huren.";
                txtHuren.Text = "";
                return;
            }
        }

        public void ToonWinkelMand() //AI
        {


            if (Session["Winkelmand"] == null)
            {
                return;
            }

            List<WinkelmandItems> winkelmand = (List<WinkelmandItems>)Session["Winkelmand"];


            foreach (WinkelmandItems item in winkelmand)
            {
                TableRow rijModal1 = new TableRow();
                TableRow rijModal2 = new TableRow();

                rijModal1.Cells.Add(new TableCell { Text = item.Merk + " - " + item.Materiaal + " (" + item.Maat + ")" });
                rijModal1.Cells[0].Style["padding-bottom"] = "0px";
                rijModal1.Cells[0].Style["padding-top"] = "0px";
                rijModal1.Cells[0].Style["border"] = "none";
                rijModal1.Cells[0].Style["padding-left"] = "18px";

                rijModal1.Cells.Add(new TableCell { Text = "Aantal: " + item.Aantal.ToString() });
                rijModal1.Cells[1].Style["padding-bottom"] = "0px";
                rijModal1.Cells[1].Style["padding-top"] = "0px";
                rijModal1.Cells[1].Style["padding-right"] = "100px";
                rijModal1.Cells[1].Style["border"] = "none";

                rijModal1.Cells.Add(new TableCell { Text = "Periode: " + item.Beginperiode.ToString("dd/MM/yyyy") + " tot " + item.Eindperiode.ToString("dd/MM/yyyy") });
                rijModal1.Cells[2].Style["padding-bottom"] = "0px";
                rijModal1.Cells[2].Style["padding-top"] = "0px";
                rijModal1.Cells[2].Style["border"] = "none";


                rijModal2.Cells.Add(new TableCell { Text = item.Merk + " - " + item.Materiaal + " (" + item.Maat + ")" });
                rijModal2.Cells[0].Style["padding-bottom"] = "0px";
                rijModal2.Cells[0].Style["padding-top"] = "0px";
                rijModal2.Cells[0].Style["border"] = "none";
                rijModal2.Cells[0].Style["padding-left"] = "18px";


                rijModal2.Cells.Add(new TableCell { Text = "Aantal: " + item.Aantal.ToString() });
                rijModal2.Cells[1].Style["padding-bottom"] = "0px";
                rijModal2.Cells[1].Style["padding-top"] = "0px";
                rijModal2.Cells[1].Style["padding-right"] = "100px";
                rijModal2.Cells[1].Style["border"] = "none";

                rijModal2.Cells.Add(new TableCell { Text = "Periode: " + item.Beginperiode.ToString("dd/MM/yyyy") + " tot " + item.Eindperiode.ToString("dd/MM/yyyy") });
                rijModal2.Cells[2].Style["padding-bottom"] = "0px";
                rijModal2.Cells[2].Style["padding-top"] = "0px";
                rijModal2.Cells[2].Style["border"] = "none";




                TableModal.Rows.Add(rijModal1);
                TableBevestigenModal.Rows.Add(rijModal2);
            }




        }

        //------------------------------------------------------------------------------------------------------------//



        protected void ddlMaten_SelectedIndexChanged(object sender, EventArgs e)
        {
            divJuist.Visible = false;
            lblJuistboodschap.Text = "";

            if (txtBeginDatum.Text != "" && txtEindDatum.Text != "")
            {
                NogBeschikbaar();
            }
        }


        protected void ddlTypeMateriaal_SelectedIndexChanged(object sender, EventArgs e)
        {
            divJuist.Visible = false;
            lblJuistboodschap.Text = "";

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
            divJuist.Visible = false;
            lblJuistboodschap.Text = "";

            VulMateriaal();
            VulMatenddl();

            if (txtBeginDatum.Text != "" && txtEindDatum.Text != "")
            {
                NogBeschikbaar();
            }
        }

        protected void ddlMateriaal_SelectedIndexChanged(object sender, EventArgs e)
        {
            divJuist.Visible = false;
            lblJuistboodschap.Text = "";

            VulMatenddl();

            if (txtBeginDatum.Text != "" && txtEindDatum.Text != "")
            {
                NogBeschikbaar();
            }
        }

        protected void txtBeginDatum_TextChanged(object sender, EventArgs e)
        {
            divJuist.Visible = false;
            lblJuistboodschap.Text = "";
            BeginDatum();
        }

        protected void txtEindDatum_TextChanged(object sender, EventArgs e)
        {
            divJuist.Visible = false;
            lblJuistboodschap.Text = "";
            EindDatum();
        }

        protected void btnToonWinkelMand_Click(object sender, EventArgs e) //AI
        {
            ToonWinkelMand();

            ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "showModal",
            "var modal = new bootstrap.Modal(document.getElementById('modalForm')); modal.show();",
            true
            );
        }

        protected void Button3_Click(object sender, EventArgs e) //modalform voor bevestigen
        {

            ToonWinkelMand();

            ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "showModal",
            "var modal = new bootstrap.Modal(document.getElementById('modalFormBevestigen')); modal.show();",
            true
            );
        }

        protected void btnModalSluitenBevestigen_Click(object sender, EventArgs e)
        {
            divModalFout.Visible = false;
            lblModal.Text = "";

            if (txtVoornaam.Text == "" || txtAchternaam.Text == "" || txtEmail.Text == "")
            {
                divModalFout.Visible = true;
                lblModal.Text = "Gelieve alle velden in te vullen.";
                OpenBevestigModal();
                return;
            }
            if (Session["Skiverhuur"] == null)
            {
                divModalFout.Visible = true;
                lblModal.Text = "Gelieve iets in de winkelmand te steken.";
                OpenBevestigModal();
                return;
            }
            divModalFout.Visible = false;
            lblModal.Text = "";
            string voornaam = txtVoornaam.Text;
            string achternaam = txtAchternaam.Text;
            string email = txtEmail.Text;

            List<WinkelmandItems> winkelmand = (List<WinkelmandItems>)Session["Winkelmand"];

            if (KlantManager.ControleerKlant(voornaam, achternaam, email))
            {
                //klant bestaat
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("zenunidrilon@gmail.com");
                mail.To.Add(txtEmail.Text);
                mail.Subject = "Bestelling skiverhuur bevestigd";
                mail.Body = "Uw bestelling van volgende materialen is bevestigd: ";

                int klantid = KlantManager.GetKlantMetGegevens(voornaam, achternaam, email);
                foreach (WinkelmandItems item in winkelmand)
                {
                    int uitleningid = Materiaal.VoegUitleningToe(item.Beginperiode, item.Eindperiode, klantid);
                    int materiaalmaatid = Materiaal.GetMateriaalMaatId(item.MateriaalId, item.MaatId);
                    Materiaal.VoegUitLeningMateriaalToe(uitleningid, materiaalmaatid, item.Aantal);
                    mail.Body += "\n"; 
                    mail.Body += item.Merk + " - " + item.Materiaal + " (" + item.Maat + ") ";
                    mail.Body += "\n";
                    mail.Body += "Aantal: " + item.Aantal;
                    mail.Body += "\n";
                    mail.Body += "Periode: " + item.Beginperiode.ToString("dd/MM/yyyy") + " tot " + item.Eindperiode.ToString("dd/MM/yyyy");
                }
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(
                "zenunidrilon@gmail.com",
                "efkb eqfq debc ddlw"
                );
                smtp.EnableSsl = true;
                smtp.Send(mail);
                divJuist.Visible = true;
                lblJuistboodschap.Text = "De huur is bevestigd.";
                Session.Remove("Winkelmand");
            }
            else
            {
                //nieuwe klant aanmaken
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("zenunidrilon@gmail.com");
                mail.To.Add(txtEmail.Text);
                mail.Subject = "Bestelling skiverhuur bevestigd";
                mail.Body = "Uw bestelling van volgende materialen is bevestigd: ";

                KlantManager.MaakKlant(voornaam, achternaam, email);

                int klantid = KlantManager.GetKlantMetGegevens(voornaam, achternaam, email);
                foreach (WinkelmandItems item in winkelmand)
                {
                    int uitleningid = Materiaal.VoegUitleningToe(item.Beginperiode, item.Eindperiode, klantid);
                    int materiaalmaatid = Materiaal.GetMateriaalMaatId(item.MateriaalId, item.MaatId);
                    Materiaal.VoegUitLeningMateriaalToe(uitleningid, materiaalmaatid, item.Aantal);
                    mail.Body += "\n";
                    mail.Body += item.Merk + " - " + item.Materiaal + " (" + item.Maat + ") ";
                    mail.Body += "\n";
                    mail.Body += "Aantal: " + item.Aantal;
                    mail.Body += "\n";
                    mail.Body += "Periode: " + item.Beginperiode.ToString("dd/MM/yyyy") + " tot " + item.Eindperiode.ToString("dd/MM/yyyy");
                }
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(
                "zenunidrilon@gmail.com",
                "efkb eqfq debc ddlw"
                );
                smtp.EnableSsl = true;
                smtp.Send(mail);
                divJuist.Visible = true;
                lblJuistboodschap.Text = "De huur is bevestigd.";
                Session.Remove("Winkelmand");
            }
        }
        public void OpenBevestigModal()
        {
            ToonWinkelMand();

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "showModalBevestigen",
                "var modal = new bootstrap.Modal(document.getElementById('modalFormBevestigen')); modal.show();",
                true
            );
        }
    }
}