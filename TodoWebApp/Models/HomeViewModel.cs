using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TodoWebApp.Models
{
    public class HomeViewModel
    {
        //https://learn.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-2.1#the-input-tag-helper
        [Required]
        public string Title { get; set; } = "";
        [Required]
        public bool Done { get; set; } = false;
        [Required]
        public DateTime CreatedDate { get; set; }

        public List<Todo> Todos { get; set; } = new List<Todo>();

        public HomeViewModel()
        { 
            CreatedDate = DateTime.UtcNow;
        }
    }
}
