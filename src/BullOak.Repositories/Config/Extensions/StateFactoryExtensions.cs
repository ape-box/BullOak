﻿namespace BullOak.Repositories.Config
{
    using System;
    using System.Collections.Generic;
    using BullOak.Repositories.StateEmit;

    public static class StateFactoryExtensions
    {
        public static IConfigureThreadSafety UseActivator(this IConfigureStateFactory stateFactoryConfiguration)
            => stateFactoryConfiguration.WithStateFactory(new ActivatorStateFactory());

        public static IConfigureThreadSafety With(this IConfigureStateFactory stateFactoryConfiguration, Func<Type, Func<object>> container)
            => stateFactoryConfiguration.WithStateFactory(new ContainerFactory(container));
    }

    internal class ActivatorStateFactory : BaseTypeFactory
    {
        private EmittedTypeFactory typeFactory = new EmittedTypeFactory();

        public override object GetState(Type type)
            => Activator.CreateInstance(type);
    }
}
