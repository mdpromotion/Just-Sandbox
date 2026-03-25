using System;
using UnityEngine;

public interface IEconomicEvents
{
    event Action<int> MoneyChanged;
}
