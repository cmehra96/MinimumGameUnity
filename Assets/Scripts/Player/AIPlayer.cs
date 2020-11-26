using Assets.Scripts.CardElements;
using Assets.Scripts.Utility;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts.Player
{
    class AIPlayer : Player
    {
        private int callPercent = 0;
        public AIPlayer(string name) : base(name)
        {

        }

        public override void NotifyPlayerForTurn()
        {
            Debug.Log("Inside Notify AIPlayer For Turn Method");
            
            UnityMainThreadDispatcher.Schedule(() =>
            {
                ChoseActionToPlayAndInformListeners();
            },
            0.5f);
        }
        /// <summary>
        /// Method to decide which action to be taken by Player based on call percent calculation
        /// </summary>
        private void ChoseActionToPlayAndInformListeners()
        {
            int callPercent = GetCallPercent(GameController.Instance.GetPlayers(), this);
            Debug.Log("Call Percent" + callPercent);
            if (callPercent >= 75)
            {
                listener.SayMinimum(this);
            }
            else if (myDeck.CardsCount() < 3)
            {
                PickBestCard(myDeck, GameController.Instance.DiscardedDeck.GetTopCard());
            }
            else
            {
                int straightResult = HandCombination.IsStraight(this, GameController.Instance.DiscardedDeck.GetTopCard());
                Debug.Log("Straight Method result" + straightResult);
                // int threeOfKindResult = HandCombination.isThreeOfKind(this, GameController.Instance.DealtDeck.GetTopCard());
                int threeOfKindResult = 0;
                Debug.Log("Three of kinds method result" + threeOfKindResult);
                if (straightResult == 1)       //Straight card exist in Player Deck
                {
                    Debug.Log("Straight Card exist in Player Deck getting it now");
                    Deck straightCards = HandCombination.GetStraight(this);
                    listener.MultiSwapFromDiscardedDeck(this, straightCards);
                }
                else if (straightResult == -1) //Straight card created by deck and discarded deck card combination
                {
                    Debug.Log("Straight card created by deck and discarded deck card combination");
                    Card nonStraightCard = HandCombination.CreateStraight(this, GameController.Instance.DiscardedDeck.GetTopCard());
                    listener.SingleSwapFromDiscardedDeck(this, nonStraightCard);
                }
                else if (threeOfKindResult == 1) //Three of kind exist in Player Deck
                {
                    Debug.Log("Three of kind exist in Player Deck getting it now");
                    Deck threeOfKind = HandCombination.GetThreeOfKind(this);
                    listener.MultiSwapFromDiscardedDeck(this, threeOfKind);
                }
                else if (threeOfKindResult == -1)  //Three of kind created using Player deck and Discarded Deck card
                {
                    Debug.Log("Three of kind created using Player deck and Discarded Deck card");
                    Card threeOfKindCard = HandCombination.CreateThreeOfKind(this, GameController.Instance.DiscardedDeck.GetTopCard());
                    listener.SingleSwapFromDiscardedDeck(this, threeOfKindCard);
                }
                else
                    PickBestCard(myDeck, GameController.Instance.DiscardedDeck.GetTopCard()); //IF nothing match, pick AI player  deck largest card
            }
        }

        /// <summary>
        /// Method that decide swapped Card to be picked either from Dealt Deck or Discarded Deck
        /// </summary>
        /// <param name="myDeck">Current Player Deck</param>
        /// <param name="deckTopCard">Top Card of Discarded Deck</param>
        private void PickBestCard(Deck myDeck, Card deckTopCard)
        {
            Debug.Log("Inside Pick Best Card Method");
            Tuple<Card, bool> result = GetLargestCardByRank(deckTopCard);
            if (result.Item2 == true)
                listener.SingleSwapFromDealtDeck(this, result.Item1);
            else
                listener.SingleSwapFromDiscardedDeck(this, result.Item1);
        }

        /// <summary>
        /// Method that largest Card from Deck
        /// </summary>
        /// <param name="deckTopCard">Current Player Deck</param>
        /// <returns>Highest Card of Deck and true value if Card Value is greater than Discarded Deck Top Card
        /// else False</returns>
        private Tuple<Card, bool> GetLargestCardByRank(Card deckTopCard)
        {
            Card temp = myDeck.GetCardByIndex(0);
            int size = myDeck.CardsCount();
            for (int i = 1; i < size; i++)
            {
                if (myDeck.GetCardByIndex(i).GetRankValue() > temp.GetRankValue())
                    temp = myDeck.GetCardByIndex(i);
            }
            if (temp.GetRankValue() > deckTopCard.GetRankValue())
                return new Tuple<Card, bool>(temp, true);
            else
                return new Tuple<Card, bool>(temp, false);
        }

        /// <summary>
        /// Get Call Percent based on analysis of cards of all Players
        /// </summary>
        /// <param name="playerLists">List of all Players</param>
        /// <param name="aIPlayer">Current AI Player</param>
        /// <returns></returns>
        private int GetCallPercent(PlayerList playerLists, AIPlayer currentPlayer)
        {
            Debug.Log("Inside Call Percent Method");
            int cardCount = myDeck.CardsCount();
            int size = 0;
            int no_Of_Players = playerLists.Count;
            int lastRoundScore = previousRoundScore;
            Card roundCard = currentRoundCard;
            int tempPercent = 0;
            int index = 0;
            int score = EvaluateScore();
            if (cardCount <= 2)
            {
                if (score <= 3)
                {
                    callPercent = 100;
                    return callPercent;
                }

            }
            foreach (Player player in playerLists)
            {
                if (player == currentPlayer)
                    continue;
                int otherPlayerCard = player.GetDeck().CardsCount();
                int otherPlayerLastRoundScore = player.GetPreviousRoundScore();
                if (cardCount <= otherPlayerCard)     //if current player card is less than other player card
                {
                    // To Do Seprate previous Round original score and ScoreBoard Score
                    if ((lastRoundScore + roundCard.GetRankValue()) <= otherPlayerLastRoundScore) // if last round won by current player
                    {
                        tempPercent += 100;
                    }
                    else if ((lastRoundScore + roundCard.GetRankValue()) <= (otherPlayerLastRoundScore + 2))
                    {
                        tempPercent += new Random().Next(50, 100);
                    }
                    else
                    {
                        tempPercent += new Random().Next(101);
                    }
                }
                else
                {
                    if ((float)(score / cardCount) <= 2.5)
                    {
                        tempPercent += new Random().Next(50, 100);
                    }
                    else
                    {
                        tempPercent += new Random().Next(101);
                    }
                }
            }
            callPercent = tempPercent / (no_Of_Players - 1); //excluding current player
            return callPercent;
        }

    }
}
