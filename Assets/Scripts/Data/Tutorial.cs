using System;
using Roguelike.Tutorials;

namespace Roguelike.Data
{
    [Serializable]
    public class Tutorial
    {
        public TutorialId Id;
        public bool CanShow;

        public Tutorial(TutorialId id)
        {
            Id = id;
            CanShow = true;
        }
    }
}