using Roguelike.Localization.Service;

namespace Roguelike.Localization
{
    [System.Serializable]
    public struct LocalizedString
    {
        public LocalizedString(string key)
        {
            Key = key;
        }
        
        public string Key { get; }

        public string Value => LocalizationService.GetLocalisedValue(Key);

        public static implicit operator LocalizedString(string key)
        {
            return new LocalizedString(key);
        }
    }
}