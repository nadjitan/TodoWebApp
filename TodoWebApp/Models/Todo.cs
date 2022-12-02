namespace TodoWebApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool Done { get; set; } = false;
        public DateTime CreatedDate { get; set; }

        public Todo()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
