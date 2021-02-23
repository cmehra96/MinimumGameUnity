
using UnityEngine;

namespace Assets.Scripts.Admob
{
    [System.Serializable]
  public  class AdmobConfig : MonoBehaviour
    {
        public Admob admob;

        [Header("")]
        public int adPeriod;
        public int rewardedVideoPeriod;
        public int rewardedVideoAmount;
        public string androidPackageID;
        public string iosAppID;

        public static AdmobConfig instance;
        private void Awake()
        {
            instance = this;
        }
    }

    [System.Serializable]
    public class Admob
    {
        [Header("Interstitial")]
        public string androidInterstitial;
        public string iosInterstitial;
        [Header("Banner")]
        public string androidBanner;
        public string iosBanner;
        [Header("RewardedVideo")]
        public string androidRewarded;
        public string iosRewarded;
    }
}
