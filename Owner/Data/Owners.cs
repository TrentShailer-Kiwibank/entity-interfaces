namespace Owner.Data;

using Owner.Types;

public static class Owners
{
    public static List<Owner> AllOwners = new();
    public static List<Pet> OwnedPets = new();

    public static void InitData()
    {
        var ownerA = new Owner()
        {
            Id = "0",
            Name = "Owner A",
            BirthDate = "05-29",
            Pets = []
        };
        var ownerB = new Owner()
        {
            Id = "1",
            Name = "Owner B",
            BirthDate = "08-03",
            Pets = []
        };
        var petA = new Pet()
        {
            Id = "0",
            Owners = [ownerA, ownerB]
        };
        ownerA.Pets = [petA];
        ownerB.Pets = [petA];

        OwnedPets = [petA];
        AllOwners = [ownerA, ownerB];
    }
}