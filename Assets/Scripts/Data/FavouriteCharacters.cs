using System;
using Roguelike.StaticData.Characters;

namespace Roguelike.Data
{
    [Serializable]
    public class FavouriteCharacters
    {
        public CharacterId Character;
        
        public void SetFavouriteCharacter(CharacterId characterId)
        {
            Character = characterId;
        }
    }
}