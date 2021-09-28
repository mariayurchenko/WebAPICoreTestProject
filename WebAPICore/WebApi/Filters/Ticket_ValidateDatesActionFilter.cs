using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace WebApi.Filters
{
    public class Ticket_ValidateDatesActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (!(context.ActionArguments["ticket"] is Ticket ticket) ||
                string.IsNullOrWhiteSpace(ticket.Owner)) return;

            var isValid = true;

            if (!ticket.EnteredDate.HasValue)
            {
                context.ModelState.AddModelError("EnteredDate", "EnteredDate is required.");
                isValid = false;
            }

            if (ticket.EnteredDate.HasValue && ticket.DueDate.HasValue && ticket.EnteredDate > ticket.DueDate)
            {
                context.ModelState.AddModelError("DueDate", "DueDate has to be later than the EnteredDate.");
                isValid = false;
            }
                 
            if(!isValid) 
                context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
