using System.ComponentModel.DataAnnotations;

namespace Core.Models.ValidationAttributes
{
    class Ticket_EnsureDueDatePresenceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return validationContext.ObjectInstance is Ticket ticket && !ticket.ValidateDueDatePresence() ? new ValidationResult("Due date is required.") : ValidationResult.Success;
        }
    }
}