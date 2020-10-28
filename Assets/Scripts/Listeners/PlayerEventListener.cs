using Assets.Scripts.CardElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Listeners
{
    public class PlayerEventListener : IPlayerEventListener
    {


        public void SingleSwapFromDealtDeck(Player.Player player, Card swapCard)
        {
            GameController.Instance.SingleSwapFromDealtDeck(player, swapCard);
        }

        public void SingleSwapFromDiscardedDeck(Player.Player player, Card swapCard)
        {
            GameController.Instance.SingleSwapFromDiscardedDeck(player, swapCard);
        }
        public void MultiSwapFromDealtDeck(Player.Player player, Deck tempLongTouchList)
        {
            GameController.Instance.MultiCardDealtDeckSwap(player, tempLongTouchList);
        }

        public void MultiSwapFromDiscardedDeck(Player.Player player, Deck tempLongTouchList)
        {
            GameController.Instance.MultiCardDiscardedDeckSwap(player, tempLongTouchList);
        }

        public void SayMinimum(Player.Player player)
        {
            GameController.Instance.CallMinium(player);
        }

    }
}
