using System.ComponentModel.DataAnnotations;

namespace Core.Models.ValidationAttributes
{
    class Ticket_EnsureDueDateAfterReportDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return validationContext.ObjectInstance is Ticket ticket && !ticket.ValidateDueDateAfterReportDate() ? new ValidationResult("Due date has to be after Report date.") : ValidationResult.Success;
        }
    }
}