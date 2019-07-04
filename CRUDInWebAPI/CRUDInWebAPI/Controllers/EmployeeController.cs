using CRUDInWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;

namespace CRUDInWebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [ActionName("GetAllEmployee")]
        public IEnumerable<Employee> Get()
        {
            string str = ConfigurationManager.ConnectionStrings["CrudConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SSpGetAll";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = con;
            List<Employee> empList = new List<Employee>();

            Employee emp = null;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                emp = new Employee();
                emp.FirstName = dr["FirstName"].ToString();
                emp.LastName = dr["LastName"].ToString();
                emp.EmailId = dr["EmailId"].ToString();
                emp.UserName = dr["UserName"].ToString();
                emp.Password = dr["Password"].ToString();

                empList.Add(emp);
            }

            con.Close();
            dr.Close();

            return empList;

        }

        [HttpGet]
        [ActionName("GetParticularEmployee")]

        //Note:In this method the variable id cannot be changed to any other name like EmpId i.e if id is changed to EmpId if done it does not call this particular Get() method
        public Employee Get(int EmpId)
        {
            string str = ConfigurationManager.ConnectionStrings["CrudConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SSpGetAll";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection = con;

            
            //cmd.Parameters.AddWithValue("@EmpId", id);

            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adap.Fill(dt);

            Employee emp = new Employee();

            emp.FirstName = dt.Rows[EmpId-1]["FirstName"].ToString();
            emp.LastName = dt.Rows[EmpId - 1]["LastName"].ToString();
            emp.EmailId = dt.Rows[EmpId - 1]["EmailId"].ToString();
            emp.UserName = dt.Rows[EmpId -1]["UserName"].ToString();
            emp.Password = dt.Rows[EmpId -1]["Password"].ToString();

            con.Close();

            return emp;
        }

        [HttpDelete]
        [ActionName("DeleteEmployee")]
        public string Delete(int EmpId)
        {
            string str = ConfigurationManager.ConnectionStrings["CrudConnection"].ConnectionString;
            SqlConnection con = new SqlConnection();

            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SpDeleteParticularEmployee";
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmpId", EmpId);
            int result = cmd.ExecuteNonQuery();

            con.Close();

            return result + " Rows affected";
}

        [HttpPost]
        [ActionName("AddEmployee")]
        public string Add(Employee emp)
        {
            string str = ConfigurationManager.ConnectionStrings["CrudConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(str);

            con.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SpInsertEmployee";
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmd.Parameters.AddWithValue("@LastName", emp.LastName);
            cmd.Parameters.AddWithValue("@EmailId", emp.EmailId);
            cmd.Parameters.AddWithValue("@UserName", emp.UserName);
            cmd.Parameters.AddWithValue("@Password", emp.Password);

            int result = cmd.ExecuteNonQuery(); 

            con.Close();

            return result + " Rows affected";
        }

    }
}
