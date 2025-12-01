using SQLite;
namespace CetTodoApp.Data;

public class TodoDatabase
{
    private readonly SQLiteConnection _db;

    public TodoDatabase(string dbPath)
    {
        _db = new SQLiteConnection(dbPath);
        _db.CreateTable<TodoItem>();
    }

    public List<TodoItem> GetItems()
    {
        return _db.Table<TodoItem>()
            .OrderBy(x => x.DueDate)
            .ToList();
    }

    public void AddItem(TodoItem item)
    {
        _db.Insert(item);
    }

    public void UpdateItem(TodoItem item)
    {
        _db.Update(item);
    }
}