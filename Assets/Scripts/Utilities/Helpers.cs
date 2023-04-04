using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Roguelike.Utilities
{
    public static class Helpers
    {
        #region UI
        private static PointerEventData s_eventDataCurrentPosition;
        private static List<RaycastResult> s_results;

        public static bool IsOverUI()
        {
            s_eventDataCurrentPosition = new PointerEventData(EventSystem.current) {position = Input.mousePosition};
            s_results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(s_eventDataCurrentPosition, s_results);

            return s_results.Count > 0;
        }
        #endregion

        #region Time
        private static readonly Dictionary<float, WaitForSeconds> s_waitDictionary =
            new Dictionary<float, WaitForSeconds>();

        public static WaitForSeconds GetTime(float timeInSeconds)
        {
            if (s_waitDictionary.TryGetValue(timeInSeconds, out WaitForSeconds wait))
                return wait;

            s_waitDictionary[timeInSeconds] = new WaitForSeconds(timeInSeconds);

            return s_waitDictionary[timeInSeconds];
        }
        #endregion Time
    }
}