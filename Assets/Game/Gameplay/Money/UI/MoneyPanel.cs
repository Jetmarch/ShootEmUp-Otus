using TMPro;
using UnityEngine;

namespace Game.Gameplay.Money.UI
{
    public class MoneyPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;

        public void SetMoneyText(string moneyText)
        {
            _moneyText.text = moneyText;
        }
    }
}
