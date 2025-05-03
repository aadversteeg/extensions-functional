using System;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Represents a result type for operations that can fail but don't return a value on success.
    /// This type is particularly useful for operations where you only need to track success/failure status
    /// and potential error information, without carrying a success value.
    /// </summary>
    /// <typeparam name="E">The type of the error when the Result is a failure.</typeparam>
    /// <remarks>
    /// VoidResult{E} is implemented as a wrapper around Result{Unit, E} to provide a more semantically
    /// accurate type for operations that don't return a value. It maintains full compatibility with
    /// Result{Unit, E} through implicit conversions while providing a cleaner API for void operations.
    /// </remarks>
    public readonly struct VoidResult<E> : IEquatable<VoidResult<E>>, IEquatable<Result<Unit, E>>
    {
        private readonly Result<Unit, E> _result;

        private VoidResult(Result<Unit, E> result)
        {
            _result = result;
        }

        /// <summary>
        /// Gets a value indicating whether the Result represents a success.
        /// </summary>
        public bool IsSuccess => _result.IsSuccess;

        /// <summary>
        /// Gets a value indicating whether the Result represents a failure.
        /// </summary>
        public bool IsFailure => _result.IsFailure;

        /// <summary>
        /// Gets the error value if the Result represents a failure.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when attempting to access Error on a successful Result.
        /// </exception>
        public E Error => _result.Error;

        /// <summary>
        /// Creates a new successful Result with no value.
        /// </summary>
        /// <returns>A new Result instance representing a success.</returns>
        public static VoidResult<E> Success()
        {
            return new VoidResult<E>(Result<Unit, E>.Success(Unit.Value));
        }

        /// <summary>
        /// Creates a new failed Result containing the specified error.
        /// </summary>
        /// <param name="error">The error to store in the failed Result.</param>
        /// <returns>A new Result instance representing a failure with the specified error.</returns>
        public static VoidResult<E> Failure(E error)
        {
            return new VoidResult<E>(Result<Unit, E>.Failure(error));
        }

        /// <summary>
        /// Implicitly converts a VoidResult{E} to a Result{Unit, E}.
        /// </summary>
        /// <param name="voidResult">The VoidResult to convert.</param>
        /// <returns>A Result{Unit, E} equivalent to the VoidResult.</returns>
        public static implicit operator Result<Unit, E>(VoidResult<E> voidResult) => voidResult._result;

        /// <summary>
        /// Implicitly converts a Result{Unit, E} to a VoidResult{E}.
        /// </summary>
        /// <param name="result">The Result to convert.</param>
        /// <returns>A VoidResult{E} equivalent to the Result.</returns>
        public static implicit operator VoidResult<E>(Result<Unit, E> result) => new VoidResult<E>(result);

        /// <summary>
        /// Determines whether this instance is equal to another VoidResult{E}.
        /// </summary>
        /// <param name="other">The VoidResult{E} to compare with.</param>
        /// <returns>true if the instances are equal; otherwise, false.</returns>
        public bool Equals(VoidResult<E> other) => _result.Equals(other._result);
        
        /// <summary>
        /// Determines whether this instance is equal to a Result{Unit, E}.
        /// </summary>
        /// <param name="other">The Result{Unit, E} to compare with.</param>
        /// <returns>true if the instances are equal; otherwise, false.</returns>
        public bool Equals(Result<Unit, E> other) => _result.Equals(other);

        /// <summary>
        /// Determines whether this instance is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>
        /// true if the object is either a VoidResult{E} or Result{Unit, E} 
        /// and is equal to this instance; otherwise, false.
        /// </returns>
        public override bool Equals(object obj) =>
            obj switch
            {
                VoidResult<E> voidResult => Equals(voidResult),
                Result<Unit, E> result => Equals(result),
                _ => false
            };

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code value that represents this instance.</returns>
        public override int GetHashCode() => _result.GetHashCode();

        /// <summary>
        /// Determines whether two VoidResult{E} instances are equal.
        /// </summary>
        /// <param name="left">The first VoidResult{E} to compare.</param>
        /// <param name="right">The second VoidResult{E} to compare.</param>
        /// <returns>true if the instances are equal; otherwise, false.</returns>
        public static bool operator ==(VoidResult<E> left, VoidResult<E> right) => left.Equals(right);

        /// <summary>
        /// Determines whether two VoidResult{E} instances are not equal.
        /// </summary>
        /// <param name="left">The first VoidResult{E} to compare.</param>
        /// <param name="right">The second VoidResult{E} to compare.</param>
        /// <returns>true if the instances are not equal; otherwise, false.</returns>
        public static bool operator !=(VoidResult<E> left, VoidResult<E> right) => !left.Equals(right);
        
        /// <summary>
        /// Determines whether a VoidResult{E} is equal to a Result{Unit, E}.
        /// </summary>
        /// <param name="left">The VoidResult{E} to compare.</param>
        /// <param name="right">The Result{Unit, E} to compare.</param>
        /// <returns>true if the instances are equal; otherwise, false.</returns>
        public static bool operator ==(VoidResult<E> left, Result<Unit, E> right) => left.Equals(right);

        /// <summary>
        /// Determines whether a VoidResult{E} is not equal to a Result{Unit, E}.
        /// </summary>
        /// <param name="left">The VoidResult{E} to compare.</param>
        /// <param name="right">The Result{Unit, E} to compare.</param>
        /// <returns>true if the instances are not equal; otherwise, false.</returns>
        public static bool operator !=(VoidResult<E> left, Result<Unit, E> right) => !left.Equals(right);
        
        /// <summary>
        /// Determines whether a Result{Unit, E} is equal to a VoidResult{E}.
        /// </summary>
        /// <param name="left">The Result{Unit, E} to compare.</param>
        /// <param name="right">The VoidResult{E} to compare.</param>
        /// <returns>true if the instances are equal; otherwise, false.</returns>
        public static bool operator ==(Result<Unit, E> left, VoidResult<E> right) => right.Equals(left);

        /// <summary>
        /// Determines whether a Result{Unit, E} is not equal to a VoidResult{E}.
        /// </summary>
        /// <param name="left">The Result{Unit, E} to compare.</param>
        /// <param name="right">The VoidResult{E} to compare.</param>
        /// <returns>true if the instances are not equal; otherwise, false.</returns>
        public static bool operator !=(Result<Unit, E> left, VoidResult<E> right) => !right.Equals(left);
    }
}