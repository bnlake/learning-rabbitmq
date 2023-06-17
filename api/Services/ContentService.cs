using Domain.Models;

namespace api.Services;
public class ContentService
{
    private readonly ICollection<Content> contents = new List<Content>{
            new Content { Title = "Headaches" },
            new Content { Title = "Heart Attack" },
            new Content { Title = "Work Stress" }
        };

    public ICollection<Content> GetAll() => contents;

    public Content Get(Guid id) => contents.First(e => e.Id.Equals(id));

    public bool Exists(Guid id) => contents.Any(e => e.Id.Equals(id));
}