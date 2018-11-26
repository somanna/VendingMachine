namespace VendingMachine.States
{
    using Autofac;

    using VendingMachine.Common;

    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // We allow circular dependencies because states depend on each other
            // start state is also registered as the initial state for the view model to get the state
            builder.RegisterType<StartState>().As<State>().AsSelf().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).SingleInstance();
            builder.RegisterType<AcceptingCoinsState>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).SingleInstance();
            builder.RegisterType<PaymentCompleteState>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies).SingleInstance();
        }
    }
}