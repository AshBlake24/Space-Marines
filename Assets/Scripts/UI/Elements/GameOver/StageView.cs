using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements.GameOver
{
    [RequireComponent(typeof(Image))]
    public class StageView : MonoBehaviour
    {
        // [field: SerializeField] public float Width { get; private set; }
        // [field: SerializeField] public float Height { get; private set; }

        [field: SerializeField] public RectTransform RectTransform { get; private set; }
    }
}