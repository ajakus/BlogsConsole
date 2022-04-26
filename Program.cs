using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace BlogsConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            try
            {

                
                var db = new BloggingContext();
                



                string choice;

                Console.WriteLine("1) Display Blogs");
                Console.WriteLine("2) Add Blog");
                Console.WriteLine("3) Display Posts");
                Console.WriteLine("4) Add Post");
                choice = Console.ReadLine();




                if (choice == "1")
                {
                    // Display all Blogs from the database
                var query = db.Blogs.OrderBy(b => b.Name);

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name, item.BlogId);
                }

                }
                else if (choice == "2")
                {
                    // Create and save a new Blog
                Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();
                var blog = new Blog { Name = name };
                using ( db = new BloggingContext())
                {
                    db.AddBlog(blog);
                    logger.Info("Blog added - {name}", name);
                    db.SaveChanges();


                }

                }
                else if (choice == "3")
                {
                    // Searchstring to search for blog name
                    Console.WriteLine("Please enter the Blog ID: (enter 1)");
                    int blogIDSearch = Convert.ToInt32(Console.ReadLine());
                    var blog_Search = db.Blogs.Where(x => x.BlogId == blogIDSearch).FirstOrDefault();
                    var post_Search = db.Posts.Where(x => x.BlogId == blogIDSearch);
                    System.Console.WriteLine($"{post_Search.Count()} Posts for blog {blog_Search.Name}");
                    
                    

                    Console.WriteLine("All Posts in blog:");
                    foreach (var post in blog_Search.Posts)
                    {

                        System.Console.WriteLine($"\tPost {post.PostId} {post.Title} {post.Content}");
                    }

                }
                else if (choice == "4")
                {

                    var post = new Post();
                    //add post to blog
                    Console.WriteLine("Enter the Blog ID you wish to post in: ");
                    post.BlogId = Convert.ToInt32(Console.ReadLine());
                    // Create and save a new Post
                    Console.Write("Enter a title for a new Post: ");
                    post.Title = Console.ReadLine();
                    Console.WriteLine("Enter some content: ");
                    post.Content = Console.ReadLine();

                   

                    
                    using (db = new BloggingContext())
                    {
                        db.Add(post);
                        logger.Info("Post added - {title}", post.Title);
                        db.SaveChanges();


                    }
                }
                








            }
            catch (Exception ex)
            {
                //logger.Error(ex.Message);
            }

            //logger.Info("Program ended");
        }
    }
}
