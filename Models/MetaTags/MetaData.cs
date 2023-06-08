namespace LampStore.Models.MetaTags;

public class MetaData
{
    public int Id { get; set; }
    public string Title { get; set; } = "Lights 4 Apart";
    public string Description { get; set; } = "Lights 4 Apart";
    
    public long? CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}