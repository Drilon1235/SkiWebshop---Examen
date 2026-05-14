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
            string query = "Select Distinct me.Naam, me.Id from Merk me " +
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
                                Naam = reader["Naam"].ToString(),
                                Id = Convert.ToInt32(reader["Id"])
                            };
                            merkList.Add(merkobject);
                        }
                        return merkList;
                    }
                }
            }
        }

        public static List<MateriaalObject> GetGeselecteerdeTypeMateriaal(MateriaalObject materiaalobject)
        {
            string constring = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;
            List<MateriaalObject> materiaalList = new List<MateriaalObject>();
            string query = "Select Model, Id from Materiaal where TypeMateriaalId = @TypeMateriaalId and MerkId = @MerkId";
            using (SqlConnection sqlcon = new SqlConnection(constring))
            {
                sqlcon.Open();
                using (SqlCommand sqlcommand = new SqlCommand(query, sqlcon))
                {
                    sqlcommand.Parameters.AddWithValue("@TypeMateriaalId", materiaalobject.TypeMateriaalId);
                    sqlcommand.Parameters.AddWithValue("@MerkId", materiaalobject.MerkId);
                    using (SqlDataReader reader = sqlcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MateriaalObject materiaalobject1 = new MateriaalObject
                            {
                                Naam = reader["Model"].ToString(),
                                Id = Convert.ToInt32(reader["Id"])
                            };
                            materiaalList.Add(materiaalobject1);
                        }
                        return materiaalList;
                    }
                }
            }
        }


        public static List<Maat> GetMaat(int materiaalId)
        {

            string query =
            "Select Maat.Naam, Maat.Id from Maat inner join Materiaalmaat m on Maat.Id = m.MaatId where m.MateriaalId = @MateriaalId" +
            " order by Maat.Naam asc";

            string constring = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;

            List<Maat> maatList = new List<Maat>();

            using (SqlConnection sqlcon = new SqlConnection(constring))
            {
                sqlcon.Open();
                using (SqlCommand sqlcommand = new SqlCommand(query, sqlcon))
                {
                    sqlcommand.Parameters.AddWithValue("@MateriaalId", materiaalId);
                    using (SqlDataReader reader = sqlcommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Maat maatobject = new Maat
                            {
                                Naam = reader["Naam"].ToString(),
                                Id = Convert.ToInt32(reader["Id"])
                            };
                            maatList.Add(maatobject);
                        }
                        return maatList;
                    }
                }
            }
        }


        public static int MaximumHoeveelheidMateriaal(int materiaalId, int maatId)
        {
            string query = "Select Aantal from MateriaalMaat" +
                " where MateriaalId = @MateriaalId" +
                " and MaatId = @MaatId";

            int hoeveelheid = 0;
            string constring = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;

            using(SqlConnection sqlco = new SqlConnection(constring))
            {
                sqlco.Open();
                using (SqlCommand sqlcmd = new SqlCommand(query, sqlco)) 
                {
                    sqlcmd.Parameters.AddWithValue("@MateriaalId", materiaalId);
                    sqlcmd.Parameters.AddWithValue("@MaatId", maatId);

                    using (SqlDataReader read = sqlcmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            hoeveelheid += Convert.ToInt32(read["Aantal"]);
                        }
                    }
                }
            }
            return hoeveelheid;
        }


        public static int VerhuurdMateriaal(DateTime datumuitlening, DateTime datuminlevering, int maatId, int materiaalId)
        {
            string query = "Select um.Aantal from UitleningMateriaal um" +
                " inner join Uitlening u on u.Id = um.UitleningId" +
                " inner join MateriaalMaat ma on ma.Id = um.MateriaalMaatId" +
                " where u.DatumUitlening <= @DatumInlevering " +
                "And u.DatumInlevering >= @DatumUitlening " +
                "AND ma.MaatId = @MaatId" +
                "AND ma.MateriaalId = @MateriaalId";

            string constring = ConfigurationManager.ConnectionStrings["Skiverhuur"].ConnectionString;

            int hoeveelheid = 0;

            using(SqlConnection sqlco = new SqlConnection(constring))
            {
                sqlco.Open();
                using(SqlCommand sqlcmd = new SqlCommand(query, sqlco))
                {
                    sqlcmd.Parameters.AddWithValue("@DatumUitlening", datumuitlening);
                    sqlcmd.Parameters.AddWithValue("@DatumInlevering", datuminlevering);
                    sqlcmd.Parameters.AddWithValue("@MaatId", maatId);
                    sqlcmd.Parameters.AddWithValue("@MateriaalId", materiaalId);

                    using (SqlDataReader reader = sqlcmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hoeveelheid += Convert.ToInt32(reader["Aantal"]);
                        }
                    }
                }
            }
            return hoeveelheid;
        }
    }
}