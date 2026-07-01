namespace Application.Features.Bank.Request
{
    public record CreateBankDto
    {
        public string AccountNumber { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
    }
}
