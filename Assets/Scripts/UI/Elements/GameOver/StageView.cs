using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements.GameOver
{
    [RequireComponent(typeof(Image))]
    public class StageView : MonoBehaviour
    {
        [field: SerializeField] public RectTransform RectTransform { get; private set; }
    }
}