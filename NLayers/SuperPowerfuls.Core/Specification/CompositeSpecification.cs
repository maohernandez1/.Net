namespace SuperPowerfuls.Core.Specification
{
    using SuperPowerfuls.Core.Specification.Contract;
    /// <summary>
    /// Base class for composite specifications
    /// </summary>
    /// <typeparam name="T">Type of entity that check this specification</typeparam>
    public abstract class CompositeSpecification<T> : ISpecification<T> where T : class
    {
        public abstract bool IsSatisfiedBy(T o);

        public ISpecification<T> And(ISpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }
        public ISpecification<T> Or(ISpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }
        public ISpecification<T> Not(ISpecification<T> specification)
        {
            return new NotSpecification<T>(specification);
        }

    }
}
