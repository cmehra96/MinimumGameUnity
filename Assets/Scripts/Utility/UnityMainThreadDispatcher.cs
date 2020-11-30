using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace Assets.Scripts.Utility
{
	public class UnityMainThreadDispatcher : MonoBehaviour
	{

		private static readonly Queue<Action> _executionQueue = new Queue<Action>();
		private Queue<IEnumerator> coroutineQueue = new Queue<IEnumerator>();
		public delegate void Task();
		public List<System.Threading.Tasks.Task> TaskList = new List<System.Threading.Tasks.Task>();
		public void Update()
		{
			lock (_executionQueue)
			{
				while (_executionQueue.Count > 0)
				{
					_executionQueue.Dequeue().Invoke();
				}
			}
		}

		/// <summary>
		/// Locks the queue and adds the IEnumerator to the queue
		/// </summary>
		/// <param name="action">IEnumerator function that will be executed from the main thread.</param>
		public void Enqueue(IEnumerator action)
		{
			lock (_executionQueue)
			{
				_executionQueue.Enqueue(() => {
					StartCoroutine(action);
				});
			}
		}

		/// <summary>
		/// Locks the queue and adds the IEnumerator to the queue
		/// </summary>
		/// <param name="action">IEnumerator function that will be executed from the main thread.</param>
		public void Enqueue(Task task, float delay)
		{
			lock (_executionQueue)
			{
				_executionQueue.Enqueue(() => {
					StartCoroutine(DoTask(task, delay));
				});
			}
		}

		public static void Schedule(Task task, float delay)
		{
			Debug.Log("Task Scheduled " + task.Method);
			//Instance().Enqueue(task, delay);
			Instance().coroutineQueue.Enqueue(DoTask(task, delay));

		}
		public static void ScheduleList(List<Task> tasks, float delay)
        {
			foreach (Task task in tasks)
				Schedule(task, delay);
        }

		private static IEnumerator DoTask(Task task, float delay)
		{
			Debug.Log("Inside DoTask Coroutine");
			yield return new WaitForSeconds(delay);
			task();
		}

		IEnumerator CoroutineCoordinator()
		{
			while (true)
			{
				while (coroutineQueue.Count > 0)
					yield return StartCoroutine(coroutineQueue.Dequeue());
				yield return null;
			}
		}

		/// <summary>
		/// Locks the queue and adds the Action to the queue
		/// </summary>
		/// <param name="action">function that will be executed from the main thread.</param>
		public void Enqueue(Action action)
		{
			Enqueue(ActionWrapper(action));
		}

		/// <summary>
		/// Locks the queue and adds the Action to the queue, returning a Task which is completed when the action completes
		/// </summary>
		/// <param name="action">function that will be executed from the main thread.</param>
		/// <returns>A Task that can be awaited until the action completes</returns>
		//public Task EnqueueAsync(Action action)
		//{
		//	var tcs = new TaskCompletionSource<bool>();

		//	void WrappedAction()
		//	{
		//		try
		//		{
		//			action();
		//			tcs.TrySetResult(true);
		//		}
		//		catch (Exception ex)
		//		{
		//			tcs.TrySetException(ex);
		//		}
		//	}

		//	Enqueue(ActionWrapper(WrappedAction));
		//	return tcs.Task;
		//}


		IEnumerator ActionWrapper(Action a)
		{
			a();
			yield return null;
		}


		private static UnityMainThreadDispatcher _instance = null;

		public static bool Exists()
		{
			return _instance != null;
		}

		public static UnityMainThreadDispatcher Instance()
		{
			if (!Exists())
			{
				throw new Exception("UnityMainThreadDispatcher could not find the UnityMainThreadDispatcher object. Please ensure you have added the MainThreadExecutor Prefab to your scene.");
			}
			return _instance;
		}

        private void Start()
        {
			StartCoroutine(CoroutineCoordinator());
		}


        void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
				DontDestroyOnLoad(this.gameObject);
			}
		}

		void OnDestroy()
		{
			_instance = null;
		}


	}

}
