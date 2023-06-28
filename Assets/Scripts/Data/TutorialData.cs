using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Tutorials;

namespace Roguelike.Data
{
    [Serializable]
    public class TutorialData
    {
        public List<Tutorial> Tutorials;

        public TutorialData()
        {
            Tutorials = new List<Tutorial>();
        }

        public Tutorial GetTutorial(TutorialId id) => 
            Tutorials.SingleOrDefault(data => data.Id == id);

        public void SetTutorial(Tutorial tutorial)
        {
            if (tutorial == null)
                throw new ArgumentNullException(nameof(tutorial));

            Tutorial existedTutorial = GetTutorial(tutorial.Id);

            if (existedTutorial == null)
                Tutorials.Add(tutorial);
            else
                existedTutorial.CanShow = tutorial.CanShow;
        }
    }
}