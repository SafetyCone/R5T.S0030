using System;


namespace R5T.S0030.F004
{
    /// <summary>
    /// Represents the set of modifiers (public, static, partial) present on a .NET C# member (class, interface, method).
    /// Note includes are: class, delegate, event, or interface, as these define the type of the member.
    /// </summary>
    public class ModiferGrid
    {
        public bool Abstract { get; set; }
        public bool Async { get; set; }
        public bool Const { get; set; }
        public bool Extern { get; set; }
        public bool Internal { get; set; }
        public bool Override { get; set; }
        public bool Private { get; set; }
        public bool Protected { get; set; }
        public bool Public { get; set; }
        public bool ReadOnly { get; set; }
        public bool Sealed { get; set; }
        public bool Static { get; set; }
        public bool Unsafe { get; set; }
        public bool Virtual { get; set; }
        public bool Volatile { get; set; }
    }
}
