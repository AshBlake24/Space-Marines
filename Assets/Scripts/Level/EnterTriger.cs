using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Player;
using Roguelike.StaticData.Levels;
using UnityEngine;
using UnityEngine.Events;

namespace Roguelike.Level
{
    public class EnterTriger : MonoBehaviour, IProgressWriter
    {
        private StageId _nextStageId;
        
        public event UnityAction<PlayerHealth> PlayerHasEntered;

        public void Counstruct(StageId nextStageId)
        {
            _nextStageId = nextStageId;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth player))
            {
                PlayerHasEntered?.Invoke(player);
                gameObject.SetActive(false);
            }
        }

        public void WriteProgress(PlayerProgress progress) => 
            progress.WorldData.CurrentStage = _nextStageId;

        public void ReadProgress(PlayerProgress progress)
        {
        }
    }
}
