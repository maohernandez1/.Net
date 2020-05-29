namespace SuperPowerfuls.Core.Specification.Common
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    /// <summary>
    /// Extension methods for adding AND and OR with parameters rebinder
    /// </summary>
    public class ExpressionSpecification<T> : CompositeSpecification<T> where T : class
    {
        private Func<T, bool> expression;
        public ExpressionSpecification(Func<T, bool> expression)
        {
            if (expression == null)
                throw new ArgumentNullException();
            else
                this.expression = expression;
        }

        public override bool IsSatisfiedBy(T o)
        {
            return this.expression(o);
        }
    }
}
