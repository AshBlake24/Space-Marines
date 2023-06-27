using System;
using System.Collections;
using Agava.YandexGames;
using Roguelike.Infrastructure.States;
using Roguelike.Logic;
using UnityEngine;

namespace Roguelike.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingScreen _loadingScreenPrefab;

        private Game _game;

        private void Awake()
        {
#if UNITY_EDITOR
            InitGame();
#else
            StartCoroutine(InitYandexSDK());
#endif
        }

        private void InitGame()
        {
            _game = new Game(this, Instantiate(_loadingScreenPrefab));
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        private IEnumerator InitYandexSDK()
        {
            yield return YandexGamesSdk.Initialize();
            
            if (YandexGamesSdk.IsInitialized == false)
                throw new ArgumentNullException(nameof(YandexGamesSdk), "Yandex SDK didn't initialize correctly");
            
            RequestData();
            InitGame();
        }
        
        private void RequestData()
        {
            if (PlayerAccount.HasPersonalProfileDataPermission == false)
                PlayerAccount.RequestPersonalProfileDataPermission();
        }
    }
}