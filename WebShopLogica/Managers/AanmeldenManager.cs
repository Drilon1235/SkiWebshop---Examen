using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopLogica.Objects;
using System.Configuration;


namespace WebShopLogica.Managers
{
    public static class AanmeldenManager
    {

        public static List<AanmeldenObject> ControleerGebruiker(AanmeldenObject gebruiker)
        {
            string constring = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;

            List<AanmeldenObject> gebruikersLijst = new List<AanmeldenObject>();
            using (SqlConnection connection = new SqlConnection(constring))
            {
                connection.Open();
                string query = "SELECT Gebruikersnaam, Wachtwoord FROM Gebruiker WHERE Gebruikersnaam = @gebruikersnaam AND Wachtwoord = @wachtwoord";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@gebruikersnaam", gebruiker.Gebruikersnaam);
                    command.Parameters.AddWithValue("@wachtwoord", gebruiker.Wachtwoord);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            AanmeldenObject gevondenGebruiker = new AanmeldenObject
                            {
                                Gebruikersnaam = reader["Gebruikersnaam"].ToString(),
                                Wachtwoord = reader["Wachtwoord"].ToString()
                            };
                            gebruikersLijst.Add(gevondenGebruiker);
                        }
                    }
                }
            }
            return gebruikersLijst;
        }
    }
}
