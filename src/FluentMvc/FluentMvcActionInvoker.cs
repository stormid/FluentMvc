namespace FluentMvc
{
    using System.Web.Mvc;

    public class FluentMvcActionInvoker : ControllerActionInvoker
    {
        private readonly IFluentMvcResolver fluentMvcResolver;

        private FluentMvcActionInvoker(IFluentMvcResolver fluentMvcResolver)
        {
            this.fluentMvcResolver = fluentMvcResolver;
        }

        protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue)
        {
            if (!(actionReturnValue is ActionResult))
                controllerContext.Controller.ViewData.Model = actionReturnValue;

            return fluentMvcResolver.CreateActionResult(ActionResultSelector(actionReturnValue, controllerContext, actionDescriptor));
        }

        private ActionResultSelector ActionResultSelector(object actionReturnValue, ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            return new ActionResultSelector(actionReturnValue, controllerContext, actionDescriptor, actionDescriptor.ControllerDescriptor);
        }

        public static IActionInvoker Create(IFluentMvcResolver fluentMvcFactory)
        {
            return new FluentMvcActionInvoker(fluentMvcFactory);
        }
    }
}