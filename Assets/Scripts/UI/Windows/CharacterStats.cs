using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public class CharacterStats : BaseWindow
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private Elements.HealthBar _health;
    }
}