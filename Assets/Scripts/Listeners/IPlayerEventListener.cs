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
        void singleSwapFromDealtDeck(Player.Player player, Card swapCard);

        void multiSwapFromDealtDeck(Player.Player player, Deck tempLongTouchList);

        void singleSwapFromDiscardedDeck(Player.Player player, Card swapCard);

        void multiSwapFromDiscardedDeck(Player.Player player, Deck tempLongTouchList);

        void sayMinimum(Player.Player player);
    }
}
