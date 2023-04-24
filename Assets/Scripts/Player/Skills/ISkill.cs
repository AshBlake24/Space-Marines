namespace Roguelike.Player.Skills
{
    public interface ISkill
    {
        bool IsActive { get; }
        bool ReadyToUse { get; }
        void UseSkill();
    }
}