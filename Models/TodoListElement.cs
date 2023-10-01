namespace TodoApi.Models
{
    public class TodoListElement
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsChecked { get; set; }
    }
}