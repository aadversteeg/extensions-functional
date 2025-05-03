namespace Ave.Extensions.Functional
{
    /// <summary>
    /// Represents a void/nothing value in functional programming.
    /// </summary>
    public readonly struct Unit
    {
        private static readonly Unit _value = new Unit();

        public static Unit Value => _value;

        public override string ToString() => "()";
    }
} 