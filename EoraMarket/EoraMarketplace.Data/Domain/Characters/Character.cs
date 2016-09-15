using EoraMarketplace.Data.Domain.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EoraMarketplace.Data.Domain.Characters
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Credits { get; set; }

        public int RaceId { get; set; }
        public int ClassId { get; set; }
        public int OwnerId { get; set; }

        [ForeignKey("RaceId")]
        public Race Race { get; set; }
        [ForeignKey("ClassId")]
        public Class Class { get; set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }
    }
}
