using System;
using BoundsApp.Biz.Core.Application.Global;

namespace BoundsApp.Biz.Persistence.Application.Global
{
    public class Factory<T> : IFactory<T>
    {
        private readonly Func<T> _factoryFunc;

        /// <summary>
        /// Simple implementation of the IFactory interface
        /// which takes a Func, that defines what gets returned
        /// </summary>
        /// <exception cref="ArgumentNullException">throws if the factory func is null</exception> 
        public Factory(Func<T> factoryFunc)
        {
            _factoryFunc = factoryFunc ?? throw new ArgumentNullException(nameof(factoryFunc));
        }

        public T Get() => _factoryFunc();
    }
}