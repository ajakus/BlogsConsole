using System.Collections.Generic;
namespace BlogsConsole
{
        public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }

        public virtual List<Post> Posts { get; set; }
    }
}
