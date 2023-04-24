using System;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Player;
using Roguelike.Player.Skills;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Skills;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public class SkillFactory : ISkillFactory
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IPersistentDataService _persistentData;
        private readonly IStaticDataService _staticData;

        public SkillFactory(ICoroutineRunner coroutineRunner, IStaticDataService staticData, IPersistentDataService persistentData)
        {
            _coroutineRunner = coroutineRunner;
            _persistentData = persistentData;
            _staticData = staticData;
        }
        
        public void CreatePlayerSkill(GameObject player)
        {
            CharacterStaticData characterData = _staticData.GetCharacterData(_persistentData.PlayerProgress.Character);
            SkillStaticData skillData = _staticData.GetSkillStaticData(characterData.Skill.Id);

            ISkill skill = ConstructSkill(skillData, player);

            player.GetComponent<PlayerSkill>().Construct(skill);
        }

        private ISkill ConstructSkill(SkillStaticData skillData, GameObject player)
        {
            return skillData.Id switch
            {
                SkillId.Regeneration => CreateRegenerationSkill(skillData as RegenerationSkillStaticData, player),
                SkillId.Rage => CreateRageSkill(skillData as RageSkillStaticData, player),
                _ => throw new ArgumentNullException(nameof(SkillId), "This skill does not exist")
            };
        }

        private ISkill CreateRegenerationSkill(RegenerationSkillStaticData skillData, GameObject player)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            
            return new RegenerationSkill(
                _coroutineRunner,
                playerHealth,
                RegenerationSkillStaticData.HealthPerTick,
                skillData.TicksCount,
                skillData.CooldownBetweenTicks,
                skillData.SkillCooldown,
                skillData.SkillEffect);
        }

        private ISkill CreateRageSkill(RageSkillStaticData skillData, GameObject player)
        {
            PlayerShooter playerShooter = player.GetComponent<PlayerShooter>();

            return new RageSkill(
                _coroutineRunner,
                playerShooter,
                skillData.AttackSpeedMultiplier,
                skillData.SkillDuration,
                skillData.SkillCooldown,
                skillData.SkillEffect);
        }
    }
}