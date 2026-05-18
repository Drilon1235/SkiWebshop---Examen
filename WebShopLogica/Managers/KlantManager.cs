using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopLogica.DataObjects;

namespace WebShopLogica.Managers
{
    public static class KlantManager
    {
        public static bool ControleerKlant(string voornaam, string achternaam, string email)
        {
            string query = "Select * from Klant where Voornaam = @Voornaam And Achternaam = @Achternaam And Email = @Email";
            string constring = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;
            bool Bestaat = false;

            using(SqlConnection sqlcon = new SqlConnection(constring))
            {
                sqlcon.Open();
                using(SqlCommand sqlcmd = new SqlCommand(query, sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@Voornaam", voornaam);
                    sqlcmd.Parameters.AddWithValue("@Achternaam", achternaam);
                    sqlcmd.Parameters.AddWithValue("@Email", email);

                    using(SqlDataReader sqlreader = sqlcmd.ExecuteReader())
                    {
                        while (sqlreader.Read())
                        {
                            Bestaat = true;
                        }
                    }
                }
            }
            return Bestaat;
        }

        public static void MaakKlant(string voornaam, string achternaam, string email)
        {
            string query = "Insert Into Klant (Voornaam, Achternaam, Email) Values (Voornaam = @Voornaam And Achternaam = @Achternaam And Email = @Email)";
            string constring = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;

            using (SqlConnection sqlcon = new SqlConnection(constring))
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand(query, sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@Voornaam", voornaam);
                    sqlcmd.Parameters.AddWithValue("@Achternaam", achternaam);
                    sqlcmd.Parameters.AddWithValue("@Email", email);

                    sqlcmd.ExecuteNonQuery();
                }
            }
        }

        public static int GetKlantMetGegevens(string voornaam, string achternaam, string email)
        {
            string query = "Select Id from Klant where Voornaam = @Voornaam And Achternaam = @Achternaam And Email = @Email";
            string constring = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;
            int id = 0;

            using (SqlConnection sqlcon = new SqlConnection(constring))
            {
                sqlcon.Open();
                using (SqlCommand sqlcmd = new SqlCommand(query, sqlcon))
                {
                    sqlcmd.Parameters.AddWithValue("@Voornaam", voornaam);
                    sqlcmd.Parameters.AddWithValue("@Achternaam", achternaam);
                    sqlcmd.Parameters.AddWithValue("@Email", email);

                    using (SqlDataReader sqlreader = sqlcmd.ExecuteReader())
                    {
                        while (sqlreader.Read())
                        {
                            id += Convert.ToInt32(sqlreader["Id"]);
                        }
                    }
                }
            }
            return id;
        }

    }
}
