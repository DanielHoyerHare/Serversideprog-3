using BlazorApp1.Models;

namespace BlazorApp1.Codes
{
    public class ToDoHandler
    {
        public bool SubmitToDo(ToDoContext _toDoContext, ToDoList toDoItem)
        {
            _toDoContext.ToDoLists.Add(toDoItem);
            _toDoContext.SaveChanges();
            return true;
        }
    }
}
