namespace Pet.Types;

[QueryType]
public static class Query
{
    public static Pet? GetPet(string id)
    {
        return Pet.ResolveReference(id);
    }
}
