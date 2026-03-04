using ContactList.Models;
using Xunit;

namespace ContactList.Tests;

public class ContactValidationTests
{
    [Fact]
    public void ValidContact_PassesValidation()
    {
        var contact = new Contact
        {
            Name = "Jane Doe",
            Email = "jane@example.com",
            Phone = "503-555-0100",
            Category = "Friend"
        };

        var errors = ContactValidator.GetErrors(contact);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void MissingName_FailsValidation(string? name)
    {
        var contact = new Contact { Name = name!, Category = "Friend" };

        var errors = ContactValidator.GetErrors(contact);

        Assert.True(errors.ContainsKey("Name"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void MissingCategory_FailsValidation(string? category)
    {
        var contact = new Contact { Name = "Test", Category = category! };

        var errors = ContactValidator.GetErrors(contact);

        Assert.True(errors.ContainsKey("Category"));
    }

    [Theory]
    [InlineData("not-an-email")]
    [InlineData("missing@")]
    [InlineData("@missing.com")]
    public void InvalidEmail_FailsValidation(string email)
    {
        var contact = new Contact { Name = "Test", Category = "Friend", Email = email };

        var errors = ContactValidator.GetErrors(contact);

        Assert.True(errors.ContainsKey("Email"));
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("phone!@#")]
    public void InvalidPhone_FailsValidation(string phone)
    {
        var contact = new Contact { Name = "Test", Category = "Friend", Phone = phone };

        var errors = ContactValidator.GetErrors(contact);

        Assert.True(errors.ContainsKey("Phone"));
    }
}
