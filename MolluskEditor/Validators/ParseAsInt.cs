using System;
using System.ComponentModel.DataAnnotations;

namespace MolluskEditor.Validators;

public class ParseAsInt : ValidationAttribute
{
    private readonly string? _errorMessage;
    public ParseAsInt(string? errorMessage = null)
    {
        _errorMessage = errorMessage;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;
        
        var stringValue = (string)value;
        if (int.TryParse(stringValue, out int result))
            return ValidationResult.Success;

        return new ValidationResult(_errorMessage ?? "Must be an int");
    }
}
