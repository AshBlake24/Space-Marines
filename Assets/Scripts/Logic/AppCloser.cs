using Roguelike.Infrastructure.States;
using UnityEngine;

namespace Roguelike.Logic
{
    public class AppCloser : MonoBehaviour
    {
        private GameStateMachine _stateMachine;

        public void Construct(GameStateMachine stateMachine) => 
            _stateMachine = stateMachine;

        private void OnApplicationQuit() => 
            _stateMachine.Enter<ExitGameState>();
    }
}