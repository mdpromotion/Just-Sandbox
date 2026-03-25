using UnityEngine;
using UnityEngine.UI;

namespace Feature.Combat.Presentation
{
    [RequireComponent(typeof(Text))]
    public class AmmoView : MonoBehaviour
    {
        private Text _ammoText;
        public void Awake()
        {
            _ammoText = GetComponent<Text>();
        }
        public void SetAmmoText(string text)
        {
            if (_ammoText != null)
            {
                _ammoText.text = text;
            }
        }
        public void SetActive(bool isActive)
        {
            _ammoText.gameObject.SetActive(isActive);
        }
    }
}