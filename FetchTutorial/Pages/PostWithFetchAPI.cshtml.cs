using FetchTutorial.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

namespace FetchTutorial.Pages
{
    [IgnoreAntiforgeryToken]
    public class PostWithFetchAPIModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<PostInfo> posts;
        private readonly ICookieService _cookieService;
        private const string Page = nameof(PostWithFetchAPIModel);

        public PostWithFetchAPIModel(ILogger<IndexModel> logger, ICookieService cookieService)
        {
            _logger = logger;
            _cookieService = cookieService;
        }
        public void OnGet()
        {
            // check for existing posts
            if (_cookieService.Exists(Helpers.PostsKey))
            {
                // load the existing posts
                posts = JsonSerializer.Deserialize<List<PostInfo>>(_cookieService.Get(Helpers.PostsKey));
            }
            else
            {
                // no posts exist, initialize the list
                posts = new List<PostInfo>();
            }

            // add a new post
            posts.Add(new PostInfo { Id = posts.Count + 1, Action = nameof(OnGet), PostPage = Page });
            var json = JsonSerializer.Serialize(posts);
            _cookieService.Set(Helpers.PostsKey, json, 10);
        }

        public PartialViewResult OnPostAddPost()
        {
            // check for existing posts
            if (_cookieService.Exists(Helpers.PostsKey))
            {
                // load the existing posts
                posts = JsonSerializer.Deserialize<List<PostInfo>>(_cookieService.Get(Helpers.PostsKey));
            }
            else
            {
                // no posts exist, initialize the list
                posts = new List<PostInfo>();
            }

            // add a new post
            posts.Add(new PostInfo { Id = posts.Count + 1, Action = nameof(OnPostAddPost), PostPage = Page });
            var json = JsonSerializer.Serialize(posts);
            _cookieService.Set(Helpers.PostsKey, json, 10);

            return Partial("_PagePosts", posts);
        }
    }
}
