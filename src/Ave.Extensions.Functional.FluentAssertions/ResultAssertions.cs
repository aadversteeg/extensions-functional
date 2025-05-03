using FluentAssertions.Execution;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace Ave.Extensions.Functional.FluentAssertions
{
	public class ResultAssertions<T,E> : ReferenceTypeAssertions<Result<T,E>, ResultAssertions<T,E>>
	{
		public ResultAssertions(Result<T,E> instance)
			: base(instance)
		{
		}

		protected override string Identifier => "Result{T,E}";

        public AndConstraint<ResultAssertions<T, E>> Succeed(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(s => s.IsSuccess)
                .FailWith("Expected Result to be successful but it failed");

            return new AndConstraint<ResultAssertions<T, E>>(this);
        }

        public AndConstraint<ResultAssertions<T, E>> SucceedWith(T value, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(s => s.IsSuccess)
                .FailWith("Expected Result to be successful but it failed")
                .Then
                .Given(s => s.Value)
                .ForCondition(v => v!.Equals(value))
                .FailWith("Excepted Result value to be {0} but found {1}", value, Subject.Value);

            return new AndConstraint<ResultAssertions<T, E>>(this);
        }

        public AndConstraint<ResultAssertions<T, E>> Fail(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(s => s.IsFailure)
                .FailWith("Expected Result to be failure but it succeeded");

            return new AndConstraint<ResultAssertions<T, E>>(this);
        }

        public AndConstraint<ResultAssertions<T, E>> FailWith(E error, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(s => s.IsFailure)
                .FailWith("Expected Result to be failure but it succeeded")
                .Then
                .Given(s => s.Error)
                .ForCondition(e => e!.Equals(error))
                .FailWith("Excepted Result value to be {0} but found {1}", error, Subject.Error);

            return new AndConstraint<ResultAssertions<T, E>>(this);
        }
    }
}
