// See https://aka.ms/new-console-template for more information
using ConsoleTables;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Question3Salgsstatistik
{
    public class DBConnection
    {
        public const string ConnectionString = "Data Source=70.34.198.110; Initial Catalog=2022RaCeMa; User ID=RaCeMa; Password=2022RaCeMa";
    }
    
    internal class Program
    {
        private static void GetAllBooks(SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand("select BookName, BookAmount FROM BooksQuestion3", connection);

            var table = new ConsoleTable(
                new ConsoleTableOptions {
                    Columns = new[] {"Bookname", "Quantity"},
                    EnableCount = false
                }
            );
    
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows) {
                while (reader.Read()) {
                    table.AddRow(reader.GetString("BookName"), reader.GetInt32("BookAmount"));
                }
            }
            table.Write();
            reader.Dispose();
            cmd.Dispose();
        }

        public static void FindBook(SqlConnection connection) {
            Console.WriteLine("Søg efter bog: ");
            string? input = Console.ReadLine()?.Trim();
            
            SqlCommand findcmd;

            if (input.Length == 0){
                GetAllBooks(connection);
                return;
            }
                
            findcmd = new SqlCommand("SELECT BookName, BookAmount FROM BooksQuestion3 WHERE BookName LIKE @BookName", connection);
            findcmd.Parameters.AddWithValue("@BookName", "%" + input + "%" );
            
            SqlDataReader reader = findcmd.ExecuteReader();
            var table = new ConsoleTable("Bookname", "Quantity");

            if (!reader.HasRows){
                Console.WriteLine("Ingen bog fundet");
                return;
            }

            while (reader.Read()) {
                table.AddRow(reader.GetString("BookName"), reader.GetInt32("BookAmount"));
            }
            table.Write();
        }
        
        
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(DBConnection.ConnectionString);
            try {
                connection.Open();
                FindBook(connection);
            }
            finally {
                connection.Close();
            }
        }
    }
    
}
