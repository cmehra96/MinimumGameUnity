﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.CardElements
{
    public class Deck
    {
        private List<Card> deck = new List<Card>();

        public Deck()
        {

        }

        public Deck(Deck otherDeck)
        {
            foreach (Card card in otherDeck.deck)
                Add(new Card(card));
        }

        public void Add(Card card,bool isLongTouched=false)
        {   
            if(!isLongTouched)
                deck.Add(card);
            else
            {
                if (deck.Contains(card) == false)   //To stop same card adding twice while long touch event
                    deck.Add(card);
            }
        }

        public void AllocateDeck()
        {
            foreach (SUIT suit in (SUIT[])Enum.GetValues(typeof(SUIT)))
            {
                foreach (RANK rank in (RANK[])Enum.GetValues(typeof(RANK)))
                {
                    Add(new Card(rank, suit));
                }
            }
        }

        /// <summary>
        /// Method to sort deck based on suit
        /// in descending order.
        /// Order will be
        /// Diamond- Highest
        /// Hearts
        /// Spades
        /// Club- Lowest
        /// </summary>
        public void SortBySuitDesc()
        {
            deck.Sort((lhs, rhs) => rhs.CompareTo(lhs));
        }

        /// <summary>
        /// compare Cards by their rank, order same as sortBySuit method
        /// </summary>
        /// 

        public void SortByRankDesc()
        {
            SortByRankDescHelper sortBy = new SortByRankDescHelper();
            deck.Sort(sortBy);
        }

        public void SortByRankAsc()
        {
            //deck.Sort(delegate (Card lhs, Card rhs)
            //{
            //    return lhs.GetRankValue().CompareTo( rhs.GetRankValue());
            //});
            //deck.Sort((IComparer<Card>)Card.SortByRank());

            SortByRankAscHelper sortBy = new SortByRankAscHelper();
            deck.Sort(sortBy);

        }

        public List<Card> getDeck()
        {
            return deck;
        }

        public Card GetCardByIndex(int index)
        {
            if (index < 0 || index >= this.deck.Count())
            {
                return null;
            }
            return (Card)this.deck.ElementAt(index);
        }



        //using an online algorithm for shuffling
        public void Shuffle()
        {
            var rand = new Random();
            for (int i = CardsCount() - 1; i > 0; i--)
            {
                int n = rand.Next(i + 1);
                Card temp = deck[i];
                deck[i] = deck[n];
                deck[n] = temp;
            }
        }

        public int CardsCount()
        {
            return deck.Count;
        }


        public Card Deal()
        {
            Card dealCard = deck.ElementAt(deck.Count - 1);
            deck.RemoveAt(deck.Count - 1);
            return dealCard;
        }

        public Card GetTopCard()
        {
            if (CardsCount() == 0)
                return null;
            return deck.ElementAt(CardsCount()-1);
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= deck.Count)
                throw new ArgumentOutOfRangeException();
            deck.RemoveAt(index);
        }
        public Card Remove(Card card)
        {
            Card card1 = null; ;
            for (int i = 0; i < deck.Count; i++)
            {
                if (deck[i].GetRankValue() == card.GetRankValue() && deck[i].GetSuitValue() == card.GetSuitValue())
                {
                  card1  = deck.ElementAt(i);
                    deck.RemoveAt(i);
                    break;
                }
            }
            return card1;
        }

        public void Clear()
        {
            deck.Clear();
        }
    }
}
