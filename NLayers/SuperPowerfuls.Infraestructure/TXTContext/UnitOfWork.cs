namespace SuperPowerfuls.Infraestructure.TXTContext
{
    using SuperPowerfuls.Core.Contracts;
    using System;
    using System.Collections.Generic;

    public class UnitOfWork : DbContext, IUnitOfWork
    {
        public IEnumerable<T> CreateSet<T>()
            where T : class
        {
            return base.Set<T>();
        }

        public void CommitAndRefreshChanges()
        {
            throw new NotImplementedException();
        }

        public void RollbackChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //this.Dispose();
        }


        int IUnitOfWork.Commit<T>(List<T> objects)
        {
            return base.SaveChanges<T>(objects);
        }
    }
}
