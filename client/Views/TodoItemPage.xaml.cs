using ben.Data;
using ben.Models;

namespace ben.Views;

[QueryProperty("Item", "Item")]
public partial class TodoItemPage : ContentPage
{
    TodoItemDatabase database;

    public TodoItem Item
	{
		get => BindingContext as TodoItem;
		set => BindingContext = value;
	}

    public TodoItemPage(TodoItemDatabase todoItemDatabase)
    {
        InitializeComponent();
        database = todoItemDatabase;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Populate the picker with enum values
        StatusPicker.ItemsSource = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
        // Ensure the picker selection reflects the bound item's Status
        if (Item != null)
            StatusPicker.SelectedItem = Item.Status;

        // Populate priority picker and sync selection
        PriorityPicker.ItemsSource = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToList();
        if (Item != null)
            PriorityPicker.SelectedItem = Item.Priority;
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Item.Name))
        {
            await DisplayAlertAsync("Name Required", "Please enter a name for the todo item.", "OK");
            return;
        }

        await database.SaveItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

    async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (Item.ID == 0)
            return;
        await database.DeleteItemAsync(Item);
        await Shell.Current.GoToAsync("..");
    }

    async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}