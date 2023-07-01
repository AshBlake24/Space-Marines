using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Animations
{
    public class UVScroller : MonoBehaviour
    {
        [SerializeField] private RawImage _image;
        [SerializeField] private float _x;
        [SerializeField] private float _y;

        private void Update()
        {
            _image.uvRect = new Rect(_image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _image.uvRect.size);
        }
    }
}