using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Provides extension methods that apply a Result-returning function to every item of a sequence and
    /// collect the outcome into a single Result.
    /// </summary>
    public static class ResultTraverseAllExtensions
    {
        /// <summary>
        /// Applies a Result-returning function to every item of the sequence. Every item is visited, also after a
        /// failure, so the caller hears about all problems at once instead of only the first.
        /// </summary>
        /// <typeparam name="Tin">The type of the items in the source sequence.</typeparam>
        /// <typeparam name="Tout">The type of the value in each successful Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The items to map.</param>
        /// <param name="map">A function that maps one item to a Result. It is called exactly once for every item, in sequence order.</param>
        /// <param name="combine">A function that turns the errors into one error. It is called only when at least one item failed,
        /// with every error in sequence order (a list of one when a single item failed).</param>
        /// <returns>A successful Result with all values in sequence order when every item succeeded (an empty list for an empty sequence);
        /// otherwise a failure Result with the error returned by <paramref name="combine"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/>, <paramref name="map"/> or <paramref name="combine"/> is null.</exception>
        public static Result<IReadOnlyList<Tout>, E> TraverseAll<Tin, Tout, E>(this IEnumerable<Tin> source, Func<Tin, Result<Tout, E>> map, Func<IReadOnlyList<E>, E> combine)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }
            if (combine == null)
            {
                throw new ArgumentNullException(nameof(combine));
            }

            var values = new List<Tout>();
            var errors = new List<E>();
            foreach (var item in source)
            {
                Collect(map(item), values, errors);
            }
            return Complete(values, errors, combine);
        }

        /// <summary>
        /// Applies an asynchronous Result-returning function to every item of the sequence. Every item is visited, also after a
        /// failure, so the caller hears about all problems at once instead of only the first. The items are mapped one after
        /// the other: the function for an item is not started before the task for the previous item has completed.
        /// </summary>
        /// <typeparam name="Tin">The type of the items in the source sequence.</typeparam>
        /// <typeparam name="Tout">The type of the value in each successful Result.</typeparam>
        /// <typeparam name="E">The type of the error.</typeparam>
        /// <param name="source">The items to map.</param>
        /// <param name="awaitableMap">An asynchronous function that maps one item to a Result. It is called exactly once for every item, in sequence order.</param>
        /// <param name="combine">A function that turns the errors into one error. It is called only when at least one item failed,
        /// with every error in sequence order (a list of one when a single item failed).</param>
        /// <returns>A task that resolves to a successful Result with all values in sequence order when every item succeeded
        /// (an empty list for an empty sequence); otherwise to a failure Result with the error returned by <paramref name="combine"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/>, <paramref name="awaitableMap"/> or <paramref name="combine"/> is null.</exception>
        public static Task<Result<IReadOnlyList<Tout>, E>> TraverseAll<Tin, Tout, E>(this IEnumerable<Tin> source, Func<Tin, Task<Result<Tout, E>>> awaitableMap, Func<IReadOnlyList<E>, E> combine)
        {
            // Validate eagerly, so that a null argument throws at the call and not when the task is awaited.
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (awaitableMap == null)
            {
                throw new ArgumentNullException(nameof(awaitableMap));
            }
            if (combine == null)
            {
                throw new ArgumentNullException(nameof(combine));
            }

            return TraverseAllCore(source, awaitableMap, combine);
        }

        private static async Task<Result<IReadOnlyList<Tout>, E>> TraverseAllCore<Tin, Tout, E>(IEnumerable<Tin> source, Func<Tin, Task<Result<Tout, E>>> awaitableMap, Func<IReadOnlyList<E>, E> combine)
        {
            var values = new List<Tout>();
            var errors = new List<E>();
            foreach (var item in source)
            {
                Collect(await awaitableMap(item).ConfigureAwait(false), values, errors);
            }
            return Complete(values, errors, combine);
        }

        private static void Collect<Tout, E>(Result<Tout, E> result, List<Tout> values, List<E> errors)
        {
            if (result.IsSuccess)
            {
                values.Add(result.Value);
            }
            else
            {
                errors.Add(result.Error);
            }
        }

        private static Result<IReadOnlyList<Tout>, E> Complete<Tout, E>(List<Tout> values, List<E> errors, Func<IReadOnlyList<E>, E> combine)
        {
            if (errors.Count > 0)
            {
                return Result<IReadOnlyList<Tout>, E>.Failure(combine(errors.AsReadOnly()));
            }
            return Result<IReadOnlyList<Tout>, E>.Success(values.AsReadOnly());
        }
    }
}
