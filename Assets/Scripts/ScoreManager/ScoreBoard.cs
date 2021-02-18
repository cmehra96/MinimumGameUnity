using Assets.Scripts.Player;
using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ScoreManager
{
   public class ScoreBoard: MonoBehaviour
    {
        public Text[] total = new Text[6];
        public ScoreRow[] rows;
        public Text[] playerNameColoumn = new Text[6];
        public static ScoreBoard Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }
        public void Start()
        {
        //    InvokeRepeating("UpdateScoreCard", 0f, 1f);
        }

        public void UpdateScoreCard()
        {
            int count = GameController.Instance.players.Count;
            if (count < 6)
                return;
            for (int i = 0; i < count; i++)
            {
                playerNameColoumn[i].text = GameController.Instance.players[i].GetName();
            }
            for (int i = 0; i < GameController.Instance.GetRoundCounter(); i++)
            {
                PopulateData(i);
            }
            
            if (GameController.Instance.GetSetCounter() == Constants.totalmatch && GameController.Instance.GetRoundCounter()==Constants.totalrounds)
            {
                UpdateRank();
                UpdateTotalText(true);
                UnityMainThreadDispatcher.Instance().ClearQueue();
                Invoke("ShowGameOver", 5.0f);
            }
            else
            {
                UpdateTotalText(false);
            }
        }

        private void UpdateTotalText(bool isGameOver)
        {
            if (isGameOver)
            {
                total[0].text = GameController.Instance.players[0].GetTotalScore().ToString() + Constants.ranksToText[GameController.Instance.players[0].GetRank()];
                total[1].text = GameController.Instance.players[1].GetTotalScore().ToString() + Constants.ranksToText[GameController.Instance.players[1].GetRank()];
                total[2].text = GameController.Instance.players[2].GetTotalScore().ToString() + Constants.ranksToText[GameController.Instance.players[2].GetRank()];
                total[3].text = GameController.Instance.players[3].GetTotalScore().ToString() + Constants.ranksToText[GameController.Instance.players[3].GetRank()];
                total[4].text = GameController.Instance.players[4].GetTotalScore().ToString() + Constants.ranksToText[GameController.Instance.players[4].GetRank()];
                total[5].text = GameController.Instance.players[5].GetTotalScore().ToString() + Constants.ranksToText[GameController.Instance.players[5].GetRank()]; 
            }
            else
            {
                total[0].text = GameController.Instance.players[0].GetTotalScore().ToString();
                total[1].text = GameController.Instance.players[1].GetTotalScore().ToString();
                total[2].text = GameController.Instance.players[2].GetTotalScore().ToString();
                total[3].text = GameController.Instance.players[3].GetTotalScore().ToString();
                total[4].text = GameController.Instance.players[4].GetTotalScore().ToString();
                total[5].text = GameController.Instance.players[5].GetTotalScore().ToString();
            }
        }

        private void UpdateRank()
        {
            PlayerList players = GameController.Instance.GetPlayers();
            for(int i=0;i<players.Count;i++)
            {
                int rank = 1;
                for(int j=0;j<players.Count;j++)
                {
                    if (i == j)
                        continue;
                    if (players[i].GetTotalScore() > players[j].GetTotalScore())
                        rank++;
                }
                players[i].SetRank(rank);
            }
        }

        private void PopulateData(int index)
        {
            rows[index].GetComponent<CanvasGroup>().alpha = 1.0f;
            rows[index].UpdateText(index+1, GameController.Instance.players[0].GetPreviousRoundScoreByIndex(index),
                 GameController.Instance.players[1].GetPreviousRoundScoreByIndex(index),
                 GameController.Instance.players[2].GetPreviousRoundScoreByIndex(index),
                 GameController.Instance.players[3].GetPreviousRoundScoreByIndex(index),
                 GameController.Instance.players[4].GetPreviousRoundScoreByIndex(index),
                 GameController.Instance.players[5].GetPreviousRoundScoreByIndex(index));
        }
        public void ClearText()
        {
            for(int i=0;i<rows.Count();i++)
            {
                rows[i].ClearText();
            }
        }

        private void ShowGameOver()
        {
            GameController.Instance.gameoverPopup.ShowPopup();
        }
    }
}
