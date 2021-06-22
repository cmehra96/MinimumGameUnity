using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RateGame
{
  public class RateGame: MonoBehaviour
    {
        public static RateGame instance { get; private set; }
       
        private void Start()
        {
            if(instance==null)
            {
                instance = this;
                if (IsPopupEnabled() == 0)
                    IncreaseSessionCount();
            }
        }

        private void IncreaseSessionCount()
        {
            int nrOfSessions = GetNumberOfSessions();
            nrOfSessions++;
            Debug.Log("No of sessions " + nrOfSessions);
            PlayerPrefs.SetInt(Constants.sessionCount, nrOfSessions);
        }

        private int GetNumberOfSessions()
        {
            return PlayerPrefs.GetInt(Constants.sessionCount);
        }

        public bool CanShowPopup()
        {
            if (GetNumberOfSessions() >= Constants.sessionLimit)
                return true;
            else
                return false;
            
        }
        public void ResetSessionCount()
        {
            PlayerPrefs.SetInt(Constants.sessionCount, 0);
        }

        public void SetDisablePopup()
        {
            PlayerPrefs.SetInt(Constants.hideRatePopupForever, 1);
        }

        public int IsPopupEnabled()
        {
           return PlayerPrefs.GetInt(Constants.hideRatePopupForever);
        }

    }
}
