using Owner.Data;

namespace Owner.Types;

[QueryType]
public static class Query
{
    public static Owner? GetOwner(string id)
    {
        return Owner.ResolveReference(id);
    }

    public static List<Pet> OwnedPets()
    {
        return Owners.OwnedPets;
    }
}
