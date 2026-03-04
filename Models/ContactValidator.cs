using System.ComponentModel.DataAnnotations;

namespace ContactList.Models;

public static class ContactValidator
{
    public static Dictionary<string, string> GetErrors(Contact contact)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(contact);
        Validator.TryValidateObject(contact, context, results, validateAllProperties: true);

        return results
            .Where(r => r.MemberNames.Any())
            .ToDictionary(
                r => r.MemberNames.First(),
                r => r.ErrorMessage ?? ""
            );
    }
}
