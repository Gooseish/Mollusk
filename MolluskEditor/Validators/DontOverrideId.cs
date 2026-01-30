using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using MolluskEditor.ViewModels;
using MolluskEngine.Data;

namespace MolluskEditor.Validators;

public class DontOverrideId : ValidationAttribute
{
    private readonly string? _errorMessage;
    public DontOverrideId(string? errorMessage = null)
    {
        _errorMessage = errorMessage;
    }
    
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;
        var parentObject = (IDataViewModel)validationContext.ObjectInstance;
        if (parentObject.CheckIdAvailable((string)value))
            return ValidationResult.Success;
        
        return new ValidationResult(_errorMessage ?? "ID already taken");
    }
    
}
