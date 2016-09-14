using System;
using System.Collections.Generic;
using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.DataAccess.Repositories;
using System.Linq;

namespace EoraMarketplace.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        private readonly IRepository<Character> _repository;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="repository"></param>
        public CharacterService(IRepository<Character> repository)
        {
            this._repository = repository;
        }

        public Character CreateUserCharacter(Character character)
        {
            throw new NotImplementedException();
        }

        public Character GetUserCharacter(int userId, int charId)
        {
            Character character = _repository.GetById(charId);

            if(!character.OwnerId.Equals(userId))
                throw new Exception("Not found character ID for current user.");

            return character;
        }

        public ICollection<Character> GetUserCharacters(int userId, int page, int take)
        {
            ICollection<Character> characters = _repository.Table
                .Where(x => x.OwnerId == userId)
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();

            return characters;
        }
    }
}
