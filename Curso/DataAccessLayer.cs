using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso
{
    public class DataAccessLayer
    {
        public SqlConnection conn = new SqlConnection("Password=Cajeta2022;Persist Security Info=True;User ID=sa;Initial Catalog=db_contactos;Data Source=Daniel");
        
        public void InsertContact(Contact contacto)
        {
            try
            {
                conn.Open();
                string query = @"INSERT INTO contactos(FirstName, LastName, Phone, Address)
                                Values(@FirstName,@LastName, @Phone, @Address)";
                
                SqlParameter nombres = new SqlParameter("@FirstName",contacto.FirstName);
                SqlParameter apellidos = new SqlParameter("@LastName", contacto.LastName);
                SqlParameter celular = new SqlParameter("@Phone", contacto.Phone);
                SqlParameter direccion = new SqlParameter("@Address", contacto.Address);

                SqlCommand command = new SqlCommand(query,conn);
                command.Parameters.Add(nombres);
                command.Parameters.Add(apellidos);
                command.Parameters.Add(celular);
                command.Parameters.Add(direccion);

                command.ExecuteNonQuery();


            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Contact> GetContactos(string buscar = null)
        {
            List<Contact> contactos = new List<Contact>();
            try
            {
                conn.Open();
                string query = @"SELECT Id, FirstName, LastName, Phone, Address From contactos";

                SqlCommand command = new SqlCommand();

                if (!string.IsNullOrEmpty(buscar))
                {
                    query += @" WHERE FirstName LIKE @buscar OR LastName LIKE @buscar OR Phone LIKE @buscar OR 
                                Address LIKE @buscar";
                    command.Parameters.Add(new SqlParameter("@buscar", $"%{buscar}%"));
                }
                command.CommandText = query;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    contactos.Add(new Contact
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString()
                    });
                }


            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return contactos;
        }
        
        public void UpdateContacto(Contact contacto)
        {
            try
            {
                conn.Open();
                string query = @"UPDATE contactos 
                                 SET FirstName = @FirstName,
                                 LastName = @LastName,
                                 Phone = @Phone,
                                 Address = @Address
                                 WHERE Id = @Id";

                SqlParameter id = new SqlParameter("@Id", contacto.Id);
                SqlParameter nombres = new SqlParameter("@FirstName", contacto.FirstName);
                SqlParameter apellidos = new SqlParameter("@LastName", contacto.LastName);
                SqlParameter celular = new SqlParameter("@Phone", contacto.Phone);
                SqlParameter direccion = new SqlParameter("@Address", contacto.Address);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(nombres);
                command.Parameters.Add(apellidos);
                command.Parameters.Add(celular);
                command.Parameters.Add(direccion);
                command.Parameters.Add(id);

                command.ExecuteNonQuery();

            }
            catch(Exception)
            {

            }
            finally
            {
                conn.Close();
            }
        }
        public void EliminarContacto(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM contactos WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }

        }
    }

}
