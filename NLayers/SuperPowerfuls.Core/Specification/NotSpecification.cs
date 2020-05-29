namespace SuperPowerfuls.Core.Specification
{
    using SuperPowerfuls.Core.Specification.Contract;
    /// <summary>
    /// A logic Not Specification
    /// </summary>
    /// <typeparam name="T">Type of entity that check this specification</typeparam>
    public sealed class NotSpecification<T> : CompositeSpecification<T> where T : class
    {
        ISpecification<T> specification;

        public NotSpecification(ISpecification<T> spec)
        {
            this.specification = spec;
        }

        public override bool IsSatisfiedBy(T o)
        {
            return !this.specification.IsSatisfiedBy(o);
        }

    }
}
