using ContactList.Models;
using Xunit;

namespace ContactList.Tests;

public class StaticContactRepositoryTests
{
    private readonly StaticContactRepository _repo = new();

    [Fact]
    public void GetAll_ReturnsSeededContacts()
    {
        var contacts = _repo.GetAll().ToList();

        Assert.Equal(3, contacts.Count);
        Assert.Contains(contacts, c => c.Name == "Alice Smith");
        Assert.Contains(contacts, c => c.Name == "Bob Jones");
        Assert.Contains(contacts, c => c.Name == "Carol White");
    }

    [Fact]
    public void GetById_ReturnsCorrectContact()
    {
        var contact = _repo.GetById(1);

        Assert.NotNull(contact);
        Assert.Equal("Alice Smith", contact.Name);
    }

    [Fact]
    public void GetById_ReturnsNullForMissingId()
    {
        var contact = _repo.GetById(9999);

        Assert.Null(contact);
    }

    [Fact]
    public void Add_AssignsIdAndAppendsContact()
    {
        var countBefore = _repo.GetAll().Count();
        var newContact = new Contact { Name = "Test User", Category = "Other" };

        _repo.Add(newContact);

        Assert.True(newContact.Id > 0);
        Assert.Equal(countBefore + 1, _repo.GetAll().Count());
        Assert.NotNull(_repo.GetById(newContact.Id));
    }

    [Fact]
    public void Remove_DeletesExistingContact()
    {
        var contact = new Contact { Name = "To Remove", Category = "Other" };
        _repo.Add(contact);
        var countAfter = _repo.GetAll().Count();

        _repo.Remove(contact.Id);

        Assert.Equal(countAfter - 1, _repo.GetAll().Count());
        Assert.Null(_repo.GetById(contact.Id));
    }

    [Fact]
    public void Remove_IsNoOpForMissingId()
    {
        var countBefore = _repo.GetAll().Count();

        _repo.Remove(9999);

        Assert.Equal(countBefore, _repo.GetAll().Count());
    }
}
