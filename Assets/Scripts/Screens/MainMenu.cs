using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.RateGame;
namespace Assets.Scripts.Screens
{
    class MainMenu: MonoBehaviour
    {
        public GameObject Instructions;
        public Popup ratePopup;
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
                if (RateGame.RateGame.instance.CanShowPopup() && Popup.currentPopup == null)
                    ratePopup.ShowPopup();
                else
                    Quit();
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

        public void Quit()
        {
            Application.Quit();
        }

        public void HideRatePopup()
        {
            RateGame.RateGame.instance.ResetSessionCount();
            ratePopup.HidePopup();
        }

        public void DisableRatePopup()
        {
            HideRatePopup();
            RateGame.RateGame.instance.SetDisablePopup();
        }
    }
}
