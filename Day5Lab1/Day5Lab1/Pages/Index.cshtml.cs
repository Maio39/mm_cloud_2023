using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Day5Lab1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ToDoList listaToDo;
        public IndexModel(ILogger<IndexModel> logger, ToDoList list)
        {
            _logger = logger;
            listaToDo = list;

        }

        public void OnGet()
        {
            
            for (int i=0; i<5; i++) 
            {
                listaToDo.Add( new ToDoItem()
                {
                    Title = "to do list",
                    Notes = $"{i}",
                    Priority = 1,
                });
            }
        }
        //metodo 2
        //i nomi passati devono avere gli stessi nomi del name dell index html
        public void OnPost(string title, string notes, DateTime duedate)
        {
            //TODO: Check Data Integrity
            /* metodo 1
            string myTitle = this.Request.Form["title"];
            string myNotes = this.Request.Form["notes"];
            //TODO: try / catch
            DateTime myDateTime = DateTime.Parse(this.Request.Form["duedate"]);
            */

            ToDoItem myItem = new ToDoItem()
            {
                /* metodo 1
                Title = myTitle,
                Notes = myNotes,
                DueDate = myDateTime
                */
                //metodo 2
                Title = title,
                Notes = notes,
                DueDate = duedate
            };

            listaToDo.Add(myItem);
        }
    }
}