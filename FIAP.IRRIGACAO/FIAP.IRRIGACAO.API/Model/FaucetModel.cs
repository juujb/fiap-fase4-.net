namespace FIAP.IRRIGACAO.API.Model
{
    public class FaucetModel : BaseEntity
    {
        public required string IsEnabled { get; set; }
        public required long LocationId { get; set; }
        public LocationModel? Location { get; set; }

        public FaucetModel() { }
    }
}
