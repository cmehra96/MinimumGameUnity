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
            DataManager.currentSceneName = SceneManager.GetActiveScene().name;
            
        }
        private void Start()
        {
            AdmobController.instance.ShowBanner();
            AdmobController.instance.HideCustomBanner();
           // AdmobController.instance.ShowInterstitial();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
        public void StartComputerPlay()
        {
            DataManager.currentGameMode = GameMode.Computer;
            SceneManager.LoadScene("Game");
        }
        public void StartMultiPlay()
        {
            EnterMultiplayer();
        }

        private void EnterMultiplayer()
        {
            DataManager.currentGameMode = GameMode.MultiPlayer;
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
