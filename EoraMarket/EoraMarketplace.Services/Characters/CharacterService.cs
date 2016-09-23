using System;
using System.Collections.Generic;
using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.DataAccess.Repositories;
using System.Linq;
using System.Data.Entity;
using EoraMarketplace.Data.Domain.Images;
using EoraMarketplace.Data.Domain.Goods;
using System.Data.Entity.Core;

namespace EoraMarketplace.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        private readonly IRepository<Character> _repository;
        private readonly IRepository<Race> _raceRepository;
        private readonly IRepository<Class> _classRepository;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="repository"></param>
        public CharacterService(IRepository<Character> repository, IRepository<Race> raceRepository, IRepository<Class> classRepository)
        {
            this._repository = repository;
            this._raceRepository = raceRepository;
            this._classRepository = classRepository;
        }

        public Character CreateUserCharacter(Character character)
        {
            return _repository.Insert(character);
        }

        public Character GetUserCharacter(int userId, int charId)
        {
            return GetUserCharacter(userId, charId, false);
        }

        public Character GetUserCharacter(int userId, int charId, bool includeAll)
        {
            IQueryable<Character> characters = _repository.Table
                .Where(x => x.Id == charId);

            if(includeAll)
            {
                characters = characters
                    .Include(x => x.Avatar)
                    .Include(x => x.Class)
                    .Include(x => x.Race);
            }

            Character character = characters.FirstOrDefault();

            if(character == null || !character.OwnerId.Equals(userId))
                throw new Exception("Not found character ID for current user.");

            return character;
        }

        public List<Character> GetUserCharacters(int userId, int page, int take)
        {
            int skipElements = (page - 1) * take;

            List<Character> characters = _repository.Table
                .Where(x => x.OwnerId == userId)
                .OrderBy(x => x.CreatedAt)
                .Skip(skipElements)
                .Take(take)
                .Include(x => x.Avatar)
                .Include(x => x.Class)
                .Include(x => x.Race)
                .ToList();

            return characters;
        }

        public List<Class> GetCharactersClasses()
        {
            return _classRepository.Table.ToList();
        }

        public List<Race> GetCharactersRaces()
        {
            return _raceRepository.Table.ToList();
        }

        public List<Picture> GetAvatarsByRaceId(int id)
        {
            return _raceRepository.Table
                .Where(x => x.Id == id)
                .SelectMany(x => x.AvailableAvatars)
                .ToList();
        }

        public int GetCharacterFunds(int id)
        {
            Character character = _repository.GetById(id);
            return character != null ? character.Credits : 0;
        }

        public List<CharactersProducts> GetCharacterInventory(int userId, int characterId)
        {
            List<CharactersProducts> products = _repository.Table.Where(x => x.OwnerId == userId && x.Id == characterId)
                .SelectMany(x => x.Inventory)
                .Include(x => x.Product)
                .Include(x => x.Product.Image)
                .ToList();

            return products;
        }

        public Character UpdateCharacterName(int ownerId, int characterId, string newName)
        {
            Character updateChar = _repository.GetById(characterId);

            if(updateChar.OwnerId != ownerId)
                throw new ObjectNotFoundException("Character not found");

            updateChar.Name = newName;

            return _repository.Update(updateChar);
        }
    }
}
