---
layout: default
title: Resource Locator
nav_order: 2
---

# Resource Locator

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
