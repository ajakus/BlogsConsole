namespace BlogsConsole
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }

        //framework entity relationship
        public virtual Blog Blog { get; set; }
    }
}
