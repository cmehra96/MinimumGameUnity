using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ScoreManager
{
  public class ScoreRow: MonoBehaviour
    {
        public Text Round;

        public Text Player1Score;

        public Text Player2Score;

        public Text Player3Score;

        public Text Player4Score;

        public Text Player5Score;

        public Text Player6Score;

        public void UpdateText(int round, int a, int b, int c, int d,int e,int f)
        {
            Round.text = round.ToString();
            Player1Score.text = a.ToString();
            Player2Score.text = b.ToString();
            Player3Score.text = c.ToString();
            Player4Score.text = d.ToString();
            Player5Score.text = e.ToString();
            Player6Score.text = f.ToString();
        }

        public void ClearText()
        {
            Round.text = String.Empty;
            Player1Score.text = String.Empty;
            Player2Score.text = String.Empty;
            Player3Score.text = String.Empty;
            Player4Score.text = String.Empty;
            Player5Score.text = String.Empty;
            Player6Score.text = String.Empty;
        }
    }
}
