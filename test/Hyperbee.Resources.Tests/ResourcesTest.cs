using Hyperbee.Resources.Tests.TestSupport.Resources;

namespace Hyperbee.Resources.Tests
{
    [TestClass]
    public class ResourcesTest
    {
        private const string LocatorNamespace = "Hyperbee.Resources.Tests.TestSupport.Resources";

        // Reading content

        [TestMethod]
        public void Should_find_file()
        {
            // arrange
            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            //act
            var result = resourceProvider.GetResource( "helloworld.txt" );

            //assert
            Assert.IsNotNull( result );
            Assert.AreEqual( "Hello", result );
        }

        [TestMethod]
        public void Should_read_through_the_static_helper()
        {
            var result = ResourceHelper.GetResource<TestResourceLocator>( "helloworld.txt" );

            Assert.AreEqual( "Hello", result );
        }

        [TestMethod]
        public void Should_return_a_readable_stream()
        {
            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            using var stream = resourceProvider.GetResourceStream( "helloworld.txt" );
            using var reader = new StreamReader( stream );

            Assert.IsTrue( stream.CanRead );
            Assert.AreEqual( "Hello", reader.ReadToEnd() );
        }

        [TestMethod]
        public void Should_enumerate_resource_names()
        {
            var names = ResourceHelper.GetResourceNames( new TestResourceLocator() );

            Assert.IsTrue( names.Contains( $"{LocatorNamespace}.helloworld.txt" ) );
        }

        // Name construction and sanitization. These run through ValueStringBuilder,
        // which is span-heavy, so they are the interesting cases on netstandard2.1.

        [TestMethod]
        public void Should_prefix_the_locator_namespace()
        {
            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            Assert.AreEqual( $"{LocatorNamespace}.helloworld.txt", resourceProvider.GetResourceName( "helloworld.txt" ) );
        }

        [TestMethod]
        [DataRow( "hello world.txt", "hello_world.txt" )]
        [DataRow( "a,b;c|d~e@f#g", "a_b_c_d_e_f_g" )]
        [DataRow( "a%b^c&d*e+f-g", "a_b_c_d_e_f_g" )]
        [DataRow( "a/b\\c<d>e?f", "a.b_c_d_e_f" )]
        [DataRow( "a[b]c(d)e{f}g", "a_b_c_d_e_f_g" )]
        [DataRow( "a\"b'c!d`e=f", "a_b_c_d_e_f" )]
        [DataRow( "a\u00a0b", "a_b" )] // non-breaking space
        public void Should_replace_invalid_characters( string name, string expected )
        {
            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            Assert.AreEqual( $"{LocatorNamespace}.{expected}", resourceProvider.GetResourceName( name ) );
        }

        [TestMethod]
        public void Should_convert_path_separators_to_dots()
        {
            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            Assert.AreEqual( $"{LocatorNamespace}.a.b.c.txt", resourceProvider.GetResourceName( "a/b/c.txt" ) );
        }

        [TestMethod]
        public void Should_trim_surrounding_path_separators()
        {
            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            Assert.AreEqual( $"{LocatorNamespace}.a.b", resourceProvider.GetResourceName( "/a/b/" ) );
        }

        [TestMethod]
        public void Should_underscore_prefix_a_leading_digit()
        {
            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            Assert.AreEqual( $"{LocatorNamespace}._1file.txt", resourceProvider.GetResourceName( "1file.txt" ) );
        }

        [TestMethod]
        [DataRow( "" )]
        [DataRow( "/" )]
        [DataRow( "///" )]
        public void Should_yield_an_underscore_for_an_empty_name( string name )
        {
            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            Assert.AreEqual( $"{LocatorNamespace}._", resourceProvider.GetResourceName( name ) );
        }

        [TestMethod]
        public void Should_sanitize_a_name_longer_than_the_initial_buffer()
        {
            // 600 chars, past ValueStringBuilder's initial rent, and leading with a
            // digit so the underscore prefix pushes the write to exactly capacity.
            var name = "1" + new string( ' ', 599 );
            var expected = "_1" + new string( '_', 599 );

            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            Assert.AreEqual( $"{LocatorNamespace}.{expected}", resourceProvider.GetResourceName( name ) );
        }

        // Failure paths

        [TestMethod]
        public void Should_throw_when_the_resource_is_missing()
        {
            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            var ex = Assert.ThrowsExactly<FileNotFoundException>( () => resourceProvider.GetResource( "nope.txt" ) );

            Assert.IsTrue( ex.Message.Contains( $"{LocatorNamespace}.nope.txt" ) );
        }

        [TestMethod]
        public void Should_throw_when_the_name_is_null()
        {
            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            var ex = Assert.ThrowsExactly<ArgumentNullException>( () => resourceProvider.GetResourceName( null! ) );

            Assert.AreEqual( "name", ex.ParamName );
        }

        [TestMethod]
        public void Should_throw_when_the_generic_helper_name_is_null()
        {
            var ex = Assert.ThrowsExactly<ArgumentNullException>( () => ResourceHelper.GetResource<TestResourceLocator>( null! ) );

            Assert.AreEqual( "name", ex.ParamName );
        }

        [TestMethod]
        public void Should_throw_when_the_locator_is_null()
        {
            var ex = Assert.ThrowsExactly<ArgumentNullException>( () => ResourceHelper.GetResourceName( null!, "helloworld.txt" ) );

            Assert.AreEqual( "locator", ex.ParamName );
        }

        // The init accessor. This is what requires IsExternalInit, which netstandard2.1
        // does not have in box; see src/Shared/NetstandardShims.cs.

        [TestMethod]
        public void Should_accept_a_locator_through_the_init_accessor()
        {
            var locator = new TestResourceLocator();

            var resourceProvider = new ResourceProvider<TestResourceLocator> { Locator = locator };

            Assert.AreSame( locator, resourceProvider.Locator );
            Assert.AreEqual( "Hello", resourceProvider.GetResource( "helloworld.txt" ) );
        }

        [TestMethod]
        public void Should_default_the_locator_when_none_is_supplied()
        {
            var resourceProvider = new ResourceProvider<TestResourceLocator>();

            Assert.IsInstanceOfType<TestResourceLocator>( resourceProvider.Locator );
            Assert.AreEqual( LocatorNamespace, resourceProvider.Locator.Namespace );
        }
    }
}
