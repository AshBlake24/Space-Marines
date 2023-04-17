using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Roguelike
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] protected State targetState;

        public State TargetState => targetState;

        public UnityAction<State> NeedTransit;
    }
}
