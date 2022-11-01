using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class TagRepository: BaseRepository, ITagRepository
    {
        public TagRepository(IConfiguration config): base(config) { }

        //get a list of all tags existing in the database table.
        public List<Tag> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name FROM Tag ORDER BY Name";
                    var reader=cmd.ExecuteReader();

                    List<Tag> tags = new List<Tag>();

                    while (reader.Read())
                    {
                        tags.Add(new Tag()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        });
                    }
                    reader.Close();
                    return tags;
                }
            }
        }

        //create ability to get a single, specified tag when needed.
        public Tag GetTagById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Name FROM Tag WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if(reader.Read())
                    {
                        Tag tag = new Tag
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };
                        reader.Close();
                        return tag;
                    }
                    else
                    {
                        reader.Close();
                        return null;    
                    }
                }
            }
        }


        //Allow (admin) users to create additional tags.  TODO: Limit to admin.
        public void AddTag(Tag tag)
        {
            using (var conn=Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    INSERT INTO Tag(Name)
                                    OUTPUT INSERTED.ID
                                    VALUES (@name)";
                    cmd.Parameters.AddWithValue("@name", tag.Name);
                    tag.Id = (int)cmd.ExecuteScalar();
                }

            }
        }

        public void DeleteTag(int tagId)
        {
            using(var conn=Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Tag WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", tagId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
