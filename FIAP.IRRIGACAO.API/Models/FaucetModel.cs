namespace FIAP.IRRIGACAO.API.Models
{
    public class FaucetModel
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string IsEnabled { get; set; }
        public long LocationId { get; set; }
        public required LocationModel Location { get; set; }
    }

}
