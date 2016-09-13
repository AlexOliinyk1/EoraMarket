namespace EoraMarketplace.Data.Domain.Characters
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Credits { get; set; }

        public Race Race { get; set; }
        public Class Class { get; set; }

    }
}
