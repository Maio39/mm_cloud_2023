using Microsoft.AspNetCore.Mvc;

namespace Day7Lab1
{
    public class TodoItem
    {
        public enum ItemStates
        {
            ToDo, Done, Deferred, Overdue
        }
        public enum ItemPriority
        {
            Low,Medium,High
        }
        public string Id { get; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public DateTime DueDate { get; set; }
        public ItemPriority Priority { get; set; }
        public ItemStates status { get; set; }


        public TodoItem()
        {
            DueDate = DateTime.Now;
            Id = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"{DueDate.ToString()} ==> {Title} : {Notes}";
        }
    }

    
    public class TodoList : List<TodoItem>
    {
        public TodoList() { }
        public void AddCasualToDo(int qtyTodoAdd=5)
        {
            string[] titoli =
            {
                    "Cinema","Storia","Matematica","Chiamare","Estetista"
                };
            string[] notes =
            {
                    "Ciao","Come stai","gianni","farfalla","Giacomo"
                };
            
            for (int i = 0; i < qtyTodoAdd; i++)
            {
                Random priority = new Random();
                Random titolo = new Random();
                Random note = new Random();
                Random rand = new Random();

                int indexPriority = rand.Next(1, 4);
                int indexTitolo = titolo.Next(titoli.Length);
                int indexNote = note.Next(notes.Length);

                TodoItem.ItemPriority pri = new TodoItem.ItemPriority();
                switch (indexPriority)
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
                }
                TodoItem todo = new TodoItem()
                {
                    Title = titoli[indexTitolo],
                    Notes = notes[indexNote],
                    Priority = pri
                    
                };
                this.Add(todo);
            }
        }

        /*public void AddTodo(TodoItem toDo)
        {
            this.Add(toDo);
        }*/
    }

    
    /*public class TodoList
    {
        public List<TodoItem> _tdl;

        public TodoList()
        {
            _tdl = new List<TodoItem>();
        }

        public void AddCasualToDo()
        {
            string[] titoli =
            {
                    "Cinema","Storia","Matematica","Chiamare","Estetista"
                };
            string[] notes =
            {
                    "Ciao","Come stai","gianni","farfalla","Giacomo"
                };
            Random titolo = new Random();
            Random note = new Random();
            int indexTitolo = titolo.Next(titoli.Length);
            int indexNote = note.Next(notes.Length);
            TodoItem todo = new TodoItem()
            {
                Title = titoli[indexTitolo],
                Notes = notes[indexNote]
            };
            _tdl.Add(todo);
        }

        public void Add(TodoItem toDo)
        {
            _tdl.Add(toDo);
        }

        public IEnumerable<TodoItem> GetToDoList()
        {
            return _tdl;
        }

    }*/
}
