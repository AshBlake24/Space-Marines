using System;
using Agava.YandexGames;
using UnityEngine;

namespace Roguelike.UI.Windows
{
    public class AuthorizationWindow : ConfirmationWindow
    {
        protected override void Initialize()
        {
            Debug.Log("Initialize inside authorization window");
#if UNITY_WEBGL && !UNITY_EDITOR
            Debug.Log("Is initialized: " + YandexGamesSdk.IsInitialized);

            if (YandexGamesSdk.IsInitialized == false)
                Destroy(gameObject);
#endif
        }

        protected override void OnConfirm()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            Debug.Log("Is authorized: " + PlayerAccount.IsAuthorized);
            if (PlayerAccount.IsAuthorized)
                throw new ArgumentNullException(nameof(PlayerAccount), "Account has already authorized");

            Debug.Log("Authorize");
            PlayerAccount.Authorize();
#endif
        }
    }
}