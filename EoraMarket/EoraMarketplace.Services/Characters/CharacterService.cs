using System;
using System.Collections.Generic;
using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.DataAccess.Repositories;
using System.Linq;
using System.Data.Entity;
using EoraMarketplace.Data.Domain.Images;

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
            Character character = _repository.GetById(charId);

            if(!character.OwnerId.Equals(userId))
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
    }
}
