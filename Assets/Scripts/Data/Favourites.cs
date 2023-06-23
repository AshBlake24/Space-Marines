using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Enhancements;
using Roguelike.StaticData.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class Favourites
    {
        public List<WeaponUsageData> WeaponsUsageData;
        public List<CharacterUsageData> CharactersUsageData;
        public List<EnhancementUsageData> EnhancementsUsageData;

        public Favourites()
        {
            WeaponsUsageData = new List<WeaponUsageData>();
            CharactersUsageData = new List<CharacterUsageData>();
            EnhancementsUsageData = new List<EnhancementUsageData>();
        }

        public WeaponId FavouriteWeapon
        {
            get
            {
                return WeaponsUsageData.Count switch
                {
                    < 1 => WeaponId.Unknown,
                    < 2 => WeaponsUsageData[0].Id,
                    _ => WeaponsUsageData.Aggregate((i1, i2) => i1.UsedTimes > i2.UsedTimes ? i1 : i2).Id
                };
            }
        }

        public CharacterId FavouriteCharacter
        {
            get
            {
                return CharactersUsageData.Count switch
                {
                    < 2 => CharactersUsageData[0].Id,
                    _ => CharactersUsageData.Aggregate((i1, i2) => i1.UsedTimes > i2.UsedTimes ? i1 : i2).Id
                };
            }
        }

        public EnhancementId FavouriteEnhancement
        {
            get
            {
                return EnhancementsUsageData.Count switch
                {
                    < 1 => EnhancementId.Unknown,
                    < 2 => EnhancementsUsageData[0].Id,
                    _ => EnhancementsUsageData.Aggregate((i1, i2) => i1.UsedTimes > i2.UsedTimes ? i1 : i2).Id
                };
            }
        }

        public void AddWeapons(WeaponId[] weapons)
        {
            foreach (WeaponId weaponId in weapons)
            {
                WeaponUsageData weaponUsageData = WeaponsUsageData
                    .SingleOrDefault(weapon => weapon.Id == weaponId);

                if (weaponUsageData != null)
                    weaponUsageData.UsedTimes++;
                else if (weaponId != WeaponId.Unknown)
                    WeaponsUsageData.Add(new WeaponUsageData(weaponId));
            }
        }

        public void AddCharacter(CharacterId characterId)
        {
            CharacterUsageData characterUsageData = CharactersUsageData
                .SingleOrDefault(character => character.Id == characterId);
            
            if (characterUsageData != null)
                characterUsageData.UsedTimes++;
            else
                CharactersUsageData.Add(new CharacterUsageData(characterId));
        }
        
        public void AddEnhancement(EnhancementId enhancementId)
        {
            EnhancementUsageData enhancementUsageData = EnhancementsUsageData
                .SingleOrDefault(enhancement => enhancement.Id == enhancementId);
            
            if (enhancementUsageData != null)
                enhancementUsageData.UsedTimes++;
            else
                EnhancementsUsageData.Add(new EnhancementUsageData(enhancementId));
        }
    }
}