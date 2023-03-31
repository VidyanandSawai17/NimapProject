using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NimapProject.Models
{
    public class ProductDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        private readonly IConfiguration configuration;
        public ProductDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string constr = configuration["ConnectionStrings:defaultConnection"];
            con = new SqlConnection(constr);
        }

        public List<Product> ProductList()
        {
            List<Product> list = new List<Product>();
            string str = "select * from Product";
            cmd = new SqlCommand(str, con);
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Product p = new Product();
                    p.ProductId = Convert.ToInt32(reader["ProductId"]);
                    p.ProductName = reader["ProductName"].ToString();
                    p.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                    list.Add(p);

                }
            }
            con.Close();
            return list;
        }

        public int AddProd(Product p)
        {
            string str = "insert into Product values(@ProductId, @ProductName, @CategoryId)";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@ProductId", p.ProductId);
            cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", p.CategoryId);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int UpdateProd(Product p)
        {
            string str = "update Product set ProductName=@ProductName, CategoryId=@CategoryId where ProductId=@id";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@id", p.ProductId);
            cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", p.CategoryId);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int DeleteProd(int id)
        {
            string str = "delete from Product where ProductId = @ProductId ";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@ProductId", id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public Product GetProdById(int id)
        {
            Product p = new Product();
            string query = "select * from Product where ProductId=@id";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    p.ProductId = Convert.ToInt32(reader["ProductId"]);
                    p.ProductName = reader["ProductName"].ToString();
                    p.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                }
            }
            con.Close();
            return p;

        }
    }
}









