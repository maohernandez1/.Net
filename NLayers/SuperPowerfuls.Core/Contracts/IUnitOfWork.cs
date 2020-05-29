namespace SuperPowerfuls.Core.Contracts
{
    using System;
    using System.Collections.Generic;
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Creates the set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> CreateSet<T>() where T : class;

        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        ///<remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,  
        /// then an exception is thrown
        ///</remarks>
        int Commit<T>(List<T> objects) where T : class;
    }
}
