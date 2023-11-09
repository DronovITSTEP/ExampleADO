using System.Data.Common;


namespace ExampleADO.DBWork
{
    public abstract class AbstractQuery 
    {
        public DbProviderFactory factory { get; }
        public DbConnection connection { get; }

        protected CRUDRows cr = null;

        public AbstractQuery(DbConnection connection, DbProviderFactory factory)
        {
            this.connection = connection;
            this.factory = factory;

            cr = new CRUDRows(factory, connection);
        }
        public abstract void Delete<T>(T obj);
        public abstract void Insert<T>(T obj);
        public abstract void Update<T>(T obj);
    }
}
