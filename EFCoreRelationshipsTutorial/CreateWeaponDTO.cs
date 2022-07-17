namespace EFCoreRelationshipsTutorial
{
    public class CreateWeaponDTO
    {
        public string Name { get; set; }=string.Empty;
        public int Damage { get; set; } = 10;
        public int CharacterId { get; set; }
    }
}
