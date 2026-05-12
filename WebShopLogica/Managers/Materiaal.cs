using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopLogica.DataObjects;
using System.Configuration;
using System.Data.SqlClient;


namespace WebShopLogica.Managers
{
    public static class Materiaal
    {
        public static List<TypeSportObject> GetTypeSport(TypeSportObject typesportobject)
        {
            List<TypeSportObject> typesportList = new List<TypeSportObject>();

            string connectieString = ConfigurationManager.ConnectionStrings["WebShop"].ConnectionString;

            using (SqlConnection sqlcon = new SqlConnection(connectieString))
            {
                sqlcon.Open();
                string query = "SELECT Naam FROM TypeSport where Naam = @Naam";

                using (SqlCommand sqlcommand = new SqlCommand(query, sqlcon))
                {
                    sqlcommand.Parameters.AddWithValue("@Naam", typesportobject.Naam);
                    using (SqlDataReader reader = sqlcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TypeSportObject type = new TypeSportObject
                            {
                                Naam = reader["Naam"].ToString()
                            };
                            typesportList.Add(type);
                        }
                    }
                }
            }

            return typesportList;
        }


        public static List<TypeMateriaalObject> GetTypeSkiMateriaal()
        {
            List<TypeMateriaalObject> typemateriaalList = new List<TypeMateriaalObject>();
            string connectieString = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;
            using (SqlConnection sqlcon = new SqlConnection(connectieString))
            {
                sqlcon.Open();
                string query = "SELECT Naam FROM TypeMateriaal where TypeSportId = @TypeSportId";
                using (SqlCommand sqlcommand = new SqlCommand(query, sqlcon))
                {
                    sqlcommand.Parameters.AddWithValue("@TypeSportId", 1);
                    using (SqlDataReader reader = sqlcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TypeMateriaalObject type = new TypeMateriaalObject
                            {
                                Naam = reader["Naam"].ToString()
                            };
                            typemateriaalList.Add(type);
                        }
                    }
                }
            }
            return typemateriaalList;
        }

        public static List<MerkObject> GetJuisteTypeMerk(MateriaalObject materiaalobject)
        {
            List<MerkObject> merkList = new List<MerkObject>();
            string connectieString = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;
            using (SqlConnection sqlcon = new SqlConnection(connectieString))
            {
                sqlcon.Open();
                string query = 
                    "SELECT Merk.Naam FROM Merk " +
                    "inner join Materiaal m" +
                    "on m.MerkId = Merk.Id" +
                    "where m.TypeMateriaalId = Merk.Id";
                using (SqlCommand sqlcommand = new SqlCommand(query, sqlcon))
                {
                    sqlcommand.Parameters.AddWithValue("@TypeMateriaalId", materiaalobject.TypeMateriaalId);
                    using (SqlDataReader reader = sqlcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MerkObject merk = new MerkObject
                            {
                                Naam = reader["Naam"].ToString()
                            };
                            merkList.Add(merk);
                        }
                    }
                }
            }
            return merkList;
        }
    }
}
