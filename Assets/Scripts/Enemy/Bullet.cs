using Roguelike.Player;
using Roguelike.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike
{
    public class Bullet : MonoBehaviour
    {
        private Vector3 _direction;

        public void Init(Vector3 direction)
        {
            _direction= direction;
        }

        private void OnEnable()
        {
            StartCoroutine(LifeTimer(7f));
        }

        private void Update()
        {
            Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerComponent>(out PlayerComponent player))
            {
                Debug.Log("Hit");
                gameObject.SetActive(false);
            }
        }

        private void Move()
        {
            transform.position += _direction * Time.deltaTime;
        }

        private IEnumerator LifeTimer(float time)
        {
            while (time > 0)
            {
                time-= Time.deltaTime;
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
}
