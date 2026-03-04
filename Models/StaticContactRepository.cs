namespace ContactList.Models;

public class StaticContactRepository : IContactRepository
{
    private readonly List<Contact> _contacts = new()
    {
        new Contact { Id = 1, Name = "Alice Smith", Email = "alice@example.com",
            Phone = "503-555-0101", Category = "Work", Notes = "Project manager" },
        new Contact { Id = 2, Name = "Bob Jones", Email = "bob@example.com",
            Phone = "503-555-0102", Category = "Friend" },
        new Contact { Id = 3, Name = "Carol White", Email = "carol@example.com",
            Phone = "503-555-0103", Category = "Family", Notes = "Sister" },
    };
    private int _nextId = 4;

    public IEnumerable<Contact> GetAll() => _contacts;

    public Contact? GetById(int id) =>
        _contacts.FirstOrDefault(c => c.Id == id);

    public void Add(Contact contact)
    {
        contact.Id = _nextId++;
        _contacts.Add(contact);
    }

    public void Remove(int id)
    {
        var contact = GetById(id);
        if (contact != null)
        {
            _contacts.Remove(contact);
        }
    }

    public IEnumerable<string> GetCategories() => Contact.GetCategories();
}
