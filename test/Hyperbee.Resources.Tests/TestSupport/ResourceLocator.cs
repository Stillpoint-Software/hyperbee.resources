namespace Hyperbee.Resources.Tests.TestSupport.Resources;

public class TestResourceLocator : IResourceLocator
{
    public string? Namespace => typeof( TestResourceLocator ).Namespace;
}
