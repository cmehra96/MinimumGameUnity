using Assets.Scripts.Admob;
using Assets.Scripts.Utility;
using UnityEngine;

namespace Assets.Scripts.Engine
{
    class BaseController : MonoBehaviour
    {
        public GameObject gameMaster;

        private void Awake()
        {
            if (GameMaster.instance == null && gameMaster != null)
                Instantiate(gameMaster);
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause == false)
            {
                UnityMainThreadDispatcher.Schedule(() =>
                {

                    AdmobController.instance.ShowInterstitial();
                }
       , 1.0f);
            }
            Debug.Log("On Application Pause");
        }
    }
}
