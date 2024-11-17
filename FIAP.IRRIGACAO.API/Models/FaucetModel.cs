namespace FIAP.IRRIGACAO.API.Models
{
    public class FaucetModel
    {
        public long Id { get; set; }
        public bool IsEnabled { get; set; }
        public long LocationId { get; set; }
    }

}
