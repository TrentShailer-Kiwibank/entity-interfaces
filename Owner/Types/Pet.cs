using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.ApolloFederation.Types;

namespace Owner.Types;

[InterfaceObject]
public class Pet
{
    [Key]
    public required string Id { get; set; }
    public required List<Owner> Owners { get; set; }

    [ReferenceResolver]
    public static Pet? ResolveReference(string id)
    {
        if (id == "0")
        {
            return new()
            {
                Id = "0",
                Owners = [
                    Data.Owners.AllOwners[0],
                    Data.Owners.AllOwners[1],
                ]
            };
        }
        else
        {
            return null;
        }
    }
}