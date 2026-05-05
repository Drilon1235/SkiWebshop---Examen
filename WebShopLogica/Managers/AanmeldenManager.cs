using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopLogica.Managers
{
    public static class AanmeldenManager
    {
        public static string Connectionstring { get; set; }

        public static bool Aanmelden(string gebruikersnaam, string wachtwoord)
        {
            using (SqlConnection connection = new SqlConnection(Connectionstring))
            {
                connection.Open();
                string query = "SELECT Gebruikersnaam, Wachtwoord FROM Gebruikers WHERE Gebruikersnaam = @gebruikersnaam AND Wachtwoord = @wachtwoord";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@gebruikersnaam", gebruikersnaam);
                    command.Parameters.AddWithValue("@wachtwoord", wachtwoord);
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
