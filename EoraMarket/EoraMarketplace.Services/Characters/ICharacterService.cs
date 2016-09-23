using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Data.Domain.Images;
using System.Collections.Generic;
using EoraMarketplace.Data.Domain.Goods;

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
        ///     Get character for user include related entities
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="charId"></param>
        /// <param name="includeAll"></param>
        /// <returns></returns>
        Character GetUserCharacter(int userId, int charId, bool includeAll);
        /// <summary>
        ///     Get characters for user by page.
        /// </summary>
        /// <param name="userId">Id of characters owner</param>
        /// <param name="page">Current page (start by 1)</param>
        /// <param name="take">Max count of characters by page</param>
        /// <returns></returns>
        List<Character> GetUserCharacters(int userId, int page, int take);

        /// <summary>
        ///     Get all characters races
        /// </summary>
        /// <returns></returns>
        List<Race> GetCharactersRaces();
        /// <summary>
        ///     Get all characters classes
        /// </summary>
        /// <returns></returns>
        List<Class> GetCharactersClasses();
        /// <summary>
        ///     Get avatar images available for Race
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Picture> GetAvatarsByRaceId(int id);
        /// <summary>
        ///     Get character own product
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="characterId"></param>
        /// <returns></returns>
        List<CharactersProducts> GetCharacterInventory(int userId, int characterId);
        /// <summary>
        ///     Get amount of character funds
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int GetCharacterFunds(int id);
        /// <summary>
        ///     Update name of character
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        Character UpdateCharacterName(int ownerId, int characterId, string newName);
    }
}
