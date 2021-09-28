using System.ComponentModel.DataAnnotations;

namespace Core.Models.ValidationAttributes
{
    public class Ticket_EnsureFutureDueDateOnCreationAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return validationContext.ObjectInstance is Ticket ticket && !ticket.ValidateFutureDueDate() ? new ValidationResult("Due date has to be in the future.") : ValidationResult.Success;
        }
    }
}