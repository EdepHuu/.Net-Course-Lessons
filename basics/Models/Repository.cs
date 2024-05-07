namespace basics.Models 
{
    public class Repository
    {
        private static readonly List<Course> _courses = new();

        static Repository()
        {
            _courses = new List<Course>()
            {
                new Course() {
                    ID = 1 ,
                    Title = "Metehan", 
                    Image="1.JPG",
                    Tags = new string[] {"aspnet","Web gelistirme"},
                    isActivate = true,
                    isHome = true,    
                },

                new Course() {
                    ID = 2 ,Title = "Betüş", 
                    Image="2.JPG",
                    Tags = new string[] {"php","Web gelistirme"},
                    isActivate = true,
                    isHome = true,
                    },

                new Course() {
                    ID = 3 ,Title = "Askim", 
                    Image="3.JPG",
                    isActivate = false,
                    isHome = false,
                    }
            };
        }

        public static List<Course> courses
        {
            get
            {
                return _courses;
            }
        }

        public static Course? GetByID(int? id)
        {
        return _courses.FirstOrDefault(c => c.ID == id);
        }

    }
}