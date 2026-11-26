namespace Hyperbee.Resources.Tests.TestSupport.Resources;

public class TestResourceLocator : IResourceLocator
{
    // Type.Namespace is declared nullable. This type is in a namespace, so the value
    // is never null here, and IResourceLocator.Namespace is non-nullable.
    public string Namespace => typeof( TestResourceLocator ).Namespace!;
}
