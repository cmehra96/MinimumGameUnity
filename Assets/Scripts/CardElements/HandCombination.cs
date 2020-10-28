using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.CardElements
{
    public static class HandCombination
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"> Current Player</param>
        /// <param name="tempLongTouchList"> Deck of Touched Cards</param>
        /// <returns>true if selected cards belongs to Straight else false</returns>

        public static bool IsStraight(Player.Player player, Deck tempLongTouchList)
        {
            Debug.Log("Checking deck is straight or not");
            if (tempLongTouchList.CardsCount() < 3)
            {
                Debug.Log("Not enough cards");
                return false;
            }

            bool result = IsStraight(tempLongTouchList);
            return result;
        }

        public static bool IsStraight(Deck tempLongTouchList)
        {
            tempLongTouchList.SortByRankDesc();
            if (tempLongTouchList.GetCardByIndex(0).GetRankValue() == 1)       // If first card is Ace
            {
                Card tempCard = tempLongTouchList.GetCardByIndex(0);
                tempLongTouchList.Add(tempCard);
            }
            int straightcount = 1;
            for (int i = 0; i < tempLongTouchList.CardsCount() - 1; i++)
            {
                if (straightcount == 3)
                {

                    break;
                }
                int currentrank = tempLongTouchList.GetCardByIndex(i).GetRankValue();
                int nextrank = tempLongTouchList.GetCardByIndex(i + 1).GetRankValue();
                if (nextrank - currentrank == 1)           //if cards suit differ by 1, increment straight
                {
                    straightcount++;
                }
                else if (nextrank == 1 && currentrank == 13)     //specific condition for King and Ace
                {
                    straightcount++;
                }
                else if (nextrank - currentrank != 1)    //if cards suit not equal to 1, reset the straight counter
                {
                    straightcount = 1;
                }
            }
            if (tempLongTouchList.GetCardByIndex(0).GetRankValue() == 1)
                tempLongTouchList.Remove(tempLongTouchList.GetTopCard());
            if (straightcount == 3)
            {
                Debug.Log("Deck contains straight set");
                return true;
            }
            Debug.Log("Deck does not contain straight set");
            return false;

        }

        /// <summary>
        /// Method to find if straight set exist using combination player deck and Discarded Deck card
        /// </summary>
        /// <param name="player">Current player</param>
        /// <param name="card">Top Card Discarded Deck</param>
        /// <returns>1 if straight exist in deck belongs, 
        /// -1 if straight exist using combination of deck and @card parameter,
        /// 0 if no straight exist.</returns>
        public static int IsStraight(Player.Player player, Card card)
        {
            Debug.Log("Inside IsStraight Method");
            Deck temp = new Deck(player.GetDeck());
            temp.SortByRankDesc();
            if (temp.GetCardByIndex(0).GetRankValue() == 1)
            {
                Card tempCard = temp.GetCardByIndex(0);
                temp.Add(tempCard);
            }
            int straightCount = 1;   //To count number of straight cards from player deck
            int otherStaightCount = 1; // To count if straight belongs with {@card}
            int size = temp.CardsCount() - 1;
            for (int i = 0; i < size; i++)
            {
                if (straightCount == 3)
                    break;
                int currentRank = temp.GetCardByIndex(i).GetRankValue();
                int nextRank = temp.GetCardByIndex(i + 1).GetRankValue();
                if (nextRank - currentRank == 1)  //if cards suit differ by 1, increment straight
                    straightCount++;

                else if (nextRank == 1 && currentRank == 13)  //specific condition for King and Ace
                    straightCount++;
                else if (nextRank - currentRank != 1)  //if cards suit not equal to 1, reset the straight counter
                    straightCount = 1;


            }
            if (temp.GetCardByIndex(0).GetRankValue() == 1)
                temp.Remove(temp.GetTopCard());
            if (straightCount == 3)
            {
                Debug.Log("Straight exist in player deck");
                return 1;
            }
            // Excution to find straight cards with Discarded Deck card starts


            temp.Add(card);
            temp.SortByRankDesc();
            if (temp.GetCardByIndex(0).GetRankValue() == 1)
            {
                Card tempCard = temp.GetCardByIndex(0);
                temp.Add(tempCard);
            }
            for (int i = 0; i < temp.CardsCount() - 1; i++)
            {
                if (otherStaightCount == 3)
                    break;
                int currentRank = temp.GetCardByIndex(i).GetRankValue();
                int nextRank = temp.GetCardByIndex(i + 1).GetRankValue();
                if (nextRank - currentRank == 1)
                    otherStaightCount++;
                else if (nextRank == 1 && currentRank == 13)
                    otherStaightCount++;
                else if (nextRank - currentRank != 1)
                    otherStaightCount = 1;
            }
            temp.Remove(card);
            if (otherStaightCount == 3)
                return -1;
            return 0;


        }

        public static Deck GetStraight(Player.Player player)
        {
            Debug.Log("Inside Get Straight method");
            Deck straightCards = new Deck(player.GetDeck());
            Deck temp = new Deck(player.GetDeck());
            temp.SortByRankDesc();
            if (temp.GetCardByIndex(0).GetRankValue() == 1)
                temp.Add(temp.GetCardByIndex(0));

            straightCards.Add(player.GetCardByIndex(0));
            int size = temp.CardsCount() - 1;
            for (int i = 0; i < size; i++)
            {
                if (straightCards.CardsCount() == 3)
                    return straightCards;

                int currentRank = temp.GetCardByIndex(i).GetRankValue();
                int nextRank = temp.GetCardByIndex(i + 1).GetRankValue();
                if (nextRank - currentRank == 1)
                    straightCards.Add(temp.GetCardByIndex(i + 1));
                else if (nextRank == 1 && currentRank == 13)
                    straightCards.Add(temp.GetCardByIndex(i + 1));
                else if (nextRank - currentRank != 1)
                {
                    straightCards.Clear();
                    straightCards.Add(temp.GetCardByIndex(i + 1));
                }
            }
            if (temp.GetCardByIndex(0).GetRankValue() == 1)
                temp.Remove(temp.GetTopCard());
            Debug.Log("Get Straight method executed completely");
            return straightCards;
        }

        public static Card CreateStraight(Player.Player player, Card topCard)
        {
            Debug.Log("Inside Create Straight Method");
            Card removeCard = null;
            Deck tempDeck = new Deck(player.GetDeck());
            tempDeck.SortByRankDesc();
            if (tempDeck.GetCardByIndex(0).GetRankValue() == 1)
                tempDeck.Add(tempDeck.GetCardByIndex(0));
            int decksize = tempDeck.CardsCount();
            Deck straightDeck = new Deck(player.GetDeck());
            straightDeck.Add(tempDeck.GetCardByIndex(0));
            for (int i = 0; i < decksize - 1; i++)
            {
                if (straightDeck.CardsCount() == 3)
                    break;
                int currentRank = tempDeck.GetCardByIndex(i).GetRankValue();
                int nextRank = tempDeck.GetCardByIndex(i + 1).GetRankValue();
                if (nextRank - currentRank == 1)
                    straightDeck.Add(tempDeck.GetCardByIndex(i + 1));
                else if (nextRank == 1 && currentRank == 13)
                    straightDeck.Add(tempDeck.GetCardByIndex(i + 1));
                else if (nextRank - currentRank != 1)
                {
                    straightDeck.Add(topCard);              // Added discarded deck top card
                    if (IsStraight(straightDeck))
                    {
                        removeCard = tempDeck.Remove(tempDeck.GetCardByIndex(i + 1));
                        break;
                    }
                    else
                    {
                        removeCard = tempDeck.Remove(tempDeck.GetCardByIndex(i));
                    }
                    straightDeck.Clear();
                    straightDeck.Add(tempDeck.GetCardByIndex(i + 1));


                }


            }
            if (tempDeck.GetCardByIndex(0).GetRankValue() == 1)
                tempDeck.Remove(tempDeck.GetTopCard());
            Debug.Log("Create Straight Method Executed successfully");

            return removeCard;
        }

        /// <summary>
        /// Method to check whether selected card of
        /// player is Three of a kind or not
        /// </summary>
        /// <param name="player">Current Player</param>
        /// <param name="tempLongTouchList">Deck of cards touched by player</param>
        /// <returns>True if touch cards are three of kind else false</returns>

        public static bool isThreeOfKind(Player.Player player, Deck tempLongTouchList)
        {
            Debug.Log("Inside Is Three Of Kind Method with Touched Cards Parameter");
            if (tempLongTouchList.CardsCount() != 3)
                return false;
            if (tempLongTouchList.GetCardByIndex(0).GetRankValue() == tempLongTouchList.GetCardByIndex(1).GetRankValue()
                    && tempLongTouchList.GetCardByIndex(1).GetRankValue() == tempLongTouchList.GetCardByIndex(2).GetRankValue())
                return true;

            return false;
        }

        /// <summary>
        /// Method to find if Three of Kind set exist using combination player deck and Discarded Deck card
        /// </summary>
        /// <param name="player">Current player</param>
        /// <param name="card">Top Card Discarded Deck</param>
        /// <returns>1 if straight exist in deck belongs, 
        /// -1 if straight exist using combination of deck and @card parameter,
        /// 0 if no straight exist.</returns>
        public static int isThreeOfKind(Player.Player player, Card card)
        {
            Debug.Log("Inside Is Three Of Kind Method with Discard Deck Parameter");
            Deck tempDeck = new Deck(player.GetDeck());
            tempDeck.SortByRankAsc();
            int count = tempDeck.CardsCount();
            for (int i = 0; i < count - 2; i++)
            {
                if (tempDeck.GetCardByIndex(i).GetRankValue() == tempDeck.GetCardByIndex(i + 1).GetRankValue()
                        && tempDeck.GetCardByIndex(i).GetRankValue() == tempDeck.GetCardByIndex(i + 2).GetRankValue())
                    return 1;

            }
            for (int i = 0; i < count - 1; i++)
            {
                if (tempDeck.GetCardByIndex(i).GetRankValue() == tempDeck.GetCardByIndex(i + 1).GetRankValue()
                        && tempDeck.GetCardByIndex(i).GetRankValue() == card.GetRankValue())
                    return -1;
            }
            return 0;
        }

        public static Deck GetThreeOfKind(Player.Player player)
        {
            Deck threeOfKind = new Deck(player.GetDeck());
            Deck tempDeck = player.GetDeck();
            int count = tempDeck.CardsCount();
            for (int i = 0; i < count - 2; i++)
            {
                if (tempDeck.GetCardByIndex(i).GetRankValue() == tempDeck.GetCardByIndex(i + 1).GetRankValue()
                        && tempDeck.GetCardByIndex(i).GetRankValue() == tempDeck.GetCardByIndex(i + 2).GetRankValue())
                {
                    threeOfKind.Add(tempDeck.GetCardByIndex(i));
                    threeOfKind.Add(tempDeck.GetCardByIndex(i + 1));
                    threeOfKind.Add(tempDeck.GetCardByIndex(i + 2));
                    break;
                }
            }
            return threeOfKind;
        }

        public static Card CreateThreeOfKind(Player.Player player, Card topCard)
        {
            Card removedcard = null;

            Deck tempDeck = new Deck(player.GetDeck());
            tempDeck.SortByRankDesc();
            int count = tempDeck.CardsCount();
            for (int i = 0; i < count - 1; i++)
            {
                if (tempDeck.GetCardByIndex(i).GetRankValue() == tempDeck.GetCardByIndex(i + 1).GetRankValue()
                        && tempDeck.GetCardByIndex(i).GetRankValue() == topCard.GetRankValue())
                {
                    if (i - 1 >= 0)
                        removedcard = tempDeck.Remove(tempDeck.GetCardByIndex(i - 1));
                    else
                        removedcard = tempDeck.Remove(tempDeck.GetCardByIndex(i + 2));
                    break;
                }
            }
            return removedcard;
        }
    }
}
