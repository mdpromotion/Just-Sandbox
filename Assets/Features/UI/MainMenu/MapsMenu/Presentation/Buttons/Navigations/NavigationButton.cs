using UnityEngine;

namespace Feature.MapsMenu.Presentation
{
    public enum NavigationButtonType { OpenMaps, CloseMaps }

    public abstract class NavigationButton : MonoBehaviour
    {
        public event System.Action<NavigationButtonType> OnClicked;
        protected NavigationButtonType NavigationButtonType;
        protected void InvokeClicked() => OnClicked?.Invoke(NavigationButtonType);
    }
}