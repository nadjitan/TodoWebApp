using System.Globalization;

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
            string dt = new DateTime(DateTime.Now.Ticks, DateTimeKind.Unspecified).ToString();
            DateTime dateTime = DateTime.Parse(dt, CultureInfo.CurrentCulture);
            CreatedDate = dateTime;
        }
    }
}