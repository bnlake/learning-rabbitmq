using Domain.Models;

namespace api.Services;
public class ContentService
{
    private readonly ICollection<Content> contents = new List<Content>{
            new Content { Title = "Headaches", Id = new Guid("9fd599dc-6aff-4788-86c6-f49afd9a3cfd") },
            new Content { Title = "Heart Attack", Id = new Guid("498df68d-9cda-4042-aa63-52b5fc1d43ee") },
            new Content { Title = "Work Stress", Id = new Guid("dd924dc0-83c9-42a0-994a-d22a9b53e273") }
        };

    public ICollection<Content> GetAll() => contents;

    public Content Get(Guid id) => contents.First(e => e.Id.Equals(id));

    public bool Exists(Guid id) => contents.Any(e => e.Id.Equals(id));
}