using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RateGame
{
    class RateGame: MonoBehaviour
    {
        private static RateGame instance;
       
        private void Start()
        {
            if(instance==null)
            {
                instance = this;
                SetStartTime();
            }
        }

        private void SetStartTime()
        {
            PlayerPrefs.SetString(Constants.openTime, System.DateTime.Now.ToBinary().ToString());
        }
        
        /// <summary>
        /// Method to determin no of hours played in this session
        /// </summary>
        /// <returns>Hours of played in session</returns>
        public double GetTimeSinceOpen()
        {
            long temp = System.Convert.ToInt64(PlayerPrefs.GetString(Constants.openTime)); 
            System.DateTime oldDate = System.DateTime.FromBinary(temp);
            System.DateTime currentDate = System.DateTime.Now;
            System.TimeSpan difference = currentDate.Subtract(oldDate);
            return difference.TotalHours;
        }

        public bool CanShowPopup()
        {
            if (GetTimeSinceOpen() >= Constants.popupScheduleTime)
                return true;
            else
                return false;
            
        }

        private void OnApplicationQuit()
        {
            
        }
    }
}
