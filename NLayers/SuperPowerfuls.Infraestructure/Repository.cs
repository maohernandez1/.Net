namespace SuperPowerfuls.Infraestructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SuperPowerfuls.Core.Contracts;
    using SuperPowerfuls.Core.Specification.Contract;
    using SuperPowerfuls.Commons.Log;

    public class Repository<T> : IRepository<T>
        where T : class
    {
        public IUnitOfWork UnitOfWork { get; set; }

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IEnumerable<T> GetAll()
        {
            return GetSet();
        }

        public IEnumerable<T> AllMatching(ISpecification<T> specification)
        {
            List<T> entities = new List<T>();
            try
            {
                entities = GetSet().FindAll(x => specification.IsSatisfiedBy(x));
            }
            catch(ArgumentNullException argNullException)
            {
                ErrorManager.RiseError(argNullException.Message);
            }
            catch (Exception Exception)
            {
                ErrorManager.RiseError(Exception.Message);
            }
            return entities;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<T> GetSet()
        {
            return UnitOfWork.CreateSet<T>().ToList();
        }       
    }
}
