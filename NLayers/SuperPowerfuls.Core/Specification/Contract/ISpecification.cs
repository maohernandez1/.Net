namespace SuperPowerfuls.Core.Specification.Contract
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Base contract for Specification pattern with Linq and
    /// lambda expression support
    /// Ref : http://martinfowler.com/apsupp/spec.pdf
    /// Ref : http://en.wikipedia.org/wiki/Specification_pattern
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T> where T : class
    {
        bool IsSatisfiedBy(T o);
        ISpecification<T> And(ISpecification<T> specification);
        ISpecification<T> Or(ISpecification<T> specification);
        ISpecification<T> Not(ISpecification<T> specification);
    }
}
