using System;

namespace Ave.Extensions.Functional
{
	/// <summary>
	/// Represents an optional value that may or may not exist.
	/// This type is particularly useful for representing nullable values of both reference and value types,
	/// and for functional programming patterns.
	/// </summary>
	/// <typeparam name="T">The type of the optional value.</typeparam>
	public readonly struct Maybe<T>
	{
		private readonly bool _hasValue;
		private readonly T? _value;

		private Maybe(bool hasValue, T? value)
		{
			_hasValue = hasValue;
			_value = value;
		}

		/// <summary>
		/// Gets the value if it exists.
		/// </summary>
		/// <exception cref="InvalidOperationException">Thrown when attempting to access Value when HasValue is false.</exception>
		/// <remarks>
		/// Always check HasValue before accessing Value to avoid exceptions.
		/// </remarks>
		public T Value
		{
			get 
			{
				if (!_hasValue)
				{
					throw new InvalidOperationException("Property Value of Maybe cannot be accesses because the Maybe has no value.");
				}
				return _value!; 
			}
		}

		/// <summary>
		/// Gets a Maybe instance representing no value (None).
		/// </summary>
		public static Maybe<T> None => new Maybe<T>(false, default);

		/// <summary>
		/// Gets a value indicating whether this Maybe instance has a value.
		/// </summary>
		public bool HasValue => _hasValue;

		/// <summary>
		/// Gets a value indicating whether this Maybe instance has no value.
		/// </summary>
		public bool HasNoValue => !_hasValue;

		/// <summary>
		/// Implicitly converts a value of type T to a Maybe{T}.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <remarks>
		/// This operator enables seamless conversion from values to Maybe instances,
		/// making it more convenient to work with Maybe types in your code.
		/// </remarks>
		public static implicit operator Maybe<T>(T? value)
		{
			if (value is Maybe<T> valueAsMaybe)
			{
				return valueAsMaybe;
			}

			return Maybe.From(value!);
		}

		/// <summary>
		/// Creates a new Maybe instance containing the specified value.
		/// </summary>
		/// <param name="source">The value to wrap in a Maybe instance.</param>
		/// <returns>A new Maybe instance containing the specified value.</returns>
		public static Maybe<T> From(T? source)
		{
			return new Maybe<T>(true, source);
		}

		/// <summary>
		/// Returns a string representation of the Maybe instance.
		/// </summary>
		/// <returns>
		/// Returns "(No Value)" if the Maybe has no value,
		/// otherwise returns the string representation of the contained value.
		/// </returns>
		public override string ToString()
		{
			if (HasNoValue)
				return "(No Value)";

			return _value?.ToString() ?? "(null)";
		}
	}

	/// <summary>
	/// Provides static factory methods for creating Maybe instances.
	/// </summary>
	public readonly struct Maybe
	{
		/// <summary>
		/// Creates a new Maybe instance containing the specified value.
		/// </summary>
		/// <typeparam name="T">The type of the value to wrap in a Maybe.</typeparam>
		/// <param name="value">The value to wrap in a Maybe instance.</param>
		/// <returns>A new Maybe instance containing the specified value.</returns>
		public static Maybe<T> From<T>(T? value) => Maybe<T>.From(value);
	}
}