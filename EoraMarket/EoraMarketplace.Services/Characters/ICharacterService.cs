using EoraMarketplace.Data.Domain.Characters;
using System.Collections.Generic;

namespace EoraMarketplace.Services.Characters
{
    public interface ICharacterService
    {
        Character CreateUserCharacter(Character character);
        Character GetUserCharacter(int userId, int charId);
        ICollection<Character> GetUserCharacters(int userId, int page, int take);
    }
}
