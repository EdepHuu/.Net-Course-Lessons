namespace basics.Models
{
    public class Course
        {
            public int ID { get; set; }
            public string? Title { get; set; } //title'ın "null" olma ihtimaline karşı "?" ekledik
        
            public string? Image { get; set; }

            public string? Tags { get; set; }
        
            public bool isHome { get; set; }
            public bool isActivate { get; set; }
        }
}