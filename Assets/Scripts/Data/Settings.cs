using System;

namespace Roguelike.Data
{
    [Serializable]
    public class Settings
    {
        public AudioSettings AudioSettings;

        public Settings()
        {
            AudioSettings = new AudioSettings();
        }
    }
}