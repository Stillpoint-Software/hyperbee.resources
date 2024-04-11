using Hyperbee.Resources.Tests.TestSupport.Resources;

namespace Hyperbee.Resources.Tests
{
    [TestClass]
    public class ResourcesTest
    {
        [TestMethod]
        public void Should_find_file()
        {
            // arrange
            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            //act
            var result = resourceProvider.GetResource( "helloworld.txt" );

            //assert
            Assert.IsNotNull( result );

        }
    }
}
