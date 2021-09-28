using System.ComponentModel.DataAnnotations;

namespace Core.Models.ValidationAttributes
{
    class Ticket_EnsureReportDatePresenceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return validationContext.ObjectInstance is Ticket ticket && !ticket.ValidateReportDatePresence() ? new ValidationResult("Report date is required.") : ValidationResult.Success;
        }
    }
}