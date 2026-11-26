# Hyperbee.Resources

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



# Build Requirements

* Building this repository requires the **.NET 11 SDK**. It cross-targets `net11.0`,
  `net10.0` and `netstandard2.1`, and the older targeting packs come from NuGet, so
  one SDK covers every target.
* Consuming the package requires .NET 8 or later. A `net8.0` or `net9.0` project
  resolves the `netstandard2.1` asset.

## Building the Project

* With the .NET 11 SDK installed, build the project using the standard `dotnet build` command.

## Running Tests

* Run tests using the `dotnet test` command as usual.


# Status

| Branch     | Action                                                                                                                                                                                                                      |
|------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `develop`  | [![Build status](https://github.com/Stillpoint-Software/hyperbee.resources/actions/workflows/publish.yml/badge.svg?branch=develop)](https://github.com/Stillpoint-Software/hyperbee.resources/actions/workflows/publish.yml)  |
| `main`     | [![Build status](https://github.com/Stillpoint-Software/hyperbee.resources/actions/workflows/publish.yml/badge.svg)](https://github.com/Stillpoint-Software/hyperbee.resources/actions/workflows/publish.yml)                 |


# Help

See [Todo](https://github.com/Stillpoint-Software/hyperbee.resources/blob/main/docs/todo.md)

[![Hyperbee.Resources](https://github.com/Stillpoint-Software/Hyperbee.Resources/blob/main/assets/hyperbee.svg?raw=true)](https://github.com/Stillpoint-Software/Hyperbee.Resources)
