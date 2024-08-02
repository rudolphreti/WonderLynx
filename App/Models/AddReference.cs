namespace App.Models
{
    public class AddReference : ReferenceItem
    {
        public List<string> TagsToAdd { get; set; } = [];
        public string? CategoryToAdd { get; set; }
        public string? TypeToAdd { get; set; }

    }
}
