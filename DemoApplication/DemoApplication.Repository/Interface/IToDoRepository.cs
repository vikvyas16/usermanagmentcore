using DemoApplication.BusinessEntity;

namespace DemoApplication.Repository.Interface
{
    public interface IToDoRepository
    {
        List<ToDo> GetAllToDoItems();
        ToDo GetToDoItemByToDoId(int todoId);
        ToDo ManageToDo(ToDo obj);
        bool AssignedToDoList(int toDoId, int assigndUserId);
    }
}