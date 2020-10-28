using Assets.Scripts;
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
    private Player currentPlayer;
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

    public void CallMinium(Player player)
    {
        throw new NotImplementedException();
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

    /// <summary>
    /// Method to add single or multiple touch cards
    /// 
    /// </summary>
    /// <param name="cardindex">Index of the card touched</param>
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

    /// <summary>
    /// Method to detect Dealt Deck tapped
    /// </summary>

    public void TopCardOfDealtDeckTapped()
    {
        Debug.Log("Dealt Deck Count " + DealtDeck.CardsCount());
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

    /// <summary>
    /// Method to remove all the touched cards of Player
    /// deck if they belong to removable rules.
    /// </summary>
    /// <param name="player">Current Player</param>
    /// <param name="tempLongTouchedList">Deck of all Touched Cards</param>
    public void MultiCardDealtDeckSwap(Player player, Deck tempLongTouchedList)
    {
        if (HandCombination.IsStraight(player, tempLongTouchedList) == true || HandCombination.isThreeOfKind(player, tempLongTouchedList) == true)
        {
            tempLongTouchedList.SortByRankAsc();
            Card temp = DealtDeck.Deal();
            int i = 0;
            int size = tempLongTouchedList.CardsCount();
            while (i < size)
            {
                Card removeCard = player.RemoveCard(tempLongTouchedList.GetCardByIndex(i));
                DiscardedDeck.Add(removeCard);
                i++;
                StartCoroutine(SingleSwapAnimationFromDealtDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex], removeCard,
           CardDistributionAnimation.instance.playersPosition[currentPlayerIndex], false));

            }
            StartCoroutine(SingleSwapAnimationFromDealtDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex], null,
           CardDistributionAnimation.instance.playersPosition[currentPlayerIndex], true));
            player.AddToHand(temp);
            tempLongTouchedList.Clear();
            if(DealtDeck.CardsCount()==0)
            {
                Debug.Log("Dealt Deck empty refilling");
                RefillDeck();
            }
            Invoke("LoadClearMethod", Constants.clearMethodDelay);
            StartCoroutine(SwitchTurnToNextPlayer(false, Constants.turnPlayerDelay));
        }
    }

   

    /// <summary>
    /// Method to swap card between Dealt Deck
    /// and Player touched card
    /// </summary>
    /// <param name="player"></param>
    /// <param name="touchedCard"></param>
    public void SingleSwapFromDealtDeck(Player player, Card touchedCard)
    {
        Debug.Log("Inside Single Swap Dealt Deck Method");
        Card temp = DealtDeck.Deal();
        Debug.Log("Dealt Deck touched card" + temp.GetCardImageName());
        StartCoroutine(SingleSwapAnimationFromDealtDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex], touchedCard,
            CardDistributionAnimation.instance.playersPosition[currentPlayerIndex], true));
        Debug.Log("Card to be swapped " + touchedCard.GetCardImageName());
        Card temp1 = player.RemoveCard(touchedCard);
        player.AddToHand(temp);
        DiscardedDeck.Add(temp1);
        this.touchedCard = null;
        if (DealtDeck.CardsCount() == 0)
        {
            Debug.Log("Dealt Deck empty refilling");
            RefillDeck();
        }
        Invoke("LoadClearMethod", Constants.clearMethodDelay);
        StartCoroutine(SwitchTurnToNextPlayer(false, Constants.turnPlayerDelay));
    }

    /// <summary>
    /// Method to display Card Swap on UI
    /// </summary>
    /// <param name="parent">Game Object of Current Player</param>
    /// <param name="touchedCard">Touched card that need to be swapped</param>
    /// <param name="animationHolder">Start point of Player Animation display</param>
    /// <param name="isparentdistrubtioncompleted">True if Current Player all card distributed else False </param>
    /// <returns></returns>
    private IEnumerator SingleSwapAnimationFromDealtDeck(GameObject parent, Card touchedCard, GameObject animationHolder, bool isparentdistrubtioncompleted)
    {
        Debug.Log("Inside Single Swap Discarded Deck Animation Method");
        foreach (Transform child in parent.transform)
        {
            CardUI cardUI = child.GetComponent<CardUI>();

            if (cardUI.card == touchedCard)
            {
                var cover = Instantiate(cardUI, animationHolder.transform.position, Quaternion.identity, parent.transform);
                cover.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                var tween = cover.transform.DOMove(GameView.Instance.discardedDeckObject.transform.position, 0.5f);
                //https://stackoverflow.com/questions/32413067/unity2d-destroy-method-not-working-when-i-try-to-destroy-a-game-object
                tween.OnComplete(() => Destroy(cover.gameObject));
                yield return new WaitForSeconds(0.6f);

            }

        }
        if (isparentdistrubtioncompleted)
        {
            var cardobject = GameView.Instance.dealtDeckObject.transform.GetChild(0);
            var animationobject = Instantiate(cardobject, GameView.Instance.dealtDeckObject.transform.position, Quaternion.identity, GameView.Instance.dealtDeckObject.transform);
            var tween1 = animationobject.transform.DOMove(animationHolder.transform.position, 0.5f);
            tween1.OnComplete(() => Destroy(animationobject.gameObject));
            yield return new WaitForSeconds(0.6f);

        }
    }

    /// <summary>
    /// Method to detect Discarded Deck is touched
    /// </summary>
    public void TopCardOfDiscardedDeckTapped()
    {
        if (!isLongPressed)
        {
            if (touchedCard != null)
                SingleSwapFromDiscardedDeck(players[0], touchedCard);
        }
        else
        {
            MultiCardDiscardedDeckSwap(players[0], tempLongTouchedList);
        }
    }

    /// <summary>
    /// Method to remove all the touched cards of Player
    /// deck if they belong to removable rules,
    /// and add them to discarded deck
    /// and add top card of discarded deck to Player deck
    /// </summary>
    /// <param name="player"></param>
    /// <param name="tempLongTouchedList"></param>
    public void MultiCardDiscardedDeckSwap(Player player, Deck tempLongTouchedList)
    {
        if (HandCombination.IsStraight(player, tempLongTouchedList) == true || HandCombination.isThreeOfKind(player, tempLongTouchedList) == true)
        {
            tempLongTouchedList.SortByRankAsc();
            Card temp = DiscardedDeck.Deal();
            int i = 0;
            int size = tempLongTouchedList.CardsCount();
            while (i < size)
            {
                Card removeCard = player.RemoveCard(tempLongTouchedList.GetCardByIndex(i));
                DiscardedDeck.Add(removeCard);
                i++;
                StartCoroutine(SingleSwapAnimationFromDiscardedDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex], removeCard,
           CardDistributionAnimation.instance.playersPosition[currentPlayerIndex], false));
            }
            StartCoroutine(SingleSwapAnimationFromDiscardedDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex], null,
           CardDistributionAnimation.instance.playersPosition[currentPlayerIndex], true));
            player.AddToHand(temp);
            tempLongTouchedList.Clear();
            Invoke("LoadClearMethod", Constants.clearMethodDelay);
            StartCoroutine(SwitchTurnToNextPlayer(false, Constants.turnPlayerDelay));
        }
    }

    /// <summary>
    /// Method to swap card between Discarded Deck
    /// and Player touched card
    /// </summary>
    /// <param name="player"></param>
    /// <param name="touchedCard"></param>
    public void SingleSwapFromDiscardedDeck(Player player, Card touchedCard)
    {
        Debug.Log("Inside Single Swap Discarded Deck Method");
        StartCoroutine(SingleSwapAnimationFromDiscardedDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex], touchedCard,
           CardDistributionAnimation.instance.playersPosition[currentPlayerIndex], true));
        Card temp = DiscardedDeck.Deal();
        Card temp2 = player.RemoveCard(touchedCard);
        Debug.Log("Card to be swapped " + touchedCard.GetCardImageName());
        Debug.Log("Discarded Deck touched card" + temp.GetCardImageName());
        DiscardedDeck.Add(temp2);
        player.AddToHand(temp);
        this.touchedCard = null;    // Global variable
        Invoke("LoadClearMethod", Constants.clearMethodDelay);
        StartCoroutine(SwitchTurnToNextPlayer(false, Constants.turnPlayerDelay));
    }



    /// <summary>
    /// Method to display Card Swap on UI between Player and Discarded Deck
    /// </summary>
    /// <param name="parent">Game Object of Current Player</param>
    /// <param name="touchedCard">Touched card that need to be swapped</param>
    /// <param name="animationHolder">Start point of Player Animation display</param>
    /// <param name="isparentdistrubtioncompleted">True if Current Player all card distributed else False </param>
    /// <returns></returns>
    private IEnumerator SingleSwapAnimationFromDiscardedDeck(GameObject parent, Card touchedCard, GameObject animationHolder, bool isparentdistrubtioncompleted)
    {
        Debug.Log("Inside Single Swap Discarded Deck Animation Method");
        foreach (Transform child in parent.transform)
        {
            CardUI cardUI = child.GetComponent<CardUI>();

            if (cardUI.card == touchedCard)
            {
                var cover = Instantiate(cardUI, animationHolder.transform.position, Quaternion.identity, parent.transform);
                cover.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                var tween = cover.transform.DOMove(GameView.Instance.discardedDeckObject.transform.position, 0.5f);
                //https://stackoverflow.com/questions/32413067/unity2d-destroy-method-not-working-when-i-try-to-destroy-a-game-object
                tween.OnComplete(() => Destroy(cover.gameObject));
                yield return new WaitForSeconds(0.6f);

            }

        }
        if (isparentdistrubtioncompleted)
        {
            var cardobject = GameView.Instance.dealtDeckObject.transform.GetChild(0);
            var animationobject = Instantiate(cardobject, GameView.Instance.discardedDeckObject.transform.position, Quaternion.identity, GameView.Instance.discardedDeckObject.transform);
            var tween1 = animationobject.transform.DOMove(animationHolder.transform.position, 0.5f);
            tween1.OnComplete(() => Destroy(animationobject.gameObject));
            yield return new WaitForSeconds(0.6f);

        }
    }

    public IEnumerator SwitchTurnToNextPlayer(bool isShowDownCalled, float delayTime)
    {
        Debug.Log("Inside Switch Turn To Next Player Method");
        yield return new WaitForSeconds(delayTime);
        int size = players.Count;
        if(isShowDownCalled)
        {
            for(int i=0;i<size;i++)
            {
                if(players[i].IsRoundWon())
                {
                    currentPlayerIndex = (i + 1) % size;
                }
            }
        }
        else
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % size;
        }
       
        players[currentPlayerIndex].NotifyPlayerForTurn();
        Debug.Log("Switch Turn Method Executed Succussfully");

    }

    private void LoadClearMethod()
    {
        Debug.Log("Inside Load Clear Method");
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

    private void RefillDeck()
    {
       if(DealtDeck.CardsCount()==0)
        {
            Card topCard = DiscardedDeck.Deal();
            while(DiscardedDeck.CardsCount()>0)
            {
                DealtDeck.Add(DiscardedDeck.Deal());
            }
            DealtDeck.Shuffle();
            DiscardedDeck.Add(topCard);

        }
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

    private void OnDisable()
    {
       Debug.Log("Inside Disable method");
        StopAllCoroutines();
    }

    public PlayerList GetPlayers()
    {
        return players;
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
