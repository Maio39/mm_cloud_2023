using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Day7Lab1.Pages
{
    
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public TodoList _ListaTodo;

        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Notes { get; set; }
        [BindProperty]
        public DateTime DueDate { get; set; }
        [BindProperty]
        public TodoItem.ItemPriority Priority { get; set; }


        public IndexModel(ILogger<IndexModel> logger,TodoList lista)
        {
            _logger = logger;
            _ListaTodo = lista;
        }

        public void OnGet()
        {
            if(_ListaTodo.Count == 0) 
            {
                //FOR TESTING PURPOSE
                _ListaTodo.AddCasualToDo();
            }
            DueDate = DateTime.Now;
        }

        public void OnPost()
        {
            //TodoItem.ItemPriority pri = new TodoItem.ItemPriority();
            /*switch (Priority)
            {
                case 1:
                    pri = TodoItem.ItemPriority.Low;
                    break;
                case 2:
                    pri = TodoItem.ItemPriority.Medium;
                    break;
                case 3:
                    pri = TodoItem.ItemPriority.High;
                    break;
                default:
                    pri = TodoItem.ItemPriority.Low;
                    break;
            }*/
            TodoItem newTodo = new TodoItem()
            {
                Title = this.Title,
                Notes = this.Notes,
                DueDate = this.DueDate,
                Priority = this.Priority
            };
            _ListaTodo.Add(newTodo);
        }
    }
}