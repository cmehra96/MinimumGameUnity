using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Screens
{
    class MainMenu: MonoBehaviour
    {
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

    }
}
