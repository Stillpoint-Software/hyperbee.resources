# Hyperbee.Resources

Provides a dependency injection pattern for embedded resources

## Usage

Implement IResourceLocator and set the implementation's namespace to your resource location.

Inject IResourceProvider<Implementation> to use.



**For example:**


*this gives the path to the resources*

 `public class MyResourceLocator : IResourceLocator
 {
     public string Namespace => typeof(MyResourceLocator).Namespace; 
 }`

 *register the provider*

* `services.AddTransient<IResourceProvider<MyResourceLocator>>();`
* `var locator = services.GetService<IResourceProvider<MyResourceLocator>>();`
* `var resource = ResourceHelper.GetResource( locator, "resourceName" ); `

# Status

| Branch     | Action                                                                                                                                                                                                                      |
|------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `develop`  | [![Build status](https://github.com/Stillpoint-Software/Hyperbee.Resources/actions/workflows/publish.yml/badge.svg?branch=develop)](https://github.com/Stillpoint-Software/Hyperbee.Resources/actions/workflows/publish.yml)  |
| `main`     | [![Build status](https://github.com/Stillpoint-Software/Hyperbee.Resources/actions/workflows/publish.yml/badge.svg)](https://github.com/Stillpoint-Software/Hyperbee.Resources/actions/workflows/publish.yml)                 |


[![Hyperbee.Resource](https://github.com/Stillpoint-Software/Hyperbee.Resources/blob/main/assets/hyperbee.jpg?raw=true)](https://github.com/Stillpoint-Software/Hyperbee.Resources)

# Help

See [Todo](https://github.com/Stillpoint-Software/Hyperbee.Resources/blob/main/docs/todo.md)
