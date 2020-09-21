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


        public void singleSwapFromDealtDeck(Player.Player player, Card swapCard)
        {
            GameController.Instance.SingleSwapFromDealtDeck(player, swapCard);
        }

        public void singleSwapFromDiscardedDeck(Player.Player player, Card swapCard)
        {
            throw new NotImplementedException();
        }
        public void multiSwapFromDealtDeck(Player.Player player, Deck tempLongTouchList)
        {
            throw new NotImplementedException();
        }

        public void multiSwapFromDiscardedDeck(Player.Player player, Deck tempLongTouchList)
        {
            throw new NotImplementedException();
        }

        public void sayMinimum(Player.Player player)
        {
            throw new NotImplementedException();
        }

    }
}
