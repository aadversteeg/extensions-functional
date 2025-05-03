using System;
using System.Collections.Generic;

namespace Ave.Extensions.Functional
{
	/// <summary>
	/// Represents a discriminated union of a success value of type T or a failure value of type E.
	/// This type is particularly useful for error handling and railway-oriented programming.
	/// </summary>
	/// <typeparam name="T">The type of the value when the Result is successful.</typeparam>
	/// <typeparam name="E">The type of the error when the Result is a failure.</typeparam>
	public struct Result<T,E> : IEquatable<Result<T,E>>
	{
		private readonly bool _isSuccess;
		private readonly E? _error;
		private readonly T? _value;

		private Result(bool isSuccess, T? value, E? error)
		{
			_isSuccess = isSuccess;
			_value = value;
			_error = error;
		}

		/// <summary>
		/// Gets a value indicating whether the Result represents a success.
		/// </summary>
		public bool IsSuccess => _isSuccess;

		/// <summary>
		/// Gets a value indicating whether the Result represents a failure.
		/// </summary>
		public bool IsFailure => !_isSuccess;

		/// <summary>
		/// Gets the error value if the Result represents a failure.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown when attempting to access Error on a successful Result.</exception>
		public E Error
		{ 
			get 
			{ 
				if(_isSuccess)
				{
					throw new InvalidOperationException("Property Error of Result cannot be accessed because Result is successful.");
				}

				return _error!; 
			} 
		}

		/// <summary>
		/// Gets the success value if the Result represents a success.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown when attempting to access Value on a failed Result.</exception>
		public T Value
		{
			get
			{
				if (!_isSuccess)
				{
					throw new InvalidOperationException("Property Value of Result cannot be accessed because Result is failure.");
				}

				return _value!;
			}
		}

		/// <summary>
		/// Creates a new successful Result containing the specified value.
		/// </summary>
		/// <param name="value">The value to store in the successful Result.</param>
		/// <returns>A new Result instance representing a success with the specified value.</returns>
		public static Result<T,E> Success(T value)
		{
			return new Result<T,E>(true, value, default);
		}

		/// <summary>
		/// Creates a new failed Result containing the specified error.
		/// </summary>
		/// <param name="error">The error to store in the failed Result.</param>
		/// <returns>A new Result instance representing a failure with the specified error.</returns>
		public static Result<T,E> Failure(E error)
		{
			return new Result<T,E>(false, default, error);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>A hash code value that represents this instance.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hash = 17;
				hash = hash * 23 + _isSuccess.GetHashCode();
				hash = hash * 23 + (_isSuccess ? (_value?.GetHashCode() ?? 0) : (_error?.GetHashCode() ?? 0));
				return hash;
			}
		}

		/// <summary>
		/// Determines whether this instance equals another Result instance.
		/// </summary>
		/// <param name="other">The Result to compare with.</param>
		/// <returns>true if the instances are equal; otherwise, false.</returns>
		public bool Equals(Result<T,E> other)
		{
			if (_isSuccess != other._isSuccess)
				return false;

			if (_isSuccess)
				return EqualityComparer<T>.Default.Equals(_value, other._value);
			
			return EqualityComparer<E>.Default.Equals(_error, other._error);
		}

		/// <summary>
		/// Determines whether this instance equals another object.
		/// </summary>
		/// <param name="obj">The object to compare with.</param>
		/// <returns>true if the object is a Result and equals this instance; otherwise, false.</returns>
		public override bool Equals(object? obj) =>
			obj is Result<T,E> other && Equals(other);

		public static bool operator ==(Result<T,E> left, Result<T,E> right) => left.Equals(right);
		public static bool operator !=(Result<T,E> left, Result<T,E> right) => !left.Equals(right);
	}
}