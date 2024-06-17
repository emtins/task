using System;
public abstract class Account
{
    public decimal Balance { get; set; }
    public abstract void Deposit(decimal amount);
    public abstract void Withdraw(decimal amount);
    public abstract void DisplayBalance();
}
public class BankAccount : Account
{
    public override void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"Deposited {amount}$ to bank account");
    }
    public override void Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrew {amount}$ from bank account");
        }
        else
        {
            Console.WriteLine("Insufficient funds in bank account");
        }
    }
    public override void DisplayBalance()
    {
        Console.WriteLine($"Bank account balance: {Balance}$");
    }
}

public class CryptoWallet : Account
{
    public override void Deposit(decimal amount)
    {
        Balance += amount;
        Console.WriteLine($"Deposited {amount} BTC to crypto wallet");
    }

    public override void Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrew {amount} BTC from crypto wallet");
        }
        else
        {
            Console.WriteLine("Insufficient funds in crypto wallet");
        }
    }

    public override void DisplayBalance()
    {
        Console.WriteLine($"Crypto wallet balance: {Balance} BTC");
    }
}

public class CreditCardAccount : Account
{
    private decimal creditLimit = 5000;

    public override void Deposit(decimal amount)
    {
        Balance -= amount;
        Console.WriteLine($"Paid {amount}$ to credit card account");
    }

    public override void Withdraw(decimal amount)
    {
        if (Balance + amount <= creditLimit)
        {
            Balance += amount;
            Console.WriteLine($"Charged {amount}$ to credit card account");
        }
        else
        {
            Console.WriteLine("Credit limit exceeded");
        }
    }

    public override void DisplayBalance()
    {
        Console.WriteLine($"Credit card account balance: {Balance}$, Credit limit: {creditLimit}$, Available credit: {creditLimit - Balance}$");
    }
}

public class Transaction
{
    public decimal Amount { get; set; }
    public string Type { get; set; }

    public Transaction(decimal amount, string type)
    {
        Amount = amount;
        Type = type;
    }
}

public class AccountManager
{
    private Account[] accounts;
    private int count;

    public AccountManager(int size)
    {
        accounts = new Account[size];
        count = 0;
    }
    public void AddAccount(Account account)
    {
        if (count < accounts.Length)
        {
            accounts[count] = account;
            count++;
            Console.WriteLine("Account added");
        }
        else
        {
            Console.WriteLine("Account list is full");
        }
    }
    public void ExecuteTransaction(Account account, Transaction transaction)
    {
        if (transaction.Type == "Deposit")
        {
            account.Deposit(transaction.Amount);
        }
        else if (transaction.Type == "Withdraw")
        {
            account.Withdraw(transaction.Amount);
        }
        else
        {
            Console.WriteLine("Invalid transaction type");
        }
    }

    public void DisplayAllBalances()
    {
        for (int i = 0; i < count; i++)
        {
            accounts[i].DisplayBalance();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BankAccount bankAccount = new BankAccount();
        CryptoWallet cryptoWallet = new CryptoWallet();
        CreditCardAccount creditCardAccount = new CreditCardAccount();

        AccountManager accountManager = new AccountManager(5);

        accountManager.AddAccount(bankAccount);
        accountManager.AddAccount(cryptoWallet);
        accountManager.AddAccount(creditCardAccount);

        Transaction dep1 = new Transaction(1000, "Deposit");
        Transaction w1 = new Transaction(200, "Withdraw");

        Transaction dep2 = new Transaction(5, "Deposit");
        Transaction w2 = new Transaction(1, "Withdraw");

        Transaction dep3 = new Transaction(500, "Deposit");
        Transaction w3 = new Transaction(300, "Withdraw");

        accountManager.ExecuteTransaction(bankAccount, dep1);
        accountManager.ExecuteTransaction(bankAccount, w1);

        accountManager.ExecuteTransaction(cryptoWallet, dep2);
        accountManager.ExecuteTransaction(cryptoWallet, w2);

        accountManager.ExecuteTransaction(creditCardAccount, dep3);
        accountManager.ExecuteTransaction(creditCardAccount, w3);

        accountManager.DisplayAllBalances();
    }
}
