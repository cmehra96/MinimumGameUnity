using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public class Timer
    {
        private static MonoBehaviour behaviour;
        public delegate void Task();

        public static void Schedule(MonoBehaviour _behaviour, float delay, Task task)
        {
            behaviour = _behaviour;
           // behaviour.StartCoroutine(DoTask(task, delay));
        }

        private static IEnumerator DoTask(Task task, float delay)
        {
            Debug.Log("Inside DoTask Coroutine");
            yield return new WaitForSeconds(delay);
            task();
        }
    }
}
