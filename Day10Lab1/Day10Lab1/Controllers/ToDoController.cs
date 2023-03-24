using Day10Lab1c;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day10Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        //Dependecy Injection
        private readonly DataContext _context;

        public ToDoController(DataContext context)
        {
            _context = context;
            //Popola
            if(context.ToDoItems.Count() == 0)
            {
                for(int i=0;i<5;i++)
                {
                    ToDoItem item = new ToDoItem()
                    {
                        Title = $"Item {i}",
                        Description = $"Description {i}",
                        CreationDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(i),
                        PriorityLevel = 10+i,
                        IsDone = false,
                        IsMandatory = false
                    };
                    context.ToDoItems.Add(item);
                }
                context.SaveChanges();
            }
        }

        [HttpGet(Name = "GetToDoItem")]
        public IEnumerable<ToDoItem> Get()
        {
            return from item in _context.ToDoItems
                   select item;

            //return _context.ToDoItems;
        }

        [HttpGet("{id}")]
        public ActionResult<ToDoItem> Get(int id)
        {
            var item = (from idx in _context.ToDoItems
                       where idx.ToDoItemID == id
                       select idx).FirstOrDefault();

            if(item == null)
            {
                return NotFound("ToDoItem Not in list");
            }
            return Ok(item);
        }

        /*/
            La prossima cosa che devi fare
        //*/
        [HttpGet("ProssimoTodo")]
        public ActionResult<ToDoItem> GetProx()
        {
            if (_context.ToDoItems == null)
            {
                return NotFound();
            }

            var toDo = (from item in _context.ToDoItems
                        orderby item.DueDate
                        select item).FirstOrDefault();
            if (toDo == null)
            {
                return NotFound();
            }

            return toDo;

        }

        /*/
            Tutti i To Do che sono ancora da fare 
        //*/
        [HttpGet("AllIsNotComplete")]
        public ActionResult<IEnumerable<ToDoItem>> GetAllIsNotComplete()
        {
            if (_context.ToDoItems == null)
            {
                return NotFound();
            }
            var todoNotComplete = (from item in _context.ToDoItems
                                   where item.IsDone == false
                                   select item).ToList();
            if (todoNotComplete.Count == 0)
            {
                return NotFound();
            }
            return todoNotComplete;
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id,ToDoItem item) 
        {
            var todo = _context.ToDoItems
                .Where(idx=>idx.ToDoItemID == id)
                .FirstOrDefault();
            if(todo == null)
            {
                //return NotFound();
                _context.ToDoItems.Add(item);
                _context.SaveChanges();
                return NotFound("ToDoItem not in list but was created");
            }
            //Copy required fields to Update
            todo.Title = item.Title;
            todo.Description = item.Description;
            todo.DueDate = item.DueDate;
            todo.CreationDate = item.CreationDate;
            todo.PriorityLevel = item.PriorityLevel;
            todo.IsDone = item.IsDone;
            todo.IsMandatory = item.IsMandatory;
            //
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = (from idx in _context.ToDoItems
                        where idx.ToDoItemID == id
                        select idx).FirstOrDefault();

            if (item == null)
            {
                return NotFound("ToDoItem Not in list");
            }
            _context.ToDoItems.Remove(item);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost(Name = "PutToDoItem")]
        public ToDoItem Put(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            _context.SaveChanges();
            return item;
        }
    }
}
