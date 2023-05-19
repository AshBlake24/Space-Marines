using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.StaticData.Levels;
using UnityEngine;

namespace Roguelike.Level
{
    public class FinishLevelTriger : EnterTriger, IProgressWriter
    {
        private StageId _nextStageId;
        private IPersistentDataService _persistentDataService;

        public void Construct(StageId nextStageId, IPersistentDataService persistentDataService)
        {
            _nextStageId = nextStageId;
            _persistentDataService = persistentDataService;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            WriteProgress(_persistentDataService.PlayerProgress);
            base.OnTriggerEnter(other);
        }

        public void WriteProgress(PlayerProgress progress) =>
            progress.WorldData.CurrentStage = _nextStageId;

        public void ReadProgress(PlayerProgress progress)
        {
        }
    }
}
