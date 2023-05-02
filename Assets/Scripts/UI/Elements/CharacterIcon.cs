using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements
{
    public class CharacterIcon : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public void Construct(Sprite characterIcon) => 
            _icon.sprite = characterIcon;
    }
}