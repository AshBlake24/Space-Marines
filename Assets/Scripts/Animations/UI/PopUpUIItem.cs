using DG.Tweening;
using UnityEngine;

namespace Roguelike.Animations.UI
{
    public class PopUpUIItem : MonoBehaviour
    {
        public void Play()
        {
            transform.localScale = Vector2.zero;
            transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
        }
    }
}