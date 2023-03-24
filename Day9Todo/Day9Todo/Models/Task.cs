namespace Day9Todo.Models
{
    public class Task
    {
        public enum ItemPriority
        {
            Low,Medium,High
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsComplete { get; set; }
        public ItemPriority Priority { get; set; }
        public Task() 
        {
            Title = Description = string.Empty;
            Created = DateTime.Now;
            Priority = ItemPriority.Low;
        }
    }
}
