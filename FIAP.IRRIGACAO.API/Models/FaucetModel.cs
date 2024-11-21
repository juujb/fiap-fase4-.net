namespace FIAP.IRRIGACAO.API.Models
{
    public class FaucetModel
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string IsEnabled { get; set; }
        public required long LocationId { get; set; }
        public LocationModel? Location { get; set; }

        public FaucetModel() { }
    }

}
