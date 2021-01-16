using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Screens
{
    class MainMenu: MonoBehaviour
    {
        public GameObject Instructions;
        public static MainMenu Instance
        {
            get;
            private set;
        }

        private void Awake()
        {
            Instance = this;
            
        }

        public void StartComputerPlay()
        {
            SceneManager.LoadScene("Game");
        }

        public void ShowInstructions()
        {
            Instructions.SetActive(true);
        }

        public void HideInstructions()
        {
            Instructions.SetActive(false);
        }

    }
}
