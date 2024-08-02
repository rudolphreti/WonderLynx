namespace App.Models
{
    public class UpdateReference : ReferenceItem
    {
        public List<string> TagsToUpdate { get; set; } = [];
        public string? CategoryToUpdate { get; set; }
        public string? TypeToUpdate { get; set; }

    }

}
