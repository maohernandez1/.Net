namespace SuperPowerfuls.Core.Contracts
{
    using SuperPowerfuls.Core.Specification.Contract;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    /// <summary>
    /// Base interface to implement Repository Pattern
    /// </remarks>
    /// <typeparam name="T">Type of entity for this repository </typeparam>
    public interface IRepository<T>:IDisposable
        where T : class
    {
        IUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// Get all elements of type T in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get all elements of type T that matching a
        /// Specification <paramref name="specification"/>
        /// </summary>
        /// <param name="specification">Specification that result meet</param>
        /// <returns></returns>
        IEnumerable<T> AllMatching(ISpecification<T> specification);

    }
}
