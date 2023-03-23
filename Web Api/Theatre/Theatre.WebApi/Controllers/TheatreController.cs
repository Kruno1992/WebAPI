using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http;
using RouteAttribute = System.Web.Http.RouteAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using System.Net;
using System.Data.SqlClient;

namespace Theatre.WebApi.Controllers
{
    public class Personnel
    {
        public Guid Id { get; set; }
        public string PersonnelName { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public int HoursOfWork { get; set; }
        public string PositionAndHours { get; set; }
    }

    public class PersonnelController : ApiController
    {
        public static string connectionString = "Data Source=DESKTOP-6E381JI;Initial Catalog=ProdajaKarataKazalište;Integrated Security=True";

        // GET api/Theatre

        public HttpResponseMessage Get()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Personnel;", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<Personnel> worker = new List<Personnel>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Personnel personnel = new Personnel
                        {
                            PositionAndHours = $"{reader.GetString(3)}{reader.GetInt32(4)}",
                            Id = reader.GetGuid(0),
                            PersonnelName = reader.GetString(1),
                            Surname = reader.GetString(2),
                            Position = reader.GetString(3),
                            HoursOfWork = reader.GetInt32(4)
                        };

                        worker.Add(personnel);
                    }
                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, worker);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Details Have Not Been Found! Try again!");
                }
            }
        }

        //GET api/Theatre/Begovic
        public HttpResponseMessage Get(string Surname)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Personnel WHERE Surname=@surname;", connection);
                cmd.Parameters.AddWithValue("@surname", Surname);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                Personnel personnel = new Personnel();

                if (reader.HasRows)
                {
                    reader.Read();
                    personnel.PositionAndHours = $"{reader.GetString(3)}{reader.GetInt32(4)}";
                    personnel.Id = reader.GetGuid(0);
                    personnel.PersonnelName = reader.GetString(1);
                    personnel.Surname = reader.GetString(2);
                    personnel.Position = reader.GetString(3);
                    personnel.HoursOfWork = reader.GetInt32(4);

                    reader.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, personnel);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Try again! Personnel has not been found!");
                }
            }
        }

        // POST api/Theatre
        public HttpResponseMessage Post([FromBody] Personnel personnel)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            using (conn)
            {
                SqlCommand command = new SqlCommand("INSERT INTO Personnel (Id, PersonnelName, Surname, Position, HoursOfWork)" +
                               " VALUES (@Id, @personnelname, @surname, @position, @hoursofwork)", conn);
                personnel.Id = Guid.NewGuid();
                command.Parameters.AddWithValue("@id", personnel.Id);
                command.Parameters.AddWithValue("@personnelname", personnel.PersonnelName);
                command.Parameters.AddWithValue("@surname", personnel.Surname);
                command.Parameters.AddWithValue("@position", personnel.Position);
                command.Parameters.AddWithValue("@hoursofwork", personnel.HoursOfWork);


                conn.Open();

                int RowsAffected = command.ExecuteNonQuery();
                if (RowsAffected >= 0)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, personnel);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, personnel, "Try again!");
                }
            }
        }

        // PUT api/Theatre/5
        public HttpResponseMessage Put(Guid id, [FromBody] Personnel personnel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Personnel SET PersonnelName=@personnelname, Surname=@surname, Position=@position, HoursOfWork=@hoursofwork WHERE Id=@id";
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@personnelname", personnel.PersonnelName);
                    command.Parameters.AddWithValue("@surname", personnel.Surname);
                    command.Parameters.AddWithValue("@position", personnel.Position);
                    command.Parameters.AddWithValue("@hoursofwork", personnel.HoursOfWork);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Successfully updated");
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "zugjhgjhgjhg");
                    }
                }
                
            }
        }
        // DELETE api/Theatre/5
        public HttpResponseMessage Delete(Guid id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("DELETE FROM Personnel WHERE Id=@Id", connection);

                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, $"Personnel with Id {id} has been deleted.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"Personnel with Id {id} has not been found.");
                }
            }
        }

    }
}


