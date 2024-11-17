namespace FIAP.IRRIGACAO.API.Models
{
    public class FaucetModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsEnabled { get; set; }
        public required LocationModel Location { get; set; }
    }

}
