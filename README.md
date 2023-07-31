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
        if (string.IsNullOrEmpty(title))
        {
            return Result.Failure<Blog>(new Error("Error.Title", "Title is mandatory"));
        }

        return new Blog(title);
    }

    public string Title { get; init; }
}

```

If we create an instance of `Blog` class with a valid title, then returned resule is successful with a new instance of `Blog`, but if it's not valid, then we will receive proper error message with detailed information about error.

```csharp

var blog = Blog.Create("B1"); 

if (blog.IsSuccess)
{
    Console.WriteLine(b.Value.Title);
}

blog = Blog.Create("");

if (!blog.IsSuccess)
{
    Console.WriteLine(b.Error);
}

```

## Project files explanations:

### Error.cs file

This file contains a `public record` named `Error` with the following properties:

- `Code`: A string representing the error code.
- `Message`: A string representing the error message.
- `LineNumber`: An optional integer representing the line number where the error occurred. This property is set using the `[CallerLineNumber]` attribute.
- `MemberName`: An optional string representing the name of the member where the error occurred. This property is set using the `[CallerMemberName]` attribute.
- `FilePath`: An optional string representing the path of the file where the error occurred. This property is set using the `[CallerFilePath]` attribute.

This record also contains a static field named `None` which represents an empty error.

### Result.cs file

This file contains a `public class` named `Result` with the following properties:

- `IsSuccess`: A boolean indicating whether the operation was successful or not.
- `Error`: An instance of the `Error` class representing any errors that occurred during the operation.

This class also contains several static methods for creating instances of this class:

- `Success()`: Creates a new instance of this class with `IsSuccess` set to true and `Error` set to `Error.None`.
- `Success<TValue>(TValue value)`: Creates a new instance of this class with `IsSuccess` set to true, `Error` set to `Error.None`, and a value of type `TValue`.
- `Failure(Error error)`: Creates a new instance of this class with `IsSuccess` set to false and an instance of the `Error` class representing any errors that occurred during the operation.
- `Failure<TValue>(Error error, TValue? value = default)`: Creates a new instance of this class with `IsSuccess` set to false, an instance of the `Error` class representing any errors that occurred during the operation, and a value of type `TValue`.

### ResultGeneric.cs file

This file contains a generic version of the `Result` class named `Result<TValue>`. This class inherits from the non-generic version of the class.

This class has an additional property named `Value`, which represents the result of a successful operation. If the operation was not successful, an exception is thrown.

This class also contains an implicit conversion operator from type TValue to type Result<TValue>.

#### P.S

This project has been inspired based on [Domain Validation With .NET | Clean Architecture, DDD, .NET 6](https://youtu.be/KgfzM0QWHrQ)
