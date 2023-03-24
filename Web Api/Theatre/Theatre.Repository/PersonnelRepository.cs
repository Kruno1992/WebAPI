using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Theatre.Model;
using Theatre.Repository.Common;

namespace Theatre.Repository
{
    public class PersonnelRepository : IPersonnelRepository
    {
        public static string connectionString = "Data Source=DESKTOP-6E381JI;Initial Catalog=ProdajaKarataKazalište;Integrated Security=True";

        public List<Personnel> GetAllPersonnel()
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
                            Id = reader.GetGuid(0),
                            PersonnelName = reader.GetString(1),
                            Surname = reader.GetString(2),
                            Position = reader.GetString(3),
                            HoursOfWork = reader.GetInt32(4)
                        };
                        worker.Add(personnel);
                    }
                    reader.Close();
                    return worker;
                }
                else
                {
                    return null;
                }
            }
        }
        public List<Personnel> GetPersonnel(string surname)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Personnel WHERE Surname=@surname;", connection);
                cmd.Parameters.AddWithValue("@surname", surname);
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                List<Personnel> workers = new List<Personnel>();

                if (reader.HasRows)
                {
                    reader.Read();
                    Personnel personnel = new Personnel();

                    personnel.Id = reader.GetGuid(0);
                    personnel.PersonnelName = reader.GetString(1);
                    personnel.Surname = reader.GetString(2);
                    personnel.Position = reader.GetString(3);
                    personnel.HoursOfWork = reader.GetInt32(4);

                    workers.Add(personnel);

                    reader.Close();
                    return workers;
                }
                else
                {
                    return null;
                }
            }
        }
        public bool AddPersonnel(Personnel personnel)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            using (connection)
            {
                SqlCommand command = new SqlCommand("INSERT INTO Personnel (Id, PersonnelName, Surname, Position, HoursOfWork)" +
                               " VALUES (@Id, @personnelname, @surname, @position, @hoursofwork)", connection);
                personnel.Id = Guid.NewGuid();
                command.Parameters.AddWithValue("@id", personnel.Id);
                command.Parameters.AddWithValue("@personnelname", personnel.PersonnelName);
                command.Parameters.AddWithValue("@surname", personnel.Surname);
                command.Parameters.AddWithValue("@position", personnel.Position);
                command.Parameters.AddWithValue("@hoursofwork", personnel.HoursOfWork);


                connection.Open();

                int RowsAffected = command.ExecuteNonQuery();
                if (RowsAffected >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool EditPersonnel(Guid id, Personnel personnel)
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
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public bool DeletePersonnel(Guid id)
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
