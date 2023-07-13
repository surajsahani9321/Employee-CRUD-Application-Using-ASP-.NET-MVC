using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private string connectionString = "Server=localhost;Database=asp_dotnet_mvc;User Id=root;Password=root";

        public IActionResult Index()
        {
            List<UserModel> model = GetResult();
            return View("~/Views/Home/List.cshtml", model);
        }

        public IActionResult Create()
        {
            return View("~/Views/Home/Create.cshtml");
        }

        [HttpPost]
        public IActionResult Create(UserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        MySqlCommand cmd = connection.CreateCommand();

                        cmd.CommandText = @"INSERT INTO employee(EmployeeId, FullName, Email, AddressLine, City) VALUES (@EmployeeId, @FullName, @Email, @AddressLine, @City)";
                        cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
                        cmd.Parameters.AddWithValue("@FullName", model.FullName);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@AddressLine", model.AddressLine);
                        cmd.Parameters.AddWithValue("@City", model.City);

                        int recs = cmd.ExecuteNonQuery();
                        if (recs > 0)
                        {
                            TempData["Message"] = "Employee created successfully.";
                        }
                        else
                        {
                            TempData["Error"] = "Failed to create employee.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while creating the employee.";
                }
            }
            else
            {
                TempData["Error"] = "Invalid data provided. Please check the input and try again.";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var data = GetEmployeeById(id);
            if (data == null)
            {
                return NotFound();
            }
            return View("~/Views/Home/Edit.cshtml", data);
        }

        [HttpPost]
        public IActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        MySqlCommand cmd = connection.CreateCommand();

                        cmd.CommandText = @"UPDATE employee SET FullName = @FullName, Email = @Email, AddressLine = @AddressLine, City = @City WHERE EmployeeId = @EmployeeId";
                        cmd.Parameters.AddWithValue("@FullName", model.FullName);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@AddressLine", model.AddressLine);
                        cmd.Parameters.AddWithValue("@City", model.City);
                        cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);

                        int recs = cmd.ExecuteNonQuery();
                        if (recs > 0)
                        {
                            TempData["Message"] = "Employee updated successfully.";
                        }
                        else
                        {
                            TempData["Error"] = "Failed to update employee.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the employee.";
                }
            }
            else
            {
                TempData["Error"] = "Invalid data provided. Please check the input and try again.";
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var data = GetEmployeeById(id);
            if (data == null)
            {
                return NotFound();
            }
            return View("~/Views/Home/Delete.cshtml", data);
        }

        [HttpPost]
        public IActionResult Delete(UserModel model)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = @"DELETE FROM employee WHERE EmployeeId = @EmployeeId";
                    cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);

                    int recs = cmd.ExecuteNonQuery();
                    if (recs > 0)
                    {
                        TempData["Message"] = "Employee deleted successfully.";
                    }
                    else
                    {
                        TempData["Error"] = "Failed to delete employee.";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the employee.";
            }

            return RedirectToAction("Index");
        }

        public List<UserModel> GetResult()
        {
            List<UserModel> ret = new List<UserModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = @"SELECT EmployeeId, FullName, Email, AddressLine, City FROM employee";

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        UserModel employee = new UserModel()
                        {
                            EmployeeId = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            Email = reader.GetString(2),
                            AddressLine = reader.GetString(3),
                            City = reader.GetString(4)
                        };
                        ret.Add(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while retrieving employee data.";
            }

            return ret;
        }

        public UserModel GetEmployeeById(int id)
        {
            UserModel employee = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();

                    cmd.CommandText = @"SELECT EmployeeId, FullName, Email, AddressLine, City FROM employee WHERE EmployeeId = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        employee = new UserModel()
                        {
                            EmployeeId = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            Email = reader.GetString(2),
                            AddressLine = reader.GetString(3),
                            City = reader.GetString(4)
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while retrieving employee data.";
            }

            return employee;
        }
    }
}
