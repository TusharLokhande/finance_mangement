using Application.Features.Bank.Request;
using FluentValidation;

namespace Application.Features.Bank.Validation
{
    public class UpdateBankValidator : AbstractValidator<UpdateBankDto>
    {
        public UpdateBankValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Bank ID is required.");

            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage("Account number is required.")
                .NotNull().WithMessage("Account number cannot be null.")
                .MaximumLength(50).WithMessage("Account number cannot exceed 50 characters.");

            RuleFor(x => x.BankName)
                .NotEmpty().WithMessage("Bank name is required.")
                .NotNull().WithMessage("Bank name cannot be null.")
                .MaximumLength(100).WithMessage("Bank name cannot exceed 100 characters.");
        }
    }
}
