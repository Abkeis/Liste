using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using MySql.Data.MySqlClient;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private int ID;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                MySql.Data.MySqlClient.MySqlConnectionStringBuilder builder = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder();
                builder.Server = "localhost";
                builder.UserID = "root";
                builder.Password = "";
                builder.Database = "test";

                using (MySqlConnection connection = new MySqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    String sql = "SELECT * FROM STUDENT";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        connection.Open();
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            Students students = new Students();
                            while (reader.Read())
                            {
                                var stud = new Student();
                                stud.Name = reader["Name"].ToString();
                                stud.Adresse = reader["Adresse"].ToString();
                                students.StudentsList.Add(stud);
                                
                            }
                            string del = "DELETE FROM STUDENT WHERE ID = _ID";

                            return View(students);
                        }
                    }
                    

                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            
            return View();
        }

        private void btn_delet_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch
            {
                
            }
        }

        public IActionResult Remove( int ID )
        {
            string del = "DELETE FROM STUDENT WHERE ID = _ID";


            return View();

        } 

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
