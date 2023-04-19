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

        #region Pool

        private static readonly Transform s_generalPoolsContainer = new GameObject($"Pools").transform;
        private static readonly Dictionary<string, Transform> s_pools = new();

        public static Transform GetGeneralPoolsContainer() => 
            s_generalPoolsContainer;

        public static Transform GetPoolsContainer(string name)
        {
            if (s_pools.ContainsKey(name) == false)
                SetPoolsContainer(name);

            return s_pools[name];
        }

        private static void SetPoolsContainer(string name)
        {
            if (s_pools.ContainsKey(name) == false)
            {
                s_pools.Add(name, new GameObject($"Pool - {name}").transform);
                SetGeneralsPoolContainer(s_pools[name]);
            }
        }

        private static void SetGeneralsPoolContainer(Transform pool) =>
            pool.SetParent(s_generalPoolsContainer);

        #endregion
    }
}