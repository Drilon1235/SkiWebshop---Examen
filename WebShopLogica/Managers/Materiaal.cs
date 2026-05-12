using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopLogica.DataObjects;
using System.Configuration;
using System.Data.SqlClient;
using System.ComponentModel;


namespace WebShopLogica.Managers
{
    public static class Materiaal
    {
        //public static List<TypeSportObject> GetTypeSport(TypeSportObject typesportobject)
        //{
        //    List<TypeSportObject> typesportList = new List<TypeSportObject>();

        //    string connectieString = ConfigurationManager.ConnectionStrings["WebShop"].ConnectionString;

        //    using (SqlConnection sqlcon = new SqlConnection(connectieString))
        //    {
        //        sqlcon.Open();
        //        string query = "SELECT Naam FROM TypeSport where Naam = @Naam";

        //        using (SqlCommand sqlcommand = new SqlCommand(query, sqlcon))
        //        {
        //            sqlcommand.Parameters.AddWithValue("@Naam", typesportobject.Naam);
        //            using (SqlDataReader reader = sqlcommand.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                { 
        //                    TypeSportObject type = new TypeSportObject
        //                    {
        //                        Naam = reader["Naam"].ToString()
        //                    };
        //                    typesportList.Add(type);
        //                }
        //            }
        //        }
        //    }

        //    return typesportList;
        //}


        //public static List<TypeMateriaalObject> GetTypeSkiMateriaal()
        //{
        //    List<TypeMateriaalObject> typemateriaalList = new List<TypeMateriaalObject>();
        //    string connectieString = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;

        //    using (SqlConnection sqlcon = new SqlConnection(connectieString))
        //    {
        //        sqlcon.Open();

        //        string query = "SELECT Id, Naam FROM TypeMateriaal WHERE TypeSportId = @TypeSportId";

        //        using (SqlCommand sqlcommand = new SqlCommand(query, sqlcon))
        //        {
        //            sqlcommand.Parameters.AddWithValue("@TypeSportId", 1);

        //            using (SqlDataReader reader = sqlcommand.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    TypeMateriaalObject type = new TypeMateriaalObject
        //                    {
        //                        Id = Convert.ToInt32(reader["Id"]),
        //                        Naam = reader["Naam"].ToString()
        //                    };

        //                    typemateriaalList.Add(type);
        //                }
        //            }
        //        }
        //    }

        //    return typemateriaalList;
        //}


        //public static List<MerkObject> GetJuisteTypeMerk(int typemateriaalID)
        //{
        //    List<MerkObject> merkList = new List<MerkObject>();
        //    string connectieString = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;

        //    using (SqlConnection sqlcon = new SqlConnection(connectieString))
        //    {
        //        sqlcon.Open();

        //        string query =
        //            "SELECT DISTINCT Merk.Naam FROM Merk " +
        //            "INNER JOIN Materiaal m ON m.MerkId = Merk.Id " +
        //            "WHERE m.TypeMateriaalId = @TypeMateriaalId";

        //        using (SqlCommand sqlcommand = new SqlCommand(query, sqlcon))
        //        {
        //            sqlcommand.Parameters.AddWithValue("@TypeMateriaalId", typemateriaalID);

        //            using (SqlDataReader reader = sqlcommand.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    MerkObject merk = new MerkObject
        //                    {
        //                        Naam = reader["Naam"].ToString()
        //                    };

        //                    merkList.Add(merk);
        //                }
        //            }
        //        }
        //    }

        //    return merkList;
        //}


        //public static List<MateriaalObject> GetGeselecteerdeTypeMateriaal(MateriaalObject materiaalobject)
        //{
        //    List<MateriaalObject> materiaalList = new List<MateriaalObject>();
        //    string connectieString = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;
        //    using (SqlConnection sqlcon = new SqlConnection(connectieString))
        //    {
        //        sqlcon.Open();
        //        string query =
        //            "SELECT Naam FROM Materiaal where TypeMateriaalId = @TypeMateriaalId and MerkId = @MerkId";
        //        using (SqlCommand sqlcommand = new SqlCommand(query, sqlcon))
        //        {
        //            sqlcommand.Parameters.AddWithValue("@TypeMateriaalId", materiaalobject.TypeMateriaalId);
        //            sqlcommand.Parameters.AddWithValue("@MerkId", materiaalobject.MerkId);
        //            using (SqlDataReader reader = sqlcommand.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    MateriaalObject mat = new MateriaalObject
        //                    {
        //                        Naam = reader["Naam"].ToString()
        //                    };
        //                    materiaalList.Add(mat);
        //                }
        //            }
        //        }
        //    }
        //    return materiaalList;
        //}




        public static int GetSportTypeById(int querystring)
        {
            string constring = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;

            int result = 0;

            string query = "Select Id from TypeSport where Id = @Id";

            using (SqlConnection sqlcon = new SqlConnection(constring))
            {
                sqlcon.Open();
                using (SqlCommand sqlcommand = new SqlCommand(query, sqlcon))
                {
                    sqlcommand.Parameters.AddWithValue("@Id", querystring);
                    using (SqlDataReader reader = sqlcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = Convert.ToInt32(reader["Id"]); 
                        }
                    }
                    return result;
                }
            }
        }

        public static List<TypeMateriaalObject> GetTypeMateriaal(int TypeSportId)
        {
            string constring = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;
            List<TypeMateriaalObject> typemateriaalList = new List<TypeMateriaalObject>();

            string query = "Select Naam, Id from TypeMateriaal where TypeSportId = @TypeSportId";

            using (SqlConnection sqlcon = new SqlConnection(constring))
            {
                sqlcon.Open();
                using (SqlCommand sqlcommand = new SqlCommand(query, sqlcon))
                {
                    sqlcommand.Parameters.AddWithValue("@TypeSportId", TypeSportId);
                    using (SqlDataReader reader = sqlcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TypeMateriaalObject typemateriaalobject = new TypeMateriaalObject
                            {
                                Naam = reader["Naam"].ToString(),
                                Id = Convert.ToInt32(reader["Id"])
                            };
                            typemateriaalList.Add(typemateriaalobject);
                        }
                        return typemateriaalList;
                    }
                }
            }
        }

       

        public static List<MerkObject> GetMerk(int TypeMateriaalId)
        {
            string constring = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;
            List<MerkObject> merkList = new List<MerkObject>();
            string query = "Select Distinct Naam from Merk me " +
                           "INNER JOIN Materiaal m ON m.MerkId = me.Id " +
                           "WHERE m.TypeMateriaalId = @TypeMateriaalId";
            using (SqlConnection sqlcon = new SqlConnection(constring))
            {
                sqlcon.Open();
                using (SqlCommand sqlcommand = new SqlCommand(query, sqlcon))
                {
                    sqlcommand.Parameters.AddWithValue("@TypeMateriaalId", TypeMateriaalId);
                    using (SqlDataReader reader = sqlcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MerkObject merkobject = new MerkObject
                            {
                                Naam = reader["Naam"].ToString()
                            };
                            merkList.Add(merkobject);
                        }
                        return merkList;
                    }
                }
            }
        }
    }
}