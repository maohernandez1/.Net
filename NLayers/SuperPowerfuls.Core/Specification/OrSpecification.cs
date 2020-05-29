namespace SuperPowerfuls.Core.Specification
{
    using SuperPowerfuls.Core.Specification.Contract;

    /// <summary>
    /// A Logic OR Specification
    /// </summary>
    /// <typeparam name="T">Type of entity that check this specification</typeparam>
    public sealed class OrSpecification<T> : CompositeSpecification<T> where T : class
    {
        ISpecification<T> leftSpecification;
        ISpecification<T> rightSpecification;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.leftSpecification = left;
            this.rightSpecification = right;
        }

        public override bool IsSatisfiedBy(T o)
        {
            return this.leftSpecification.IsSatisfiedBy(o)
                || this.rightSpecification.IsSatisfiedBy(o);
        }

    }
}
