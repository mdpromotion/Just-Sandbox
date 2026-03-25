using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class EconomicView : MonoBehaviour, IEconomicView
{
    private Text moneyText;

    private void Awake()
    {
        moneyText = GetComponent<Text>();
    }

    public void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }

    public void UpdateMoneyText(string text)
    {
        moneyText.text = text;
    }
}
