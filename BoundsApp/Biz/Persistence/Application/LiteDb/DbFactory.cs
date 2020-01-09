using BoundsApp.Biz.Core.Application.Global;
using BoundsApp.Biz.Core.Application.LiteDb;
using LiteDB;

namespace BoundsApp.Biz.Persistence.Application.LiteDb
{
    /// <summary>
    /// Creates LiteDbObjects depending on the Factory setting
    /// </summary>
    public class DbFactory<T> : IFactory<IDb<T>>
    {
        /// <summary>
        /// LiteDbConfiguration
        /// set on Production => create a real LiteDb objects which runs on a real lite.db in you configured litedb location 
        /// <see cref="ConnectionString"/>
        /// </summary>
        public enum Configuration
        {
            Production
        }

        private readonly Configuration _factoryConfiguration;

        public DbFactory(Configuration factoryConfiguration)
        {
            _factoryConfiguration = factoryConfiguration;
        }

         

        /// <summary>
        /// Generate a new Instance of IDb
        /// </summary>
        public IDb<T> Get()
        {
            switch (_factoryConfiguration)
            {
                case Configuration.Production:
                    // set factory on production mode
                    return new Db<T>();
                default:
                    return null;
            }
        }
    }
}
