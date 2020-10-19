using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerUIMapping : MonoBehaviour
    {
        public List<GameObject> cardholder;        // Base of which cards are drawn for each Player
        public List<GameObject> turnIndicators;       //Holder for turn of each player

        public static PlayerUIMapping Instance
        {
            get;
            private set;
        }

        PlayerUIMapping()
        {

        }

        private void Awake()
        {
            PlayerUIMapping.Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            turnIndicators[0].SetActive(false);
            turnIndicators[1].SetActive(false);
            turnIndicators[2].SetActive(false);
            turnIndicators[3].SetActive(false);
            turnIndicators[4].SetActive(false);
            turnIndicators[5].SetActive(false);
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}