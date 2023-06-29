using System;
using System.Collections.Generic;
using Roguelike.Tutorials;

namespace Roguelike.Data
{
    [Serializable]
    public class TutorialData
    {
        public bool IsTutorialCompleted;
        public List<TutorialId> CompletedTutorials;

        public TutorialData()
        {
            IsTutorialCompleted = false;
            CompletedTutorials = new List<TutorialId>();
        }
    }
}