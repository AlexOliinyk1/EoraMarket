using EoraMarketplace.Data.Domain.Characters;
using System.Collections.Generic;

namespace EoraMarketplace.Services.Characters
{
    public interface ICharacterService
    {
        /// <summary>
        ///     Create new character for user
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        Character CreateUserCharacter(Character character);
        /// <summary>
        ///     Get character for user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="charId"></param>
        /// <returns></returns>
        Character GetUserCharacter(int userId, int charId);
        /// <summary>
        ///     Get characters for user by page.
        /// </summary>
        /// <param name="userId">Id of characters owner</param>
        /// <param name="page">Current page (start by 1)</param>
        /// <param name="take">Max count of characters by page</param>
        /// <returns></returns>
        List<Character> GetUserCharacters(int userId, int page, int take);

        List<Race> GetCharactersRaces();
        List<Class> GetCharactersClasses();
    }
}
