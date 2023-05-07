using Roguelike.StaticData.Characters;
using UnityEngine;

namespace Roguelike.Logic.CharacterSelection
{
    public class SelectableCharacter : MonoBehaviour
    {
        [SerializeField] private CharacterId _id;

        public CharacterId Id => _id;
    }
}