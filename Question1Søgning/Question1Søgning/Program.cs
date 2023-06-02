using ConsoleTables;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Question1Soegning
{
    public class Personer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int cityID { get; set; }
    }

    public class Cities
    {
        public int ID { get; set; }
        public string CityName { get; set; }
    }

    public class DBConnection
    {
        public const string ConnectionString = "Data Source=70.34.198.110; Initial Catalog=2022RaCeMa; User ID=RaCeMa; Password=2022RaCeMa";
    }

    internal class Program
    {
        //user table with fistname
        private static string findfirstname = "SELECT UserID, FirstName, LastName, CityID FROM UsersQuestion1 WHERE FirstName = @FirstName";
        private static string findAll = "SELECT UserID, FirstName, LastName, CityID FROM UsersQuestion1";
            
        //city table
        const string CitySqlQuery = "SELECT CityID, CityName FROM CitiesQuestion1";

        public static string getcity(int cityid, List<Cities> citiesList)
        {
            foreach (var city in citiesList)
            {
                if (city.ID == cityid)
                {
                    return city.CityName;
                }
            }
            return "City not found";
        }
        
        static void Main(string[] args)
        {
            //read cities from database
            SqlConnection con = new SqlConnection(DBConnection.ConnectionString);
            
            List<Cities> cities = new List<Cities>();
            try
            {
                con.Open();
                
                SqlCommand citycmd = new SqlCommand(CitySqlQuery, con);
                
                SqlDataReader reader1 = citycmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        cities.Add(new Cities()
                        {
                            ID = reader1.GetInt32("CityID"),
                            CityName = reader1.GetString("CityName")
                        }); 
                    }
                }
                reader1.Close();
                
                citycmd.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
            Console.WriteLine("Søg efter et navn? (blank for alle)");
            string? input = Console.ReadLine();
            
            try
            {
                string userSqlQuery;
                SqlCommand usercmd;

                if (input.Trim().Length != 0 )
                {
                    userSqlQuery = findfirstname;
                    usercmd = new SqlCommand(userSqlQuery, con);
                    usercmd.Parameters.AddWithValue("@FirstName", input);
                }
                else
                {
                    userSqlQuery = findAll;
                    usercmd = new SqlCommand(userSqlQuery, con);
                }
                //create table for console output passe ind the column headers
                var table = new ConsoleTable("Firstname", "Lastname", "City");
                
                SqlDataReader reader2 = usercmd.ExecuteReader();
                if (reader2.HasRows)
                {
                    while (reader2.Read())
                    {
                        table.AddRow(
                            reader2.GetString("FirstName"), 
                            reader2.GetString("LastName"), 
                            getcity(reader2.GetInt32("CityID"), cities)
                        );
                    }
                }
                table.Write();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                con.Close();
            }
        }
    }

   
}