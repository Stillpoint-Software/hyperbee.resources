namespace Hyperbee.Resources;

public static class ResourceHelper
{
    public static string GetResource<TLocator>( string name )
        where TLocator : IResourceLocator
    {
        if ( name == null )
        {
            throw new ArgumentNullException( nameof( name ) );
        }

        var locator = Activator.CreateInstance<TLocator>();
        return GetResource( locator, name );

    }

    public static string GetResource( IResourceLocator locator, string name )
    {
        var stream = GetResourceStream( locator, name );
        using var reader = new StreamReader( stream );
        return reader.ReadToEnd();
    }

    public static Stream GetResourceStream( IResourceLocator locator, string name )
    {
        if ( locator == null )
            throw new ArgumentNullException( nameof( locator ) );

        if ( name == null )
            throw new ArgumentNullException( nameof( name ) );

        var resourceName = GetResourceName( locator, name );

        var stream = locator.GetType().Assembly.GetManifestResourceStream( resourceName );

        return stream ?? throw new FileNotFoundException( $"Cannot find '{resourceName}'." );
    }

    public static string GetResourceName( IResourceLocator locator, string name )
    {
        if ( locator == null )
            throw new ArgumentNullException( nameof( locator ) );

        if ( name == null )
            throw new ArgumentNullException( nameof( name ) );

        var sanitizedName = SanitizeName( name );
        return $"{locator.Namespace}.{sanitizedName}";
    }


    public static string[] GetResourceNames( IResourceLocator locator )
    {
        return locator.GetType().Assembly.GetManifestResourceNames();
    }

    private static readonly char[] InvalidChars =
    [
        ' ',
        '\u00A0' /* non-breaking space */,
        ',',
        ';',
        '|',
        '~',
        '@',
        '#',
        '%',
        '^',
        '&',
        '*',
        '+',
        '-',
        '/',
        '\\',
        '<',
        '>',
        '?',
        '[',
        ']',
        '(',
        ')',
        '{',
        '}',
        '\"',
        '\'',
        '!',
        '`',
        '='
    ];

    private static string SanitizeName( ReadOnlySpan<char> name )
    {
        name = name.Trim( '/' ); // allow path like names

        if ( name.IsEmpty )
            return "_";

        var builder = new ValueStringBuilder( name.Length + 1 );

        if ( char.IsDigit( name[0] ) )
            builder.Append( '_' );

        foreach ( var c in name )
        {
            if ( c == '/' ) // path
                builder.Append( '.' );
            else
                builder.Append( InvalidChars.Contains( c ) ? '_' : c );
        }

        return builder.ToString();
    }
}
