namespace basics.Models 
{
    public class Repository
    {
        private static readonly List<Course> _courses = new();

        static Repository()
        {
            _courses = new List<Course>()
            {
                new Course() {ID = 1 ,Title = "Metehan", Image="1.JPG"},
                new Course() {ID = 2 ,Title = "Betüş", Image="2.JPG"},
                new Course() {ID = 3 ,Title = "Askim", Image="3.JPG"}
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