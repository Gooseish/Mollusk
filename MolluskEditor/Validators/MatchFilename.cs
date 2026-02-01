using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace MolluskEditor.Validators;

public class MatchFilename : ValidationAttribute
{
    private string _directoryPath;
    private readonly string _errorMessage;
    public MatchFilename(string errorMessage, string directoryPath)
    {
        _directoryPath = directoryPath;
        _errorMessage = errorMessage;
    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;
        
        string[] validFilePaths = Directory.GetFiles(_directoryPath);
        List<string> validFilenames = validFilePaths.Select(
            path => Path.GetFileNameWithoutExtension(path)).ToList();

        var stringValue = (string)value;
        if (validFilenames.Contains(stringValue))
            return ValidationResult.Success;

        return new ValidationResult(_errorMessage);
    }
}
