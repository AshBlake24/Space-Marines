using UnityEngine;

namespace Roguelike.Tutorials
{
    public class TutorialNavigationPoint : MonoBehaviour
    {
        [field: SerializeField] public int RouteIndex { get; private set; }
    }
}