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

* To build and run this project, **.NET 9 SDK** is required.
* Ensure your development tools are compatible with .NET 8 or higher.

## Building the Project

* With .NET 9 SDK installed, you can build the project using the standard `dotnet build` command.

## Running Tests

* Run tests using the `dotnet test` command as usual.


# Status

| Branch     | Action                                                                                                                                                                                                                      |
|------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `develop`  | [![Build status](https://github.com/Stillpoint-Software/hyperbee.resources/actions/workflows/publish.yml/badge.svg?branch=develop)](https://github.com/Stillpoint-Software/hyperbee.resources/actions/workflows/pack_publish.yml)  |
| `main`     | [![Build status](https://github.com/Stillpoint-Software/hyperbee.resources/actions/workflows/publish.yml/badge.svg)](https://github.com/Stillpoint-Software/hyperbee.resources/actions/workflows/pack_publish.yml)                 |


# Benchmarks
 See [Benchmarks](https://github.com/Stillpoint-Software/Hyperbee.Resources/test/Hyperbee.Resources.Benchmark/benchmark/results/Hyperbee.Resources.Benchmark.ResourcesBenchmark-report-github.md)
 

# Help

See [Todo](https://github.com/Stillpoint-Software/hyperbee.resources/blob/main/docs/todo.md)

[![Hyperbee.Resources](https://github.com/Stillpoint-Software/Hyperbee.Resources/blob/main/assets/hyperbee.svg?raw=true)](https://github.com/Stillpoint-Software/Hyperbee.Resources)
