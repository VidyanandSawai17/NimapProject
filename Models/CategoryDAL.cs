using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NimapProject.Models
{
    public class CategoryDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        private readonly IConfiguration configuration;
        public CategoryDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string constr = configuration.GetConnectionString("defaultConnection");
            con = new SqlConnection(constr);
        }

        public List<Category> CategoryList()
        {
            List<Category> list = new List<Category>();
            string str = "select * from Category";
            cmd = new SqlCommand(str, con);
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Category c = new Category();
                    c.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    c.CategoryName = reader["CategoryName"].ToString();
                    list.Add(c);
                }
            }
            con.Close();
            return list;
        }

        public int AddCat(Category cat)
        {
            string str = "insert into Category values(@CategoryId, @CategoryName)";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@CategoryId", cat.CategoryId);
            cmd.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int UpdateCat(Category cat)
        {
            string str = "update Category set CategoryName = @CategoryName where CategoryId = @CategoryId";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@CategoryId", cat.CategoryId);
            cmd.Parameters.AddWithValue("@CategoryName", cat.CategoryName);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }


        public int DeleteCat(int id)
        {
            string str = "delete from Category where CategoryId = @CategoryId ";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@CategoryId", id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public Category GetCatById(int id)
        {
            Category c = new Category();
            string query = "select * from Category where CategoryId=@id";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            
            
            
            
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    c.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    c.CategoryName = reader["CategoryName"].ToString();
                }
            }
            con.Close();
            return c;

        }
    }
}












