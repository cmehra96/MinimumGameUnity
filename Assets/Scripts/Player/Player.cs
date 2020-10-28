using Assets.Scripts.CardElements;
using Assets.Scripts.Listeners;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player
    {
        protected string name;
        protected Deck myDeck;
        protected bool isPlayerReady;
        protected Card currentRoundCard;
        protected bool roundwon;
        protected int previousRoundScore;
        protected int score;
        protected int currentRoundScore = 0;
        protected PlayerEventListener listener= new PlayerEventListener();

        public Player(string name)
        {
            this.name = name;
            myDeck = new Deck();
        }

        public void AddToHand(Card card)
        {
            currentRoundCard = card;
            myDeck.Add(card);
            myDeck.SortBySuitDesc();
        }

        public Deck GetDeck()
        { return myDeck;
        }

        public Card GetCardByIndex(int i)
        {
            return myDeck.GetCardByIndex(i);

        }

        public Card RemoveCard(Card touchedCard)
        {
            return myDeck.Remove(touchedCard);
        }

        public bool IsRoundWon()
        {
            return roundwon;
        }

        public void SetRoundWon(bool roundwon)
        {
            this.roundwon = roundwon;
        }
        public virtual void NotifyPlayerForTurn()
        {
            Debug.Log("Inside Notify Player For Turn Method");
        }

        public int EvaluateScore()
        {
            int score = 0;
            foreach (Card card in myDeck.getDeck())
            {
                score += card.GetRankValue();
            }
            previousRoundScore = score;
            Debug.Log("Score of " + name + "this round is " + score);
            return score;
        }

        public int GetPreviousRoundScore()
        {
            return previousRoundScore;
        }
    }
}
