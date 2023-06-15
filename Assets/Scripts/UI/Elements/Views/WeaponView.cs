using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements.Views
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private Image _weaponIcon;
        [SerializeField] private Image _rarityShadow;

        public void Construct(Sprite weaponIcon, Color rarityColor)
        {
            _weaponIcon.sprite = weaponIcon;
            _rarityShadow.color = rarityColor;
        }
    }
}