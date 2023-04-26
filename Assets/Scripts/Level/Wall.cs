using UnityEngine;

namespace Roguelike.Level
{
    public class Wall : MonoBehaviour
    {
        public void Hide()
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        public void Show()
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
