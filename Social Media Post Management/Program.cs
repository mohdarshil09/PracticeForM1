namespace Social_Media_Post_Management
{
    // ========== USER CLASS ==========
    // Holds data for one user on the platform.
    internal class User
    {
        public string UserId { get; set; }      // Unique id (e.g. "U1")
        public string UserName { get; set; }    // Display name
        public string Bio { get; set; }        // Short description
        public int FollowersCount { get; set; } // Number of followers
        public List<string> Following { get; set; } = new(); // UserIds this user follows
    }

    // ========== POST CLASS ==========
    // One post created by a user (text, image, or video).
    internal class Post
    {
        public string PostId { get; set; }     // Unique id (e.g. "P1")
        public string UserId { get; set; }     // Who created it
        public string Content { get; set; }    // Post text/description
        public DateTime PostTime { get; set; } // When it was posted
        public string PostType { get; set; }   // "Text", "Image", or "Video"
        public int Likes { get; set; }         // Like count
        public List<string> Comments { get; set; } = new(); // List of comment strings
    }

    // ========== SOCIAL MEDIA MANAGER ==========
    // Handles users, posts, likes, comments, and queries.
    internal class SocialMediaManager
    {

        // In-memory storage: list of users and list of posts.
        private readonly List<User> _users = new();
        private readonly List<Post> _posts = new();
        private int _nextUserId = 1;  // Used to generate UserId (U1, U2, ...)
        private int _nextPostId = 1;  // Used to generate PostId (P1, P2, ...)

        // Register a new user. Creates User, gives Id, adds to list.
        public void RegisterUser(string userName, string bio)
        {
            var user = new User
            {
                UserId = "U" + _nextUserId++,
                UserName = userName,
                Bio = bio,
                FollowersCount = 0,
                Following = new List<string>()
            };
            _users.Add(user);
        }

        // Create a new post for the given user. type = "Text", "Image", or "Video".
        public void CreatePost(string userId, string content, string type)
        {
            var post = new Post
            {
                PostId = "P" + _nextPostId++,
                UserId = userId,
                Content = content,
                PostTime = DateTime.Now,
                PostType = type,
                Likes = 0,
                Comments = new List<string>()
            };
            _posts.Add(post);
        }

        // Add one like to a post (we don't track who liked; just increment count).
        public void LikePost(string postId, string userId)
        {
            var post = _posts.FirstOrDefault(p => p.PostId == postId);
            if (post != null)
                post.Likes++;
        }

        // Add a comment to a post. Stored as "userId: comment" so we know who said it.
        public void AddComment(string postId, string userId, string comment)
        {
            var post = _posts.FirstOrDefault(p => p.PostId == postId);
            if (post != null)
                post.Comments.Add($"{userId}: {comment}");
        }

        // Group all posts by their UserId. Returns Dictionary<UserId, List of that user's posts>.
        public Dictionary<string, List<Post>> GroupPostsByUser()
        {
            var result = new Dictionary<string, List<Post>>();
            foreach (var post in _posts)
            {
                if (!result.ContainsKey(post.UserId))
                    result[post.UserId] = new List<Post>();
                result[post.UserId].Add(post);
            }
            return result;
        }

        // Get posts that have at least minLikes (trending). Sorted by Likes descending.
        public List<Post> GetTrendingPosts(int minLikes)
        {
            return _posts
                .Where(p => p.Likes >= minLikes)
                .OrderByDescending(p => p.Likes)
                .ToList();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = new SocialMediaManager();

            // --- Use case 1: User registration ---
            manager.RegisterUser("Alice", "Love coding!");
            manager.RegisterUser("Bob", "Photography enthusiast");

            // --- Use case 2: Create different types of posts ---
            manager.CreatePost("U1", "Hello world!", "Text");
            manager.CreatePost("U1", "Check out this sunset photo", "Image");
            manager.CreatePost("U2", "My first vlog", "Video");

            // --- Use case 3: Like and comment on posts ---
            manager.LikePost("P1", "U2");
            manager.LikePost("P1", "U1");
            manager.LikePost("P2", "U2");
            manager.AddComment("P1", "U2", "Great post!");
            manager.AddComment("P1", "U1", "Thanks!");

            // --- Use case 4: Group posts by user ---
            var byUser = manager.GroupPostsByUser();
            Console.WriteLine("=== Posts grouped by user ===");
            foreach (var kv in byUser)
            {
                Console.WriteLine($"User {kv.Key}: {kv.Value.Count} post(s)");
                foreach (var p in kv.Value)
                    Console.WriteLine($"  - {p.PostId} ({p.PostType}): {p.Content}");
            }

            // --- Use case 5: Find trending posts (min 1 like) ---
            var trending = manager.GetTrendingPosts(minLikes: 1);
            Console.WriteLine("\n=== Trending posts (min 1 like) ===");
            foreach (var p in trending)
                Console.WriteLine($"{p.PostId}: {p.Content} - {p.Likes} likes");
        }
    }
}
