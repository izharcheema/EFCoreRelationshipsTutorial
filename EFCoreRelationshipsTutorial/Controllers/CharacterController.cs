using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreRelationshipsTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _context;

        public CharacterController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Character>>> GetCharacters(int UserId)
        {
            var characters = await _context.Characters.Where(c => c.UserId == UserId).Include(c=>c.Weapon).Include(c=>c.Skills).ToListAsync();
            return characters;
        }
        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacters(CreateCharacterDTO request)
        {
            var user=await _context.Users.FindAsync(request.UserId);
            if(user == null)
                return NotFound();
            var newCharacter = new Character
            {
                Name = request.Name,
                RpgClass = request.RpgClass,
                User = user,
            };
            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();
            return await GetCharacters(newCharacter.UserId);
        }
        [HttpPost("weapon")]
        public async Task<ActionResult<Character>> CreateWeapon(CreateWeaponDTO request)
        {
            var Character = await _context.Characters.FindAsync(request.CharacterId);
            if (Character == null)
                return NotFound();
            var newWeapon = new Weapon
            {
                Name = request.Name,
                Damage = request.Damage,
                Character = Character,
            };
            _context.Weapons.Add(newWeapon);
            await _context.SaveChangesAsync();
            return Character;
        }
        [HttpPost("skill")]
        public async Task<ActionResult<Character>> CreateCharacterSkill(AddCharacterSkillDTO request)
        {
            var Character = await _context.Characters.Where(c=>c.Id==request.CharacterId).Include(c=>c.Skills).FirstOrDefaultAsync();
            if (Character == null)
                return NotFound();
            var Skill = await _context.Skills.FindAsync(request.SkillId);
            if (Skill == null)
                return NotFound();
            Character.Skills.Add(Skill);
            await _context.SaveChangesAsync();
            return Character;
        }
    }
}
