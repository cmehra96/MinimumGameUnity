using Assets.Scripts.CardElements;
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
        }
    }

    private void InitialisePlayers()
    {
        players.Add(new Player("You"));
    }


    // Start is called before the first frame update
    void Start()
    {
        if (!initialise)
            InitialiseGame();
    }

    private void Update()
    {
        if (GameView.Instance != null)
            GameView.Instance.DrawMainPlayerDeck();

    }

}
