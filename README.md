![Nuget](https://img.shields.io/nuget/v/DomainValidation.NET)
![GitHub](https://img.shields.io/github/license/ipazooki/DomainValidation)
![GitHub contributors](https://img.shields.io/github/contributors/ipazooki/DomainValidation)
![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/ipazooki/DomainValidation/dotnet.yml)

# Domain validation with a result object

This library provides a simple, consistent approach to domain validation using a `Result` object. The `Result` class encapsulates both the outcome of an operation and any associated validation errors.

- When validation passes and the model is valid, `result.IsSuccess` is `true` and `result.Value` contains the valid model.
- When validation fails, `result.IsSuccess` is `false` and `result.Errors` contains one or more `Error` instances describing what went wrong.

## Example

Consider a `Blog` class with basic domain validation logic:

```csharp
public class Blog
{
    private Blog(string title)
    {
        Title = title;
    }

    public static Result<Blog> Create(string title)
    {
        var errors = new List<Error>();

        if (string.IsNullOrEmpty(title))
        {
            return Result<Blog>.Failure("Title is mandatory");
        }

        if (title.Length < 3)
        {
            errors.Add(new Error("The minimum title length is 3 characters"));
        }

        if (title.Length > 40)
        {
            errors.Add(new Error("The maximum title length is 40 characters"));
        }

        if (errors.Any())
        {
            return Result<Blog>.Failure(errors.ToArray());
        }

        return new Blog(title);
    }

    public string Title { get; init; }
}
```

Creating an instance of `Blog` with a valid title returns a successful `Result<Blog>` containing the new instance. If the title is invalid, the result indicates failure and contains the corresponding error details.

```csharp
Result<Blog> blog = Blog.Create("My awesome blog");

if (blog.IsSuccess)
{
    Console.WriteLine(blog.Value.Title);
}

blog = Blog.Create("");

if (!blog.IsSuccess)
{
    foreach (var error in blog.Errors)
    {
        Console.WriteLine(error);
    }
}

blog = Blog.Create("B");

if (!blog.IsSuccess)
{
    foreach (var error in blog.Errors)
    {
        Console.WriteLine(error);
    }
}
```

## Contribution

You are very welcome to contribute by opening issues, submitting pull requests, or simply giving the project a ⭐ on GitHub if you find it useful.

## Social media

[![Email](https://img.shields.io/badge/Email-gray?logo=gmail&style=flat-square)](mailto:ipazooki@live.com)
[![Stack Overflow](https://img.shields.io/badge/Stackoverflow-gray?logo=stackoverflow&style=flat-square)](https://stackoverflow.com/users/1424065/mrp)
[![Linkedin](https://img.shields.io/badge/-LinkedIn-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/pazooki)](https://www.linkedin.com/in/pazooki/)
![Twitter Follow](https://img.shields.io/twitter/follow/ipazooki)