using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.CardElements
{
    public class Card : IComparable
    {
        private RANK rank;
        private SUIT suit;
        private Card card;
       
        public Card(RANK rank, SUIT suit)
        {
            this.rank = rank;
            this.suit = suit;
        }

        public Card(Card card)
        {
            this.card = card;
        }

        public override bool Equals(object other)
        {
            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            return GetSuitValue() * 13 + GetRankValue();
        }
        public int CompareTo(object o)
        {
            return GetHashCode().CompareTo(o.GetHashCode());
        }
        public int GetRankValue()
        { return (int)rank; }

        public int GetSuitValue()
        {
            return (int)suit;
        }

      

         public string GetCardImageName()
        {
            return suit.ToString().ToLower() + GetRankValue();
        }
    }

    public enum RANK
    {
        ACE = 1, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING
    }
    public enum SUIT
    {

        CLUBS = 1,
        SPADES,
        HEARTS,
        DIAMONDS

    }

}
