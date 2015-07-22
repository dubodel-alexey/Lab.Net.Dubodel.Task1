using System.Collections.Generic;

namespace task3.Models
{
    public class CustomersViewModel
    {
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
        public List<Dictionary<string, string>> Items { get; set; }
        public List<CommentViewModel> CommentList { get; set; }
        public CommentViewModel CommentView { get; set; }
    }
}