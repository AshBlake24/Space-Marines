using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Authorization;
using Roguelike.Infrastructure.Services.Windows;

namespace Roguelike.UI.Windows.Confirmations
{
    public class AuthorizationWindow : ConfirmationWindow
    {
        private IAuthorizationService _authorizationService;

        public void Construct(IAuthorizationService authorizationService)
        {
            _authorizationService = AllServices.Container.Single<IAuthorizationService>();
        }
        
        protected override void Initialize()
        {
            base.Initialize();
            
            if (_authorizationService.IsAuthorized)
            {
                WindowService.Open(WindowId.AlreadyAuthorize);
                Destroy(gameObject);
            }
        }

        protected override void OnConfirm()
        {
            base.OnConfirm();
            
#if UNITY_WEBGL && !UNITY_EDITOR
            _authorizationService.Authorize();
#endif
            Close();
        }
    }
}