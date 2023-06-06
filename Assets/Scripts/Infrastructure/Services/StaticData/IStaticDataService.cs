using System;
using System.Collections.Generic;
using Roguelike.StaticData;
using Roguelike.StaticData.Player;
using Roguelike.StaticData.Weapons;
using Roguelike.StaticData.Loot.Powerups;

namespace Roguelike.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        PlayerStaticData Player { get; }
        GameConfig GameConfig { get; }

        void Load();

        TResult GetDataById<TKey, TResult>(TKey id) where TKey : Enum 
            where TResult : IStaticData;

        IEnumerable<TData> GetAllDataByType<TEnum, TData>() 
            where TEnum : Enum 
            where TData : IStaticData;
    }
}