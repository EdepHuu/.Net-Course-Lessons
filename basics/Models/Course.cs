namespace basics.Models
{
    public class Course
        {
            public int ID { get; set; }
            public string? Title { get; set; } //title'ın "null" olma ihtimaline karşı "?" ekledik
        }
}