using BlazorTallerLive.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorTallerLive.Client.Pages
{
    public partial class AddPost
    {
        int Id,userId;
        string Title, Body;
        
        void SaveChanges()
        {
            Post newPost = new Post
            {
                Body = Body,
                Id = Id,
                Title = Title,
                UserId = userId
            };
            Console.WriteLine($"nuevo post= {newPost.Id} {newPost.Title}");
        }


    }
}
