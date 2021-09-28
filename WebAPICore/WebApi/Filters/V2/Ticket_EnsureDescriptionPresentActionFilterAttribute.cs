using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters.V2
{
    public class Ticket_EnsureDescriptionPresentActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            if (!(context.ActionArguments["ticket"] is Ticket ticket) || ticket.ValidationDescription()) return;

            context.ModelState.AddModelError("Description", "Description is required.");
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
