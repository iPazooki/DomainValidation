![Nuget](https://img.shields.io/nuget/v/DomainValidation.NET)
![GitHub](https://img.shields.io/github/license/ipazooki/DomainValidation)
![GitHub contributors](https://img.shields.io/github/contributors/ipazooki/DomainValidation)
![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/ipazooki/DomainValidation/dotnet.yml)

# Domain validation with returning result object

This project is an approach to domain validation with a result object. The `Result` class is used to handle errors and results in your application in a consistent and reliable way. If there is no validation issue and if the model is valid, then `result.Value` will return the value which is valid model and the `result.IsSuccess` is true. However, if something is wrong with domain validation, then `result.IsSuccess` is false and `result.Error` will hold error code and description. 

### Example:

Here we have a `Blog` class with a domain validation logic:

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
            return Result.Failure<Blog>(new Error("Error.Title.Empty", "Title is mandatory"));
        }

        if (title.Length < 3)
        {
            errors.Add(new Error("Error.Title.MinLength", "The minimum title length is 3 character"));
        }

        if (title.Length > 40)
        {
            errors.Add(new Error("Error.Title.MaxLength", "The maximum title length is 40 character"));
        }

        if (errors.Any())
        {
            return Result.Failure<Blog>(errors.ToArray());
        }

        return new Blog(title);
    }

    public string Title { get; init; }
}

```

If we create an instance of `Blog` class with a valid title, then returned resule is successful with a new instance of `Blog`, but if it's not valid, then we will receive proper error message with detailed information about error.

```csharp

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

### Contribution
👍 You are encouraged to contribute to this project by forking it or giving it a star if you find it valuable :)

### Social Media

[![Email](https://img.shields.io/badge/Email-gray?logo=gmail&style=flat-square)](mailto:ipazooki@gmail.com)
[![Stack Overflow](https://img.shields.io/badge/Stackoverflow-gray?logo=stackoverflow&style=flat-square)](https://stackoverflow.com/users/1424065/mrp)
[![Linkedin](https://img.shields.io/badge/-LinkedIn-blue?style=flat-square&logo=Linkedin&logoColor=white&link=https://www.linkedin.com/in/pazooki)](https://www.linkedin.com/in/pazooki/)
![Twitter Follow](https://img.shields.io/twitter/follow/ipazooki)