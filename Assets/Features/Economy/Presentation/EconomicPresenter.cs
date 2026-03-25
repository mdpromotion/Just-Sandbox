using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EconomicPresenter : IInitializable, IDisposable
{
    private readonly IEconomicEvents _economicEvents;
    private readonly IEconomicView _view;

    public EconomicPresenter(
        IEconomicEvents economicEvents,
        IEconomicView view)
    {
        _economicEvents = economicEvents;
        _view = view;
    }
    public void Initialize()
    {
        _economicEvents.MoneyChanged += UpdateMoneyUI;
    }
    public void Show()
    {
        _view.SetVisible(true);
    }
    public void Hide()
    {
        _view.SetVisible(false);
    }
    private void UpdateMoneyUI(int newAmount)
    {
        _view.UpdateMoneyText(newAmount + "$");
    }
    public void Dispose()
    {
        _economicEvents.MoneyChanged -= UpdateMoneyUI;
    }
}
