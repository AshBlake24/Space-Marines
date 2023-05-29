using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows.Regions
{
    public class RegionDangerViewer : MonoBehaviour
    {
        [SerializeField] private Color _dangerColor;
        [SerializeField] private Image[] _skullImages;

        public void Init(int difficulty)
        {
            for (int i = 0; i < difficulty; i++)
                _skullImages[i].color = _dangerColor;
        }
    }
}