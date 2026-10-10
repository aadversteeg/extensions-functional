using System;

namespace Ave.Extensions.Functional
{
	/// <summary>
	/// Represents an optional value that may or may not exist.
	/// This type is particularly useful for representing nullable values of both reference and value types,
	/// and for functional programming patterns.
	/// </summary>
	/// <remarks>
	/// Null and None are the same thing: no factory or conversion on this type produces a Maybe that
	/// reports <see cref="HasValue"/> while holding a null value. See <see cref="From"/>.
	/// </remarks>
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
		/// <param name="value">The value to convert. May be null.</param>
		/// <remarks>
		/// This operator enables seamless conversion from values to Maybe instances,
		/// making it more convenient to work with Maybe types in your code.
		/// A null value converts to <see cref="None"/>, consistent with <see cref="From"/>.
		/// </remarks>
		public static implicit operator Maybe<T>(T? value)
		{
			if (value is Maybe<T> valueAsMaybe)
			{
				return valueAsMaybe;
			}

			return From(value);
		}

		/// <summary>
		/// Creates a Maybe instance for the specified value.
		/// </summary>
		/// <param name="source">The value to wrap in a Maybe instance. May be null.</param>
		/// <returns>
		/// <see cref="None"/> if <paramref name="source"/> is null; otherwise a Maybe instance
		/// containing the specified value.
		/// </returns>
		/// <remarks>
		/// A null source yields None rather than a Maybe that has a null value. This is what
		/// guarantees the invariant that <see cref="HasValue"/> implies a non-null
		/// <see cref="Value"/>. For a Nullable&lt;T&gt; source, a Nullable without a value is
		/// null once boxed and therefore also yields None.
		/// Note that only null yields None: default values such as 0, false and the empty
		/// string are values and yield a Maybe that has a value.
		/// </remarks>
		public static Maybe<T> From(T? source)
		{
			if (source is null)
			{
				return None;
			}

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
		/// Creates a Maybe instance for the specified value.
		/// </summary>
		/// <typeparam name="T">The type of the value to wrap in a Maybe.</typeparam>
		/// <param name="value">The value to wrap in a Maybe instance. May be null.</param>
		/// <returns>
		/// <see cref="Maybe{T}.None"/> if <paramref name="value"/> is null; otherwise a Maybe
		/// instance containing the specified value.
		/// </returns>
		public static Maybe<T> From<T>(T? value) => Maybe<T>.From(value);
	}
}