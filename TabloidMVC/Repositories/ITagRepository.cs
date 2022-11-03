using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAll();
        Tag GetTagById(int id);
        public void AddTag(Tag tag);
        public void DeleteTag(int tagId);
        public void EditTag(Tag tag);

        public void TagPost(PostTag postTag);
        List<Tag> GetTagsForPost(int postId);
    }
}
