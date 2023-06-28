using System;

namespace Roguelike.Data
{
    [Serializable]
    public class TutorialData
    {
        public bool IsTutorialCompleted;

        public TutorialData()
        {
            IsTutorialCompleted = false;
        }
    }
}