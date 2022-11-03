using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class PostDetailsViewModel
    {
        Post Post { get; set; }
        List<Tag> PostTags { get; set; }
    }
}
