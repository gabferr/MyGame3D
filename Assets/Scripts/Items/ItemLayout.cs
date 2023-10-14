using Itens;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    public class ItemLayout : MonoBehaviour
    {
        public Image uiIcon;
        public TextMeshProUGUI uiValue;

        private ItemSetup _currentSetup;
        public void LoadSetup(ItemSetup setup)
        {
            _currentSetup = setup;
            UpdateUi();
        }

        private void UpdateUi()
        {
            uiIcon.sprite = _currentSetup.icon;
        }
        private void Update()
        {
            uiValue.text = _currentSetup.soInt.value.ToString();
        }
    }
}