using UnityEngine;

namespace Roguelike.Localization
{
    [System.Serializable]
    public struct LocalizedString
    {
        [SerializeField] private string _key;

        public LocalizedString(string key)
        {
            _key = key;
        }

        public string Value => LocalizationSystem.GetLocalizedValue(_key);

        public static implicit operator LocalizedString(string key)
        {
            return new LocalizedString(key);
        }
    }
}