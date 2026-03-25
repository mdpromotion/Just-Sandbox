using UnityEngine;

public interface IEconomicController
{
    void ModifyMoney(int amount);
    Result SetMoney(int amount);
}
