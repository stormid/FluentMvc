using FluentMvc.Configuration;
using FluentMvc.Conventions;
using NUnit.Framework;
using Rhino.Mocks;

namespace FluentMvc.Spec.Unit.Conventions.FilterConventions
{
    [TestFixture]
    public class when_applying_the_conventions : SpecificationBase
    {
        private FilterConventionCollection filterConventionCollection;
        private IFilterRegistration filterRegistration;

        public override void Given()
        {
            filterConventionCollection = new FilterConventionCollection(FilterConventionActivator.Default);
            filterConventionCollection
                .LoadFromAssemblyContaining<TestConvention>();

            filterRegistration = CreateStub<IFilterRegistration>();
        }

        public override void Because()
        {
            filterConventionCollection.ApplyConventions(filterRegistration);
        }

        [Test]
        public void conventions_should_be_run()
        {
            TestConvention.WasExecuted.ShouldBeTrue();
        }

        [Test]
        public void should_add_the_filter()
        {
            filterRegistration.AssertWasCalled(x => x.WithFilter<TestActionFilter>());
        }
    }

    public class TestConvention : IFilterConvention
    {
        public static bool WasExecuted;

        public void ApplyConvention(IFilterRegistration filterRegistration)
        {
            WasExecuted = true;
            filterRegistration.WithFilter<TestActionFilter>();
        }
    }
}