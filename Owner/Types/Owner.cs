using HotChocolate.ApolloFederation.Resolvers;
using HotChocolate.ApolloFederation.Types;
using Owner.Data;

namespace Owner.Types;

[ObjectType]
[Tag("Test")]
public class Owner
{
    [Key]
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string BirthDate { get; set; }
    public required List<Pet> Pets { get; set; }

    [ReferenceResolver]
    public static Owner? ResolveReference(string id)
    {

        if (id == "0")
        {
            return Owners.AllOwners[0];
        }
        else if (id == "1")
        {
            return Owners.AllOwners[1];
        }
        else
        {
            return null;
        }
    }
}