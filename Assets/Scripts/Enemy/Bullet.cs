using Roguelike.Player;
using Roguelike.Utilities;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Roguelike
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Vector3 _direction;

        public void Init(Vector3 direction)
        {
            _direction= (transform.position + direction*100);
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
                Destroy(gameObject);
            }
            else if (other.gameObject.TryGetComponent<Wall>(out Wall wall))
            {
                Debug.Log("Wall");
                Destroy(gameObject);
            }
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, _direction, _speed*Time.deltaTime);
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
