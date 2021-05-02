using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]    
    public class UniqueCharactersAttribute : ValidationAttribute, IClientModelValidator
    {
        private readonly int numberOfUniqueChars;

        public UniqueCharactersAttribute(int numberOfUniqueChars)
        {
            this.numberOfUniqueChars = numberOfUniqueChars;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            else if (value is string myString)
            {
                if (myString.Distinct().Count() >= 6)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(GetErrorMessage(), new List<string> { validationContext.MemberName });
                }

            }
            throw new NotImplementedException($"The attribute {nameof(UniqueCharactersAttribute)} is not implemented for object {value.GetType()}. Uniqueness only is implemented.");
        }

        protected string GetErrorMessage()
        {
            return $"password must have at least 6 unique characters!";
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val", "");
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-password", GetErrorMessage());
            ClientSideAttributeHelper.MergeAttribute(context.Attributes, "data-val-password-uniquecharacters", numberOfUniqueChars.ToString());
        }
    }
}
