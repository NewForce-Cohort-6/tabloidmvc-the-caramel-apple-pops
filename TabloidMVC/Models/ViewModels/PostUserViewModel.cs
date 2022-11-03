using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class PostUserViewModel
    {
        //I think I need to get all posts. Maybe posts by user? Maybe just the logged in user?
        public int UserProfileId { get; set; }
        public List<Post> Posts { get;set; }

    }
}
