namespace EoraMarketpalce.Web.Models.Characters
{
    public class CreateCharacterVM
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Credits { get; set; }

        public CreateCharacterVM()
        {
            this.Credits = 1000;
        }
    }
}