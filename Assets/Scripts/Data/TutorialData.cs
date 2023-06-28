using System;

namespace Roguelike.Data
{
    [Serializable]
    public class TutorialData
    {
        public bool TutorialCompleted;

        public TutorialData()
        {
            TutorialCompleted = false;
        }
    }
}