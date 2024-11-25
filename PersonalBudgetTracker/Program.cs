using System;
using System.Collections.Generic;
using System.IO;

namespace PersonalBudgetTracker
{
    class Program
    {
        static List<Transaction> transactions = new List<Transaction>();
        const string filePath = "transactions.txt";

        static void Main(string[] args)
        {
            // Load transactions from the file
            LoadTransactions();

            // Start the main loop for user input
            while (true)
            {
                Console.Clear();
                Console.WriteLine("== Personal Budget Tracker ==");
                Console.WriteLine("1. Add Income");
                Console.WriteLine("2. Add Expense");
                Console.WriteLine("3. View Transactions");
                Console.WriteLine("4. View Balance");
                Console.WriteLine("5. Save and Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                // Handle user input based on the choice
                switch (choice)
                {
                    case "1":
                        AddTransaction(true);
                        break;
                    case "2":
                        AddTransaction(false);
                        break;
                    case "3":
                        ViewTransactions();
                        break;
                    case "4":
                        ViewBalance();
                        break;
                    case "5":
                        SaveTransactions();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }

            // Keep the console open after execution
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void AddTransaction(bool isIncome)
        {
            Console.Clear();
            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.Write("Enter amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount. Press Enter to return.");
                Console.ReadLine();
                return;
            }

            transactions.Add(new Transaction
            {
                Date = DateTime.Now,
                Description = description,
                Amount = amount,
                IsIncome = isIncome
            });

            Console.WriteLine($"{(isIncome ? "Income" : "Expense")} added! Press Enter to return.");
            Console.ReadLine();
        }

        static void ViewTransactions()
        {
            Console.Clear();
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
            }
            else
            {
                foreach (var transaction in transactions)
                {
                    Console.WriteLine(transaction);
                }
            }
            Console.WriteLine("Press Enter to return.");
            Console.ReadLine();
        }

        static void ViewBalance()
        {
            Console.Clear();
            decimal income = 0, expense = 0;

            foreach (var transaction in transactions)
            {
                if (transaction.IsIncome)
                    income += transaction.Amount;
                else
                    expense += transaction.Amount;
            }

            Console.WriteLine($"Total Income: ${income:F2}");
            Console.WriteLine($"Total Expense: ${expense:F2}");
            Console.WriteLine($"Balance: ${income - expense:F2}");
            Console.WriteLine("Press Enter to return.");
            Console.ReadLine();
        }

        static void SaveTransactions()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var transaction in transactions)
                {
                    writer.WriteLine($"{transaction.Date},{transaction.Description},{transaction.Amount},{transaction.IsIncome}");
                }
            }
            Console.WriteLine("Transactions saved! Press Enter to exit.");
            Console.ReadLine();
        }

        static void LoadTransactions()
        {
            if (!File.Exists(filePath)) return;

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    transactions.Add(new Transaction
                    {
                        Date = DateTime.Parse(parts[0]),
                        Description = parts[1],
                        Amount = decimal.Parse(parts[2]),
                        IsIncome = bool.Parse(parts[3])
                    });
                }
            }
        }
    }
}
