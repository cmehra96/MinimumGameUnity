using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.RateGame
{
    class StarPopup : MonoBehaviour
    {
        public Button sendBtn;
        public Transform starHolder;
        private void Start()
        {
            sendBtn.interactable = false;
        }

        /// <summary>
        /// Called when a star is clicked, activates the required stars
        /// </summary>
        /// <param name="touchedstarnumber"></param>
        public void StarClicked(int touchedstarnumber)
        {
            
            Sprite star_empty = Resources.Load<Sprite>("star_empty");
            Sprite star_fill = Resources.Load<Sprite>("Star_Full");
            for (int i=0;i<starHolder.childCount;i++)
            {
                Transform child = starHolder.GetChild(i);
                if (i < touchedstarnumber)
                    child.GetComponent<Image>().sprite = star_fill;
                else
                    child.GetComponent<Image>().sprite = star_empty;
            }
            sendBtn.interactable = true;
        }

    }
}
