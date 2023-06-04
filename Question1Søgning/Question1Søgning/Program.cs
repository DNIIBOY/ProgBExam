using ConsoleTables;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Question1Soegning
{
    public class City {
        public int ID { get; set; }
        public string CityName { get; set; }
    }

    public class DBConnection {
        public const string ConnectionString = "Data Source=70.34.198.110; Initial Catalog=2022RaCeMa; User ID=RaCeMa; Password=2022RaCeMa";
    }

    internal class Program {
        //user table with fistname
        private static string findfirstname = "SELECT UserID, FirstName, LastName, CityID FROM UsersQuestion1 WHERE FirstName LIKE @FirstName";
        private static string findAll = "SELECT UserID, FirstName, LastName, CityID FROM UsersQuestion1";
            
        //city table
        const string CitySqlQuery = "SELECT CityID, CityName FROM CitiesQuestion1";

        public static string getcity(int cityid, List<City> cities) {
            foreach (var city in cities) {
                if (city.ID == cityid) {
                    return city.CityName;
                }
            }
            return "City not found";
        }
        
        static void Main(string[] args) {
            //read cities from database
            SqlConnection connnection = new SqlConnection(DBConnection.ConnectionString);
            
            List<City> cities = new List<City>();
            try {
                connnection.Open();
                SqlCommand cityCommand = new SqlCommand(CitySqlQuery, connnection);
                
                SqlDataReader cityReader = cityCommand.ExecuteReader();
                if (cityReader.HasRows) {
                    while (cityReader.Read()) {
                        cities.Add(new City() {
                            ID = cityReader.GetInt32("CityID"),
                            CityName = cityReader.GetString("CityName")
                        }); 
                    }
                }
                cityReader.Close();
                cityCommand.Dispose();
            }
            catch (Exception e) {
                connnection.Close();
                throw;
            }
            
            Console.WriteLine("Søg efter et navn? (blank for alle)");
            string? input = Console.ReadLine().Trim();
            
            try {
                string userSqlQuery;
                SqlCommand userCommand;

                if (input.Length == 0) {
                    userSqlQuery = findAll;
                    userCommand = new SqlCommand(userSqlQuery, connnection);
                }
                else {
                    userSqlQuery = findfirstname;
                    userCommand = new SqlCommand(userSqlQuery, connnection);
                    userCommand.Parameters.AddWithValue("@FirstName", input + "%");
                }

                var table = new ConsoleTable(
                    new ConsoleTableOptions {
                        Columns = new[] {"Firstname", "Lastname", "City"},
                        EnableCount = false
                    }
                );
                
                SqlDataReader userReader = userCommand.ExecuteReader();
                if (userReader.HasRows) {
                    while (userReader.Read()) {
                        table.AddRow(
                            userReader.GetString("FirstName"), 
                            userReader.GetString("LastName"), 
                            getcity(userReader.GetInt32("CityID"), cities)
                        );
                    }
                }
                table.Write();
            }
            finally {
                connnection.Close();
            }
        }
    }

   
}

