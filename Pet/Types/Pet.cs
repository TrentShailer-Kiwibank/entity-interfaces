using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.ApolloFederation.Types;

namespace Pet.Types;

[InterfaceType]
// TODO this shouldn't be required
[Key(fieldSet: "id")]
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

