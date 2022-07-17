namespace EFCoreRelationshipsTutorial
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Damage { get; set; }
        [JsonIgnore]
        public List<Character> Characters { get; set; }
    }
}
