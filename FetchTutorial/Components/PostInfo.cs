namespace FetchTutorial.Components
{
    public class PostInfo
    {
        public int Id { get; set; } = -1;
        public DateTime PostDate { get; set; } = DateTime.Now;
        public string PostPage { get; set; } = string.Empty;
        public string Action { get; set; }
    }

    
}
