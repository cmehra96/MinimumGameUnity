using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerUIMapping : MonoBehaviour
    {
        public List<GameObject> cardholder;        // Base of which cards are drawn for each Player


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

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}