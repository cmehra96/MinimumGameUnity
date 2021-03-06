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
        private void Start()
        {
            AdmobController.instance.ShowBanner();
            AdmobController.instance.ShowInterstitial();
        }

        public void StartComputerPlay()
        {
            GameController.currentGameMode = GameMode.Computer;
            SceneManager.LoadScene("Game");
        }
        public void StartMultiPlay()
        {
            EnterMultiplayer();
        }

        private void EnterMultiplayer()
        {
            GameController.currentGameMode = GameMode.MultiPlayer;
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
