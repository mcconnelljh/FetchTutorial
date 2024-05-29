using FetchTutorial.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace FetchTutorial.Pages
{

    public class PostFormDataWithFetchAPIModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<PostInfo> posts;
        private readonly ICookieService _cookieService;
        private const string Page = nameof(PostFormDataWithFetchAPIModel);

        public PostFormDataWithFetchAPIModel(ILogger<IndexModel> logger, ICookieService cookieService)
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

        public PartialViewResult OnPostFormWithFetch([FromBody] PostInfo data)
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
            posts.Add(data);
            var json = JsonSerializer.Serialize(posts);
            _cookieService.Set(Helpers.PostsKey, json, 10);

            return Partial("_PagePosts", posts);
        }
    }
}
