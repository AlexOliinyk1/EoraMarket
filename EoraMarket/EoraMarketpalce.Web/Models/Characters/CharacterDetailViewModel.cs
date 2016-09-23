using EoraMarketplace.Data.Domain.Goods;
using System.Collections.Generic;

namespace EoraMarketpalce.Web.Models.Characters
{
    public class CharacterDetailViewModel
    {
        public CharacterInfoViewModel Character { get; set; }
        public List<Product> Inventory { get; set; }
    }
}