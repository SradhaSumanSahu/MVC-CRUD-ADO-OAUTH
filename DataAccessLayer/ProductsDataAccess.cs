using MVC_CRUD_ADO_OAUTH.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace MVC_CRUD_ADO_OAUTH.DataAccessLayer
{
    public class ProductsDataAccess
    {
        string conString = ConfigurationManager.ConnectionStrings["MyConnection"].ToString();

        //Get All Products
        public List<Products> GetAllProducts()
        {
            List<Products> productList = new List<Products>();


            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spGetAllProducts";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtproducts = new DataTable();


                connection.Open();
                sqlDA.Fill(dtproducts);
                connection.Close();

                foreach (DataRow dr in dtproducts.Rows)
                {
                    productList.Add(new Products
                    {

                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remark = dr["Remark"].ToString()
                    });
                }

            }

            return productList;
        }


        //Insert Products
        public bool InsertProduct(Products products)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("spInsertProducts", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", products.ProductName);
                command.Parameters.AddWithValue("@Price", products.Price);
                command.Parameters.AddWithValue("@Qty", products.Qty);
                command.Parameters.AddWithValue("@Remark", products.Remark);


                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();

            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        //Get products by productsID

        public List<Products> GetProductByID(int ProductID)
        {
            List<Products> productList = new List<Products>();


            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "spGetproductByID ";
                command.Parameters.AddWithValue("@ProductID", ProductID);
                SqlDataAdapter SqlDA = new SqlDataAdapter(command);
                DataTable dtproducts = new DataTable();


                connection.Open();
                SqlDA.Fill(dtproducts);
                connection.Close();

                foreach (DataRow dr in dtproducts.Rows)
                {
                    productList.Add(new Products
                    {

                        ProductID = Convert.ToInt32(dr["ProductID"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Qty = Convert.ToInt32(dr["Qty"]),
                        Remark = dr["Remark"].ToString()
                    });
                }

            }

            return productList;
        }

        //update products
        public bool UpdateProduct(Products products)
        {
            int i = 0;
            using(SqlConnection connection=new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("spUpdateProduct",connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", products.ProductID);
                command.Parameters.AddWithValue("@ProductName", products.ProductName);
                command.Parameters.AddWithValue("@Price", products.Price);
                command.Parameters.AddWithValue("@Qty",products.Qty);
                command.Parameters.AddWithValue("@Remark", products.Remark);

                connection.Open();
                i=command.ExecuteNonQuery();
                connection.Close();
            }
            if(i>0)
            {
                return true;

            }
            else
            {
                return false;
            }
            
        }





        //Delete Product
        
        public string DeleteProduct(int productid)
        {
            string result = "";
            using (SqlConnection connection =new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("spDeleteProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductID", productid);
                command.Parameters.Add("@OutputMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@Outputmessage"].Value.ToString();
                connection.Close();
            }
            return result;
        }

    }
}
