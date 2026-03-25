using System;
using Zenject;

public class PlayerEconomy : IEconomicController, IEconomicEvents, IReadOnlyPlayerEconomy
{
    public int Money { get; private set; }

    public event Action<int> MoneyChanged;

    public void ModifyMoney(int amount)
    {
        Money += amount;
        if (Money < 0)
            Money = 0;

        NotifyMoneyChanged();
    }

    public Result SetMoney(int amount)
    {
        if (amount < 0)
            return Result.Failure("Money cannot be negative.");

        Money = amount;
        NotifyMoneyChanged();
        return Result.Success();
    }

    private void NotifyMoneyChanged()
    {
        MoneyChanged?.Invoke(Money);
    }


}

