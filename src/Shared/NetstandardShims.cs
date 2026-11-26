#if NETSTANDARD

// Everything netstandard2.1 needs from the compiler that newer targets have in box.
// Hand-written rather than taken from the Polyfill package: Polyfill compiles all of
// its internal polyfills into the consuming assembly and the compiler does not strip
// unused internal types, which measured in the order of 320,000 bytes across the
// Hyperbee repos against a few kilobytes for shims like these. Only one thing is
// actually needed here, and it is small and stable.

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Marker the compiler requires to emit an <c>init</c> accessor.
    /// </summary>
    /// <remarks>
    /// Used by <c>ResourceProvider&lt;TLocator&gt;.Locator</c>. The compiler only needs
    /// this type to exist by name, so an empty internal class is the whole contract.
    /// Added in .NET 5.
    /// </remarks>
    internal static class IsExternalInit;
}

#endif
