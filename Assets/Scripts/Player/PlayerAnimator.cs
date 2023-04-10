using System;
using Roguelike.Logic.Animations;
using UnityEngine;

namespace Roguelike.Player
{
    public static class PlayerAnimator
    {
            AnimatorState state = GetState(stateHash);
            StateExited?.Invoke(state);
        }

        private AnimatorState GetState(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _walkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;
            
            return state;
        }
    }
}