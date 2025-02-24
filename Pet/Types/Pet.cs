using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.ApolloFederation.Types;

namespace Pet.Types;

[InterfaceType]
// Required outside the body for HotChocolate to register the @key
[Key(fieldSet: "id")]
// Required outside the body to be inherited by concrete types.
[ReferenceResolver(EntityResolverType = typeof(Pet), EntityResolver = nameof(ResolveReference))]
public abstract class Pet
{
    public required string Id { get; set; }

    public string? Name { get; set; }
    public string? BirthDate { get; set; }

    public static Pet? ResolveReference(string id)
    {
        if (id != "0")
        {
            return null;
        }

        return new Dog()
        {
            Id = "0",
            BirthDate = "05-29"
        };
    }
}

