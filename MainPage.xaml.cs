using CetTodoApp.Data;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
namespace CetTodoApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        FakeDb.AddToDo("Test1" ,DateTime.Now.AddDays(-1));
        FakeDb.AddToDo("Test2" ,DateTime.Now.AddDays(1));
        FakeDb.AddToDo("Test3" ,DateTime.Now);
        RefreshListView();
        ;


    }


    private void AddButton_OnClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Title.Text))
        {
            DisplayAlert("Validation", "Please enter a title for the task.", "OK");
            return;
        }
        
        if (DueDate.Date < DateTime.Today)
        {
            DisplayAlert("Validation", "Due date cannot be in the past.", "OK");
            return;
        }
        
        FakeDb.AddToDo(Title.Text, DueDate.Date);
        Title.Text = string.Empty;
        DueDate.Date=DateTime.Now;
        RefreshListView();
    }

    private void RefreshListView()
    {
        TasksListView.ItemsSource = FakeDb.Data
            .Where(x => !x.IsComplete ||
                        (x.IsComplete && x.DueDate > DateTime.Now.AddDays(-1)))
            .ToList();
    }

    private void TasksListView_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is not TodoItem item)
            return;

        FakeDb.ChageCompletionStatus(item);
        RefreshListView();
        
        TasksListView.SelectedItem = null;
    }
}


public class TodoItemToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not TodoItem item)
            return Colors.Black;
        
        if (item.IsComplete)
            return Colors.Gray;
        
        if (item.DueDate.Date < DateTime.Today)
            return Colors.Red;
        
        return Colors.Black;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}