using Microsoft.Maui.Storage;
namespace CetTodoApp.Data;

public static class FakeDb
{
    private static readonly TodoDatabase _database;
    
    static FakeDb()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "todo.db3");
        _database = new TodoDatabase(dbPath);
    }
    
    public static List<TodoItem> Data
    {
        get
        {
            return _database.GetItems();
        }
    }
    
    public static void AddToDo(string title, DateTime dueDate)
    {
        var item = new TodoItem
        {
            Title = title,
            DueDate = dueDate,
            IsComplete = false
        };

        _database.AddItem(item);
    }
    
    public static void AddToDo(TodoItem item)
    {
        _database.AddItem(item);
    }
    
    public static void ChageCompletionStatus(TodoItem item)
    {
        item.IsComplete = !item.IsComplete;
        _database.UpdateItem(item);
    }
}