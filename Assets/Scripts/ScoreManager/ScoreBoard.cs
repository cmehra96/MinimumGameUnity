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
            InvokeRepeating("UpdateScoreCard", 0f, 1f);
        }

        public void UpdateScoreCard()
        { int count = GameController.Instance.players.Count;
            if (count < 6)
                return;
           for(int i=0;i<count;i++)
            {
                playerNameColoumn[i].text = GameController.Instance.players[i].GetName();
            }
           for(int i=0;i< rows.Length;i++)
            {
                PopulateData(i);
            }
            total[0].text = GameController.Instance.players[0].GetTotalScore().ToString();
            total[1].text = GameController.Instance.players[1].GetTotalScore().ToString();
            total[2].text = GameController.Instance.players[2].GetTotalScore().ToString();
            total[3].text = GameController.Instance.players[3].GetTotalScore().ToString();
            total[4].text = GameController.Instance.players[4].GetTotalScore().ToString();
            total[5].text = GameController.Instance.players[5].GetTotalScore().ToString();
        }

        private void PopulateData(int index)
        {
            rows[index].GetComponent<CanvasGroup>().alpha = 1.0f;
            rows[index].UpdateText(index+1, GameController.Instance.players[0].GetCurrentRoundScore(),
                 GameController.Instance.players[1].GetCurrentRoundScore(),
                 GameController.Instance.players[2].GetCurrentRoundScore(),
                 GameController.Instance.players[3].GetCurrentRoundScore(),
                 GameController.Instance.players[4].GetCurrentRoundScore(),
                 GameController.Instance.players[5].GetCurrentRoundScore());
        }
    }
}
