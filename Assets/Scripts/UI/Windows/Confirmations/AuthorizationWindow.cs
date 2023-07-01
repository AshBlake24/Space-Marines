using System;
using Agava.YandexGames;
using UnityEngine;

namespace Roguelike.UI.Windows.Confirmations
{
    public class AuthorizationWindow : ConfirmationWindow
    {
        protected override void Initialize()
        {
            base.Initialize();
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
                Destroy(gameObject);
#endif
        }

        protected override void OnConfirm()
        {
            base.OnConfirm();
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
                throw new ArgumentNullException(nameof(PlayerAccount), "Account has already authorized");

            PlayerAccount.Authorize();
#endif
            Close();
        }
    }
}