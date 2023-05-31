using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Player.Enhancements;
using Roguelike.Weapons.Stats;

namespace Roguelike.Weapons
{
    public interface IWeapon : IProgressWriter, IEnhanceable<int>
    {
        WeaponStats Stats { get; }
        bool TryAttack();
        void Show();
        void Hide();
    }
}