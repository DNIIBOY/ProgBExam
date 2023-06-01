using System;
using System.Data;
using System.Data.SqlClient;

namespace Question1Soegning
{
    public class Personer {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int cityID { get; set; }
    }

    public class Cities {
        public int ID { get; set; }
        public string CityName { get; set; }
    }
    
    
    public class DBConnection {
        public const string ConnectionString = "Data Source=70.34.198.110; Initial Catalog=2022RaCeMa; User ID=RaCeMa; Password=2022RaCeMa";
    }
   //Data Source=70.34.198.110; Initial Catalog=2023PWManager; User ID=PWManager; Password=2023PWManager" 
    
    
    internal class Program {
        //user table with fistname
        public static string findfirstname = "SELECT UserID, FirstName, LastName, CityID FROM UsersQuestion1 WHERE FirstName LIKE @FirstName";
        public static string findAll = "SELECT UserID, FirstName, LastName, CityID FROM UsersQuestion1";
            
        //city table
        const string CitySqlQuery = "SELECT CityID, CityName FROM CitiesQuestion1";
        
        static void Main(string[] args) {
            //read cities from database
            SqlConnection citycon = new SqlConnection(DBConnection.ConnectionString);
            
            List<Cities> cities = new List<Cities>();
            try {
                citycon.Open();
                
                SqlCommand citycmd = new SqlCommand(CitySqlQuery, citycon);
                SqlDataReader reader1 = citycmd.ExecuteReader();
                if (reader1.HasRows) {
                    while (reader1.Read()) {
                        cities.Add(new Cities() {
                            ID = reader1.GetInt32("CityID"),
                            CityName = reader1.GetString("CityName")
                        }); 
                    }
                }
                reader1.Close();
                citycmd.Dispose();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
                throw;
            }

            //initiate connection with connections string from class DBConnection
            SqlConnection Usercmd = new SqlConnection(DBConnection.ConnectionString);
            Console.WriteLine("Søg efter et navn? (blank for alle)");
            string? input = Console.ReadLine();
            
            try {
                Usercmd.Open();
                string userSqlQuery = findfirstname;
                if (input == "" || input == " ") {
                    userSqlQuery = findAll;
                }
                
                SqlCommand usercmd = new SqlCommand(userSqlQuery, Usercmd);
                if(input != null){
                    usercmd.Parameters.AddWithValue("@FirstName", input);                   
                }
                
                SqlDataReader reader2 = usercmd.ExecuteReader();
                if (reader2.HasRows) {
                    Console.WriteLine("FirstName \tLastName \t City");
                    
                    while (reader2.Read()) {
                        Console.Write(reader2.GetString("FirstName")+ "\t");
                        Console.Write(reader2.GetString("LastName")+" \t");
                        
                        //run through the list of cities and find city name
                        foreach (var city in cities) {
                            if (city.ID == reader2.GetInt32("CityID")) {
                                Console.Write(city.CityName + "\n");
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally {
                Usercmd.Close();
            }
        }
    }

   
}
