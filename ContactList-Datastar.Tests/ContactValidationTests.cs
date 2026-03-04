using System.ComponentModel.DataAnnotations;
using ContactList.Models;
using Xunit;

namespace ContactList.Tests;

public class ContactValidationTests
{
    private static List<ValidationResult> Validate(Contact contact)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(contact);
        Validator.TryValidateObject(contact, context, results, validateAllProperties: true);
        return results;
    }

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

        var errors = Validate(contact);

        Assert.Empty(errors);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void MissingName_FailsValidation(string? name)
    {
        var contact = new Contact { Name = name!, Category = "Friend" };

        var errors = Validate(contact);

        Assert.Contains(errors, e => e.MemberNames.Contains("Name"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void MissingCategory_FailsValidation(string? category)
    {
        var contact = new Contact { Name = "Test", Category = category! };

        var errors = Validate(contact);

        Assert.Contains(errors, e => e.MemberNames.Contains("Category"));
    }

    [Theory]
    [InlineData("not-an-email")]
    [InlineData("missing@")]
    [InlineData("@missing.com")]
    public void InvalidEmail_FailsValidation(string email)
    {
        var contact = new Contact { Name = "Test", Category = "Friend", Email = email };

        var errors = Validate(contact);

        Assert.Contains(errors, e => e.MemberNames.Contains("Email"));
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("phone!@#")]
    public void InvalidPhone_FailsValidation(string phone)
    {
        var contact = new Contact { Name = "Test", Category = "Friend", Phone = phone };

        var errors = Validate(contact);

        Assert.Contains(errors, e => e.MemberNames.Contains("Phone"));
    }
}
