using BlazorTallerLive.Shared;
using Microsoft.AspNetCore.Components;
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
        [Parameter]
        public EventCallback<Post> OnAddedPost { get; set; }

        async Task SaveChanges()
        {
            Post newPost = new Post
            {
                Body = Body,
                Id = Id,
                Title = Title,
                UserId = userId
            };
            await OnAddedPost.InvokeAsync(newPost);
        }
        


    }
}
