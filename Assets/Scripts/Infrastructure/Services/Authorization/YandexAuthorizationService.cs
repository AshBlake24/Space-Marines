using System;
using Agava.YandexGames;

namespace Roguelike.Infrastructure.Services.Authorization
{
    public class YandexAuthorizationService : IAuthorizationService
    {
        public bool IsAuthorized => PlayerAccount.IsAuthorized;

        public event Action Authorized;

        public void Authorize()
        {
            if (IsAuthorized == false)
                PlayerAccount.Authorize(OnAuthorized);
        }

        private void OnAuthorized() => Authorized?.Invoke();
    }
}