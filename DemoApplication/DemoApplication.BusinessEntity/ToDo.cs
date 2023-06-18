namespace DemoApplication.BusinessEntity
{
    public class ToDo
    {
        public int Id { get; set; }
        public string ToDoItems { get; set; }
        public DateTime AssignDueDates { get; set; }
        public string Comments { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
    }
}