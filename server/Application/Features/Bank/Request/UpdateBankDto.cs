namespace Application.Features.Bank.Request
{
    public record UpdateBankDto
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
    }
}
