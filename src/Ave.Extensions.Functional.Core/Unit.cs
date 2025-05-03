namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Represents a void/nothing value in functional programming.
    /// </summary>
    public readonly struct Unit
    {
        private static readonly Unit _value = new Unit();

        /// <summary>
        /// Gets the singleton instance of the Unit struct.
        /// </summary>
        public static Unit Value => _value;

        /// <summary>
        /// Returns a string representation of the Unit.
        /// </summary>
        /// <returns>A string representation of the Unit, which is "()".</returns>
        public override string ToString() => "()";
    }
} 