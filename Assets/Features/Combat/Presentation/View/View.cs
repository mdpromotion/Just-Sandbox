using System;
using UnityEngine;
using Zenject;

namespace Feature.Combat.Presentation
{
    public class View : IView
    {
        private readonly AmmoView _ammoView;

        public View(AmmoView ammoView)
        {
            _ammoView = ammoView;
        }

        public void SetAmmoText(string text)
        {
            _ammoView.SetAmmoText(text);
        }

        public void ToggleAmmoText(bool enabled)
        {
            _ammoView.SetActive(enabled);
        }
    }
}