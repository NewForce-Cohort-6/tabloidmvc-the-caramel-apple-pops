using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;
using TabloidMVC.Utils;



namespace TabloidMVC.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration config) : base(config) { }
 public List<Category> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name FROM Category ORDER BY name";
                    var reader = cmd.ExecuteReader();

                    var categories = new List<Category>();

                    while (reader.Read())
                    {
                        categories.Add(new Category()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                        });
                    }

                    reader.Close();

                    return categories;
                }
            }
        }


        public void AddCategory(Category category)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Category (Name)
                        OUTPUT INSERTED.ID
                        VALUES (@Name)";
                    cmd.Parameters.AddWithValue("@Name", category.Name);
                    
                    category.Id = (int)cmd.ExecuteScalar();
                    
                }
            }
        }

        public void DeleteCategory(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Category
                            WHERE Id = @id
                        "
                    ;

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public Category GetCategoryById(int id)
        {
            using (SqlConnection conn = Connection)
            {
               conn.Open();
              using (SqlCommand cmd = conn.CreateCommand())
                {
                 cmd.CommandText = @"
                    SELECT Id, Name
                        FROM Category
                        WHERE Id = @id
                        ";

               cmd.Parameters.AddWithValue("@id", id);
                 Category category = null;
                   using (SqlDataReader reader = cmd.ExecuteReader())
                 {
                    while (reader.Read())
                     {
                          if (category == null)
                           {
                                category = new Category
                              {
                                   Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Name = reader.GetString(reader.GetOrdinal("Name"))
                                };
                            }
                        }
                    }
                    return category;
              }
            }
        }

        public void UpdateCategory(Category category)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Category
                            SET 
                                Name = @name                                 
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@name", category.Name);                   
                    cmd.Parameters.AddWithValue("@id", category.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
