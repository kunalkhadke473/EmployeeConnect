using EmployeeConnect.Models;
using Microsoft.Data.SqlClient;
using System.IO;
namespace EmployeeConnect.DAL
{
    public class EmployeeDBContext
    {
        string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=EmpDb;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=False";
        public List<Employee> SelectEmpRecord()
        {
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("select * from Emp", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Employee> emps = new List<Employee>();
            while (reader.Read())
            {
                Employee emp = new Employee();
                emp.Id = (int)reader["Id"];
                emp.Name = reader["Name"].ToString();
                emp.Address = reader["Address"].ToString();
                emps.Add(emp);
            }
            con.Close();
            return emps;

        }
        public int InsertEmpRecord(Employee emp)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            //Directly using string interpolation
            string queryText = string.Format("insert into Emp values ('{0}', '{1}')", emp.Name, emp.Address);
            SqlCommand cmd = new SqlCommand(queryText, con);
            int no0fRowsAfftected = cmd.ExecuteNonQuery();
            con.Close();
            return no0fRowsAfftected;
        }


        public int UpdateEmpRecord(Employee emp)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
           // Directly using string interpolation
            string queryText = string.Format("update Emp set Name ='{0}', Address ='{1}' where Id = '{2}'", emp.Name, emp.Address,emp.Id);
            SqlCommand cmd = new SqlCommand(queryText, con);
            int no0fRowsAfftected = cmd.ExecuteNonQuery();
            con.Close();
            return no0fRowsAfftected;
        }      
        public int DeleteEmpRecord(int no)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            //Directly using string interpolation
            string queryText = string.Format("delete from Emp where Id = {0}", no);
            SqlCommand cmd = new SqlCommand(queryText, con);
            int no0fRowsAfftected = cmd.ExecuteNonQuery();
            con.Close();
            return no0fRowsAfftected;

        }
    }
}
