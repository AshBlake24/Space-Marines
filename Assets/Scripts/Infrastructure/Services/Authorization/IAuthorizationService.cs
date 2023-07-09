using System;

namespace Roguelike.Infrastructure.Services.Authorization
{
    public interface IAuthorizationService : IService
    {
        bool IsAuthorized { get; }
        
        event Action Authorized;
        
        void Authorize();
    }
}