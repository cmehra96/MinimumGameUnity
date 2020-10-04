using Assets.Scripts.CardElements;
using Assets.Scripts.Listeners;
using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    public Deck DealtDeck=new Deck();
    public Deck DiscardedDeck=new Deck();
    private bool initialise = false;
    [HideInInspector]
    public PlayerList players = new PlayerList();
    private Card touchedCard = null;
    private bool isLongPressed = false;

    public static GameController Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance= this;
        
    }

    private void InitialiseGame()
    {
        InitialisePlayers();
        DistributeCards();
    }

    private void DistributeCards()
    {
        CardDistributionAnimation.instance.PlayCardDistributionAnimation();
        DealtDeck.AllocateDeck();
        DealtDeck.Shuffle();
        DiscardedDeck.Add(DealtDeck.Deal());
        for(int i=0;i<players.Count;i++)
        {
            players[i].AddToHand(DealtDeck.Deal());
            players[i].AddToHand(DealtDeck.Deal());
            players[i].AddToHand(DealtDeck.Deal());
            players[i].AddToHand(DealtDeck.Deal());
            players[i].AddToHand(DealtDeck.Deal());
            players[i].AddToHand(DealtDeck.Deal());
            players[i].AddToHand(DealtDeck.Deal());
        }
    }

    private void InitialisePlayers()
    {
        players.Add(new Player("You"));

        foreach(Player player in players)
        {
            
        }
    }

    public void MainPlayerCardTapped(int cardindex, bool longPressed)
    {
        isLongPressed = longPressed;
        if (!isLongPressed)
        {
            touchedCard = players[0].GetCardByIndex(cardindex);
        }
    }

    public void TopCardOfDealtDeckTapped()
    {
        if(isLongPressed)
        { if (touchedCard != null)
                SingleSwapFromDealtDeck(players[0], touchedCard);    
         }
    }

    public void SingleSwapFromDealtDeck(Player player, Card swapCard)
    {
        Debug.Log("Inside Single Swap Dealt Deck Method");
        Debug.Log("Card to be swapped" + swapCard.GetCardImageName());
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!initialise)
            InitialiseGame();
    }

    private void Update()
    {
        if (GameView.Instance != null && CardDistributionAnimation.instance.getIsCardDistributionCompleted())

        {
            GameView.Instance.DrawMainPlayerDeck();
            GameView.Instance.DrawDealtDeck();
            GameView.Instance.DrawDiscardDeck();
            GameView.Instance.DrawPlayerAtLeft();
            GameView.Instance.DrawPlayerAtTopLeft();
            GameView.Instance.DrawPlayerAtTopCenter();
            GameView.Instance.DrawPlayerAtTopRight();
            GameView.Instance.DrawPlayerAtRight();
        }

    }

}
