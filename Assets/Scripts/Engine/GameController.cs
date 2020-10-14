using Assets.Scripts.CardElements;
using Assets.Scripts.Listeners;
using Assets.Scripts.Player;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Deck DealtDeck = new Deck();
    public Deck DiscardedDeck = new Deck();
    private bool initialise = false;
    [HideInInspector]
    public PlayerList players = new PlayerList();
    private Card touchedCard = null;
    private bool isLongPressed = false;
    private Player mainPlayer;
    public int no_Of_CPU_Players;
    private int currentPlayerIndex = 0;
    private Deck tempLongTouchedList = new Deck();


    public static GameController Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;

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
        for (int i = 0; i < players.Count; i++)
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
        for (int i = 1; i <= no_Of_CPU_Players; i++)
        {
            players.Add(new AIPlayer("Bot " + (i)));
        }

        mainPlayer = players[0];

    }

    public void MainPlayerCardTapped(int cardindex)
    {

        if (!isLongPressed)
        {
            touchedCard = players[0].GetCardByIndex(cardindex);
        }
        else
        {
            tempLongTouchedList.Add(players[0].GetCardByIndex(cardindex));
        }
    }



    public void TopCardOfDealtDeckTapped()
    {
        if (!isLongPressed)
        {
            if (touchedCard != null)
                SingleSwapFromDealtDeck(players[0], touchedCard);
        }
        else
        {
            MultiCardDealtDeckSwap(players[0], tempLongTouchedList);
        }
    }

    private void MultiCardDealtDeckSwap(Player player, Deck tempLongTouchedList)
    {
       
    }

    public void SingleSwapFromDealtDeck(Player player, Card touchedCard)
    {
        Debug.Log("Inside Single Swap Dealt Deck Method");
        Card temp = DealtDeck.Deal();
        Debug.Log("Dealt Deck touched card" + temp.GetCardImageName());
       StartCoroutine( SwapAnimationFromDealtDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex],touchedCard,tempLongTouchedList,temp,isLongPressed
            ,CardDistributionAnimation.instance.playersPosition[currentPlayerIndex]));
        Debug.Log("Card to be swapped " + touchedCard.GetCardImageName());
        Card temp1 = player.RemoveCard(touchedCard);
        player.AddToHand(temp);
        DiscardedDeck.Add(temp1);
        this.touchedCard = null;
        
        Invoke("LoadClearMethod",1.0f);
    }

    private IEnumerator SwapAnimationFromDealtDeck(GameObject parent, Card touchedCard, Deck tempLongTouchedList, Card temp, bool isLongPressed, GameObject animationHolder)
    {
        
        foreach (Transform child in parent.transform)
        {
           CardUI cardUI= child.GetComponent<CardUI>();
            if(!isLongPressed)
            {
               if(cardUI.card==touchedCard)
                {
                    var cover = Instantiate(cardUI, animationHolder.transform.position, Quaternion.identity, parent.transform);
                    cover.GetComponent<RectTransform>().sizeDelta = new Vector2(100,100);
                    var tween = cover.transform.DOMove(GameView.Instance.discardedDeckObject.transform.position, 0.5f);
                    //https://stackoverflow.com/questions/32413067/unity2d-destroy-method-not-working-when-i-try-to-destroy-a-game-object
                    tween.OnComplete(() => Destroy(cover.gameObject));
                    yield return new WaitForSeconds(0.6f);

                }
            }
           
        }
        var cardobject = GameView.Instance.dealtDeckObject.transform.GetChild(0);
        var animationobject = Instantiate(cardobject, GameView.Instance.dealtDeckObject.transform.position,Quaternion.identity, GameView.Instance.dealtDeckObject.transform);
        var tween1 = animationobject.transform.DOMove(animationHolder.transform.position, 0.5f);
        tween1.OnComplete(() => Destroy(animationobject));
        yield return new WaitForSeconds(0.6f);


    }

    public void TopCardOfDiscardedDeckTapped()
    {
        if (!isLongPressed)
        {
            if (touchedCard != null)
                SingleSwapFromDiscardedDeck(players[0], touchedCard);
        }
    }

    private void SingleSwapFromDiscardedDeck(Player player, Card touchedCard)
    {
        Debug.Log("Inside Single Swap Discarded Deck Method");
        Debug.Log("Card to be swapped " + touchedCard.GetCardImageName());
    }

    private void LoadClearMethod()
    {
        StartCoroutine(IClearItems(PlayerUIMapping.Instance.cardholder[currentPlayerIndex]));
        StartCoroutine(IClearItems(GameView.Instance.dealtDeckObject));
        StartCoroutine(IClearItems(GameView.Instance.discardedDeckObject));
    }

    private IEnumerator IClearItems(GameObject content)
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        yield return new WaitUntil(() => content.transform.childCount == 0);

       
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

    public Player GetCurrentPlayer()
    {
        return players[currentPlayerIndex];
    }

    public Player GetMainPlayer()
    {
        return mainPlayer;
    }

    public bool GetIsLongPressed()
    {
        return isLongPressed;
    }

    public void SetIsLongPressed(bool isLongPressed)
    {
        this.isLongPressed = isLongPressed;
    }
}
