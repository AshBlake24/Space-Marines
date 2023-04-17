using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public class PlayerIsArrived : Transition
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Player>(out Player player))
            {
                NeedTransit?.Invoke(targetState);
            }
        }
    }
}
