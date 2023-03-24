namespace Day5Lab1
{
    public class MySingleton
    {
        static MySingleton _istance;
        protected MySingleton() { }

        public MySingleton GetSingleton() 
        {
            if(_istance == null)
            {
                _istance = new MySingleton();
            }
            return _istance;
        }
    }
    public class ToDoItem
    {
        public enum ItemStates
        {
            ToDo,Done,Deferred,Overdue
        }
        public string Id { get; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public DateTime DueDate { get; set; } 
        public int Priority { get; set; }
        public ItemStates status { get; set; }
        public bool IsDone { get { return status == ItemStates.Done; } }

        public ToDoItem()
        {
            DueDate = DateTime.Now;
            Id = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"{IsDone} {DueDate.ToString()} ==> {Title} : {Notes}";
        }
    }

    public class ToDoList
    {
        public List<ToDoItem> _tdl = new List<ToDoItem>(); 

        public ToDoList()
        {
        }

       public void Add(ToDoItem toDo)
        {
            _tdl.Add(toDo);
        }

        public IEnumerable<ToDoItem> GetToDoList()
        {
            return _tdl;
        }
        
    }
}
