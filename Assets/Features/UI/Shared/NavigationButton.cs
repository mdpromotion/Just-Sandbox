using UnityEngine;

namespace Feature.UI.Shared
{
    public enum NavigationButtonType { Open, Close }

    public abstract class NavigationButton : MonoBehaviour
    {
        public event System.Action<NavigationButtonType> OnClicked;
        protected NavigationButtonType NavigationButtonType;
        protected void InvokeClicked() => OnClicked?.Invoke(NavigationButtonType);
    }
}