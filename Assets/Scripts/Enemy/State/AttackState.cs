using Roguelike.Player;
using Roguelike.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public class AttackState : State
    {
        [SerializeField] private Bullet _bullet;

        protected override void OnDisable()
        {
            base.OnDisable();

            StopCoroutine(Atack());
        }

        public override void Enter(PlayerComponent target)
        {
            base.Enter(target);

            StartCoroutine(Atack());
        }

        private IEnumerator Atack()
        {
            while (true)
            {
                Bullet bullet = Instantiate<Bullet>(_bullet, transform.position, Quaternion.identity);
                bullet.Init(player.transform.position - transform.position);

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
