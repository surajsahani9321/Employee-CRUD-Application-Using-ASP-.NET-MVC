using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using WebApplication1.Models;
using dto = WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DALFile : Controller
    {
        private MySqlDatabase MySqlDatabase { get; set; }
        public DALFile(MySqlDatabase mySqlDatabase)
        {
            this.MySqlDatabase = mySqlDatabase;
        }
        [HttpPost]
        public void Complete(dto.UserModel input)
        {
            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"Update User SET Completed = EmployeeId, FullName, Email,AddressLine,City WHERE EmployeeID= @EmployeeId; ";
            cmd.Parameters.AddWithValue("@EmployeeId", input.EmployeeId);
            cmd.Parameters.AddWithValue("@FullName", input.FullName);
            cmd.Parameters.AddWithValue("@Email", input.Email);
            cmd.Parameters.AddWithValue("@AddressLine", input.AddressLine);
            cmd.Parameters.AddWithValue("@City", input.City);

            var recs = cmd.ExecuteNonQuery();
        }
        [HttpPost]
        public void Archive(WebApplication1.Models.UserModel input)
        {
            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"Update User SET Archive = EmployeeId, FullName, Email,AddressLine,City WHERE EmployeeID= @EmployeeId; ";
            cmd.Parameters.AddWithValue("@EmployeeId", input.EmployeeId);
            cmd.Parameters.AddWithValue("@FullName", input.FullName);
            cmd.Parameters.AddWithValue("@Email", input.Email);
            cmd.Parameters.AddWithValue("@AddressLine", input.AddressLine);
            cmd.Parameters.AddWithValue("@City", input.City);

            var recs = cmd.ExecuteNonQuery();
        }
        [HttpPost]
        public string Sms(dto.UserModel input)

        {
            var Response = "";
            var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand;

            cmd.CommandText = @"INSERT INTO User(Text,Created) VALUES (@EmployeeId, FullName, Email,AddressLine,City WHERE EmployeeID= @EmployeeId); ";
            cmd.Parameters.AddWithValue("@EmployeeId", input.EmployeeId);
            cmd.Parameters.AddWithValue("@FullName", input.FullName);
            cmd.Parameters.AddWithValue("@Email", input.Email);
            cmd.Parameters.AddWithValue("@AddressLine", input.AddressLine);
            cmd.Parameters.AddWithValue("@City", input.City);

            var recs = cmd.ExecuteNonQuery();

            if (recs == 1)
                Response = "OK";
            else
                Response = "Sorry ! I Didn't get that.";

            return Response;

        }


    }




    }


