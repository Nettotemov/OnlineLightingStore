using System.ComponentModel.DataAnnotations;
using LampStore.Models.AboutPages;
using LampStore.Models.CollectionsLights;
using LampStore.Models.ProductsPages;
using Newtonsoft.Json;

namespace LampStore.Models.MetaTags;

public class MetaData
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = "Lights 4 Apart";
    public string Description { get; set; } = "Lights 4 Apart";
    public string Url { get; set; } = null!;

    public long? ProductId { get; set; }
    
    [JsonIgnore]
    public Product? Product { get; set; }
    public long? CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public long? AboutPageId { get; set; }
    public AboutPage? AboutPage { get; set; }
    
    public int? CollectionLightId { get; set; }
    public CollectionLight? CollectionLight { get; set; }
}