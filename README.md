# Functional C#
A library of static classes, extension methods, and classes that 
apply functional techniques to C#.

I am by no means an expert in this area. This repository is acting 
more as my personal notes as I study this concept and try applying
it to my other C# projects.  

## Resources
* [Functional Programming with C#](https://app.pluralsight.com/library/courses/functional-programming-csharp)
* [Applying Functional Principles in C#](https://app.pluralsight.com/library/courses/csharp-applying-functional-principles)
* [Functional Programming in C#](https://www.codeproject.com/Articles/375166/Functional-programming-in-Csharp)

## Other Techniques to Keep in Mind
When using this library to write your own library or application,
there are some items you will have to keep in mind in order if you
want to stay true to functional C#.

### Singular Behavior Functions
Keeping the methods/functions to just a singular action (especially 
in class libraries) gives you greater flexibilty in composing 
functions to reach the ultimate result of your application. This can
also make your library or application much more readable when a 
function only does one thing, and the name of that function 
describes what it does.
```
var substring = mystring.Substring(3,5);
```
gives the same result as 
```
var substring = mystring.Skip(3).Take(5);
```
but if you did not already know the behavior of `Substring` the 
composition of `Skip` and `Take` more clearly describes what the
result ultimately is. The separate `Skip` and `Take` functions
then also allows you to compose either or both with other functions
to obtain more complex behavior.

### Immutable Types
When creating a new class it is up to you to make it immutable to
enforce the idea that your library or application does not use
changing state. To do this you would use readonly getter auto
properties and then initialize those properties in the class 
constructor.
```
public class MyClass
{
    public readonly string MyProperty;

    public MyClass(string property)
    {
        MyProperty = property;
    }
}
```
Sometimes is does become nessessary to change the property of a 
class and thus change state that could effect the output of a 
function. The compromise for this situation could be to use private
setters with a public function that returns the changed object.
```
public class MyClass
{
    public string MyProperty { get; private set; }

    public MyClass SetProperty(string property)
    {
        MyProperty = property;
        return this;
    }
}
```

### Monads (the `Result` classes)
* [Monads in plain English for OO programmers](https://stackoverflow.com/questions/2704652/monad-in-plain-english-for-the-oop-programmer-with-no-fp-background)
> Formally, a monad is constructed by defining two operations (bind 
> and return) and a type constructor M that must fulfill several 
> properties to allow the correct composition of monadic functions 
> (i.e. functions that use values from the monad as their 
> arguments). The return operation takes a value from a plain type 
> and puts it into a monadic container of type M. The bind operation
> performs the reverse process, extracting the original value from 
> the container and passing it to the associated next function in 
> the pipeline.

#### Bind
The bind method on our `Result<T>` class is what makes the 
composition of functions on this amplified type work.

### Exception Handling
* [Exceptions for flow control](https://enterprisecraftsmanship.com/2015/02/26/exceptions-for-flow-control-in-c/)

Using exceptions as a sort of flow control for your library or 
application violates the idea of functional programming that any
function should only return a single type. Using exceptions in flow
control means that a function could return the type it is supposed
to return or it could return an exception type. To assist with this,
the Result class in this library can be used. Using that Result
class, exceptions should be caught and handled at the abosulte 
lowest level possible (as in a library) or at the highest level (as 
in an application). This can be tricky to do sometimes and will
probably cause more exceptions to buble up than you are used to
seeing. This might cause some distress at first, but seeing these 
exceptions will allow you to write in a handle for those exceptions
and create a more stable library or application in the long run.