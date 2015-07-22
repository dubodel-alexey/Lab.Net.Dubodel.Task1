using System.ComponentModel.DataAnnotations;

namespace task3.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Field is reqiered")]
        [MinLength(5, ErrorMessage = "At least 5 symbols required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Comment")]
        public string Body { get; set; }
    }
}