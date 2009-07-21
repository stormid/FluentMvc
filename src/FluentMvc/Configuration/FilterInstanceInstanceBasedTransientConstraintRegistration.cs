namespace FluentMvc.Configuration
{
    using System;
    using System.Web.Mvc;
    using Constraints;

    public class FilterInstanceInstanceBasedTransientConstraintRegistration : InstanceBasedTransientConstraintRegistration
    {
        private readonly object itemInstance;

        public FilterInstanceInstanceBasedTransientConstraintRegistration(object itemInstance, IConstraint constraint, ActionDescriptor actionDescriptor, ControllerDescriptor controllerDescriptor)
            : base(constraint, actionDescriptor, controllerDescriptor)
        {
            this.itemInstance = itemInstance;
        }

        public override ActionFilterRegistryItem CreateRegistryItem(Type filterType)
        {
            return new ActionFilterRegistryItem(new InstanceItemActivator(itemInstance), Constraint, ActionDescriptor, ControllerDescriptor);
        }
    }
}