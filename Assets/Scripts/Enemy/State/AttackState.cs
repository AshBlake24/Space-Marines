using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public class AttackState : State
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            StartCoroutine(Atack());
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            StopCoroutine(Atack());
        }

        private IEnumerator Atack()
        {
            yield return null;
        }
    }
}
