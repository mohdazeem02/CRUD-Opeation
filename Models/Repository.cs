using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace example20CRUD.Models
{
    public class Repository
    {
        string str = ConfigurationManager.ConnectionStrings["get"].ToString();
        public void AddEmp(EmpModel obj)
        {
            SqlConnection cn=new SqlConnection(str);
            try
            {
                cn.Open();
                SqlCommand cm = new SqlCommand("sp_add2", cn);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@empid", obj.id);
                cm.Parameters.AddWithValue("@name", obj.Name);
                cm.Parameters.AddWithValue("@city", obj.City);
                cm.Parameters.AddWithValue("@address", obj.Address);
                cm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new Exception("Record already exist",ex);
            }
            finally
            {
                cn.Close();
            }
        }
        public List<EmpModel> GetAllEmployees()
        {
            SqlConnection cn = new SqlConnection(str);
            List<EmpModel> obj = new List<EmpModel>();
            SqlCommand cm = new SqlCommand("sp_view", cn);
            cm.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cm);
            DataTable dt = new DataTable();
            cn.Open();
            da.Fill(dt);
            cn.Close();
            foreach(DataRow dr in dt.Rows)
            {
                obj.Add(new EmpModel()
                {
                    id = Convert.ToInt32(dr["id"]),
                    Name = Convert.ToString(dr["name"]),
                    City = Convert.ToString(dr["city"]),
                    Address = Convert.ToString(dr["address"])
                });
            }
            return obj;
        }
        public void UpdateEmployee(EmpModel obj)
        {
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cm = new SqlCommand("sp_update", cn);
            cm.CommandType= CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@id", obj.id);
            cm.Parameters.AddWithValue("@name", obj.Name);
            cm.Parameters.AddWithValue("@city", obj.City);
            cm.Parameters.AddWithValue("@address", obj.Address);
            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();
        }
        public void DeleteEmployee(int id)
        {
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cm = new SqlCommand("sp_delete", cn);
            cm.CommandType= CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@id", id);
            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();
        }
    }
}