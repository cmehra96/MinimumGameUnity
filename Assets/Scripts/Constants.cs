using Assets.Scripts.Admob;
using System.Collections.Generic;
namespace Assets.Scripts
{
    public class Constants
    {
        public const float turnPlayerDelay = 2.0f;
        public const float clearMethodDelay = 1.5f;
        public const int totalrounds = 4;
        public const int totalmatch = 3;
        public const float startnextround = 0.5f;
        public const float maxTimer = 10f;
        public const string sessionCount = "SessionCount";
        public const int sessionLimit = 5;
        public const string AndroidURL = "https://play.google.com/store/apps/details?id=com.cmehra.minimum";
        public const string hideRatePopupForever = "HideRatePopup";
        /// <summary>
        /// Production ads keys uncomment them before deploying to prod.
        /// </summary>
        //public const string androidInterstitial= "ca-app-pub-9121154268667556/8821229341";
        //public const string iosInterstitial = "ca-app-pub-3940256099942544/4411468910";
        //public const string androidBanner = "ca-app-pub-9121154268667556/9012801032";
        //public const string iosBanner = "ca-app-pub-3940256099942544/2934735716";
        //public const string androidRewarded = "ca-app-pub-9121154268667556/4690412641";
        //public const string iosRewarded = "ca-app-pub-3940256099942544/1712485313";

        /// <summary>
        /// Test ads keys comment them before deploying to prod.
        /// </summary>
        //public const string androidInterstitial = "ca-app-pub-3940256099942544/1033173712";
        //public const string iosInterstitial = "ca-app-pub-3940256099942544/4411468910";
        //public const string androidBanner = "ca-app-pub-3940256099942544/6300978111";
        //public const string iosBanner = "ca-app-pub-3940256099942544/2934735716";
        //public const string androidRewarded = "ca-app-pub-3940256099942544/5224354917";
        //public const string iosRewarded = "ca-app-pub-3940256099942544/1712485313";
        public static Dictionary<int, string> ranksToText = new Dictionary<int, string>
        {
            {1," (1st)" },
            {2," (2nd)" },
            {3," (3rd)" },
            {4," (4th)" },
            {5," (5th)" },
            {6," (6th)" },
        };
    }
}
