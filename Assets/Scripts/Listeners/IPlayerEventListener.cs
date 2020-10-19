using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.CardElements;
using Assets.Scripts.Player;
namespace Assets.Scripts.Listeners
{
  public  interface IPlayerEventListener
    {
        void SingleSwapFromDealtDeck(Player.Player player, Card swapCard);

        void MultiSwapFromDealtDeck(Player.Player player, Deck tempLongTouchList);

        void SingleSwapFromDiscardedDeck(Player.Player player, Card swapCard);

        void MultiSwapFromDiscardedDeck(Player.Player player, Deck tempLongTouchList);

        void SayMinimum(Player.Player player);
    }
}
