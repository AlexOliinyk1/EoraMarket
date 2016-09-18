using EoraMarketplace.Data.Domain.Goods;
using EoraMarketplace.Data.Domain.Images;
using EoraMarketplace.Data.Domain.Users;
using System;
using System.Collections.Generic;
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
        public int Credits { get; set; }

        public int OwnerId { get; set; }
        public int RaceId { get; set; }
        public int ClassId { get; set; }
        public int ImageId { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        [ForeignKey("RaceId")]
        public Race Race { get; set; }

        [ForeignKey("ClassId")]
        public Class Class { get; set; }

        [ForeignKey("ImageId")]
        public Picture Avatar { get; set; }

        public List<Product> Inventory { get; set; }
    }
}
