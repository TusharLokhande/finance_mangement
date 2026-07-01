namespace Application.Features.Bank.Response
{
    public record BankListDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
