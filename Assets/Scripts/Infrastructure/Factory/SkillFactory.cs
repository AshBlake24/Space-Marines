using Roguelike.Player;
using Roguelike.Player.Skills;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public class SkillFactory
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SkillFactory(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public void CreatePlayerSkill(GameObject player)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            
            ISkill skill = new RegenerationSkill(
                _coroutineRunner,
                playerHealth,
                healthPerTick: 1,
                ticksCount: 3,
                cooldownBetweenTicks: 3,
                skillCooldown: 60);


            player.GetComponent<PlayerSkill>().Construct(skill);
        }
    }
}