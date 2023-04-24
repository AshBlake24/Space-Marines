using Roguelike.Infrastructure.Services;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface ISkillFactory : IService
    {
        void CreatePlayerSkill(GameObject player);
    }
}