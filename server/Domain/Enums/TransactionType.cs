using System.ComponentModel;

namespace Domain.Enums
{
    public enum TransactionType
    {
        [Description("Income")]
        INCOME = 1,

        [Description("Expense")]
        EXPENSE = 2,

        [Description("Self Transfer")]
        SELF_TRANSFER = 3,

        [Description("Credit Card Payment")]
        Credit_CARD_PAYMENT = 4,

        [Description("Lent Out")]
        LENT_OUT = 5
    }
}
