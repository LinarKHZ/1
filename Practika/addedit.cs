using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Practika
{
    class addedit
    {
        public static async Task Add(Product prod)
        {

            string connectionString = "data source=DESKTOP-C1FJ2O7;initial catalog=prak;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                string sql = string.Format("Insert Into Product" + "(Name, Comment, DataAdd, TypeUnitId, UserId, Photo) " +
                                           "Values(@Name, @Comment, @DataAdd , @TypeUnitId ,@UserId,@Photo)");
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", prod.Name);
                    cmd.Parameters.AddWithValue("@Comment", prod.Comment);
                    cmd.Parameters.AddWithValue("@DataAdd", prod.DataAdd);
                    cmd.Parameters.AddWithValue("@TypeUnitId", prod.TypeUnitId);
                    cmd.Parameters.AddWithValue("@UserId", prod.UserId);
                    cmd.Parameters.AddWithValue("@Photo", prod.Photo);
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
