using Roguelike.Data;
using Roguelike.Infrastructure.Services.StaticData;

namespace Roguelike.Infrastructure.Services.PersistentData
{
    public class PersistentDataService : IPersistentDataService
    {
        private readonly IStaticDataService _staticData;

        public PersistentDataService(IStaticDataService staticData) => 
            _staticData = staticData;

        public PlayerProgress PlayerProgress { get; set; }

        public void Reset()
        {
            PlayerProgress.State.ResetState();
            PlayerProgress.WorldData = new WorldData(
                _staticData.GameConfig.StartPlayerLevel,
                _staticData.GameConfig.StartPlayerStage);
        }
    }
}