using System;

namespace PersonalBudgetTracker
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; } // True for Income, False for Expense

        public override string ToString()
        {
            string type = IsIncome ? "Income" : "Expense";
            return $"{Date.ToShortDateString()} | {type} | {Description} | ${Amount:F2}";
        }
    }
}
