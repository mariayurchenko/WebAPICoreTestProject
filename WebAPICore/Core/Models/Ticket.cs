using System;
using System.ComponentModel.DataAnnotations;
using Core.Models.ValidationAttributes;

namespace Core.Models
{
    public class Ticket
    {
        public int? TicketId { get; set; }

        [Required]
        public int? ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }

        [StringLength(50)]
        public string Owner { get; set; }

        [Ticket_EnsureDueDateAfterReportDate]
        [Ticket_EnsureFutureDueDateOnCreation]
        [Ticket_EnsureDueDatePresence]
        public DateTime? DueDate { get; set; }
        public DateTime? EnteredDate { get; set; }

        [Ticket_EnsureReportDatePresence]
        public DateTime? ReportDate { get; set; }
        public Project Project { get; set; }

        public bool ValidationDescription()
        {
            return !string.IsNullOrWhiteSpace(Description);
        }

        /// <summary>
        /// When creating a ticket, if due date is entered? it has to be in the feature
        /// </summary>
        public bool ValidateFutureDueDate()
        {
            if (TicketId.HasValue) return true;
            if (!DueDate.HasValue) return true;

            return DueDate.Value > DateTime.Now;
        }

        /// <summary>
        /// When owner is assigned, the report date has to be present
        /// </summary>
        public bool ValidateReportDatePresence()
        {
            return string.IsNullOrWhiteSpace(Owner) || ReportDate.HasValue;
        }

        /// <summary>
        /// When owner is assigned, the due date has to be present
        /// </summary>
        public bool ValidateDueDatePresence()
        {
            return string.IsNullOrWhiteSpace(Owner) || DueDate.HasValue;
        }

        /// <summary>
        /// When due date and report date are present, due date has to be later or equal to report date
        /// </summary>
        public bool ValidateDueDateAfterReportDate()
        {
            if (!DueDate.HasValue || !ReportDate.HasValue) return true;

            return DueDate.Value.Date >= ReportDate.Value.Date;
        }

    }
}
