using UnityEngine;

namespace Roguelike.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootstrapper _gameBootstrapperPrefab;

        private static bool s_isInitialized;

        private void Awake()
        {
            Debug.Log("Game Run!");
            if (s_isInitialized)
                return;

            GameBootstrapper bootstrapper = FindObjectOfType<GameBootstrapper>();

            if (bootstrapper == null)
                Instantiate(_gameBootstrapperPrefab);

            s_isInitialized = true;
            Debug.Log("Game Initialized!");
        }
    }
}