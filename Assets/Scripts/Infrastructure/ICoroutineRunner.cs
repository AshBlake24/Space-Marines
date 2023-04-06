using System.Collections;
using UnityEngine;

namespace Roguelike.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator couroutine);
    }
}