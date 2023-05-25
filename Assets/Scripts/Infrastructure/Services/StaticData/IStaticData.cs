using System;

namespace Roguelike.Infrastructure.Services.StaticData
{
    public interface IStaticData
    {
        public Enum Key { get; }
    }
}