﻿using Assets.Scripts;
using Assets.Scripts.CardElements;
using Assets.Scripts.Listeners;
using Assets.Scripts.Player;
using Assets.Scripts.Utility;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public int currentPlayerIndex = 0;
    private Deck tempLongTouchedList = new Deck();
    private GameObject[] PlayerGameObject= new GameObject[6];
    public Popup exitPopup,scoreboardPopup;
    private int roundCounter = 0;

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
        CardDistributionAnimation.instance.PlayCardDistributionAnimation(true);
        DealtDeck.AllocateDeck();
        DealtDeck.Shuffle();
        DiscardedDeck.Add(DealtDeck.Deal());
        for (int i = 0; i < players.Count; i++)
        {
            players[i].AddToHand(DealtDeck.Deal());
            players[i].AddToHand(DealtDeck.Deal());
        }
    }

    public void CallMinium(Player currentPlayer)
    {
        Debug.Log("Minimum is called by " + currentPlayer.GetName());
        bool roundwon = true;
        Player winnerPlayer = currentPlayer;
        int roundScore = currentPlayer.EvaluateScore();
        Debug.Log(currentPlayer.GetName() + " round score is " + roundScore);
        for(int i=0;i<players.Count;i++)
        { Player player = players[i];
            player.SetShowCard(true);
            if (player == currentPlayer)
                continue;
            int playerRoundScore = player.EvaluateScore();
            Debug.Log(player.GetName() + " round score is " + playerRoundScore);
            if(roundScore>=playerRoundScore)
            {
                roundwon = false;
                winnerPlayer = player;
                roundScore = playerRoundScore;
            }
        }
        if(roundwon)
        {
            for(int i=0;i<players.Count;i++)
            {
                Player player = players[i];
                if (player==currentPlayer)
                {
                    player.SetRoundWon(true);
                    player.AddScore(0);
                    continue;
                }
                player.AddScore(player.EvaluateScore());
                player.SetRoundWon(false);
            }
        }
        else
        {
            for(int i=0;i<players.Count;i++)
            {
                Player player = players[i];
                if (player == currentPlayer)
                {
                    player.SetRoundWon(false);
                    player.AddScore(2* player.EvaluateScore());     // Double Points added if call player loose
                    continue;
                }
                player.AddScore(0);
               
            }
            players[players.IndexOf(winnerPlayer)].SetRoundWon(true); // Index of player who won the round.
        }
        Debug.Log("Winner of this round is " + winnerPlayer.GetName());
        UnityMainThreadDispatcher.Schedule(() => 
        {
            scoreboardPopup.ShowPopup();
        }
        , 5.0f);
        UnityMainThreadDispatcher.Schedule(() =>
        {
            scoreboardPopup.HidePopup();
        }
        , 5.0f);
        //UnityMainThreadDispatcher.Schedule(() =>
        //{
        //    CardDistributionAnimation.instance.PlayCardDistributionAnimation(false);
        //    StartNextRound();
        //}
        //, 5.0f);

        UnityMainThreadDispatcher.Schedule(() =>
       {
           CardDistributionAnimation.instance.PlayCardDistributionAnimation(false);
        },0.5f);
        
        UnityMainThreadDispatcher.Schedule(() => StartNextRound(),0.5f);
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

    public void CallMinimumClicked()
    {
        Debug.Log("Minimum Button Clicked");
        CallMinium(mainPlayer);
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
                UnityMainThreadDispatcher.Instance().Enqueue(SingleSwapAnimationFromDealtDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex], removeCard,
           CardDistributionAnimation.instance.playersPosition[currentPlayerIndex], false));

            }
            UnityMainThreadDispatcher.Instance().Enqueue(SingleSwapAnimationFromDealtDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex], null,
           CardDistributionAnimation.instance.playersPosition[currentPlayerIndex], true));
            player.AddToHand(temp);
            tempLongTouchedList.Clear();
            if(DealtDeck.CardsCount()==0)
            {
                Debug.Log("Dealt Deck empty refilling");
                RefillDeck();
            }
            GameView.Instance.isClearMethodCompleted = false;
            UnityMainThreadDispatcher.Instance().Enqueue(SwitchTurnToNextPlayer(false, Constants.turnPlayerDelay));
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
        UnityMainThreadDispatcher.Instance().Enqueue(SingleSwapAnimationFromDealtDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex], touchedCard,
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
        UnityMainThreadDispatcher.Schedule(() => GameView.Instance.LoadClearMethod(), Constants.clearMethodDelay);

        UnityMainThreadDispatcher.Instance().Enqueue(SwitchTurnToNextPlayer(false, Constants.turnPlayerDelay));
        
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
                UnityMainThreadDispatcher.Instance().Enqueue(SingleSwapAnimationFromDiscardedDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex], removeCard,
           CardDistributionAnimation.instance.playersPosition[currentPlayerIndex], false));
            }
            UnityMainThreadDispatcher.Instance().Enqueue(SingleSwapAnimationFromDiscardedDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex], null,
           CardDistributionAnimation.instance.playersPosition[currentPlayerIndex], true));
            player.AddToHand(temp);
            tempLongTouchedList.Clear();
            GameView.Instance.isClearMethodCompleted = false;
            UnityMainThreadDispatcher.Instance().Enqueue(SwitchTurnToNextPlayer(false, Constants.turnPlayerDelay));
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
        UnityMainThreadDispatcher.Instance().Enqueue(SingleSwapAnimationFromDiscardedDeck(PlayerUIMapping.Instance.cardholder[currentPlayerIndex], touchedCard,
           CardDistributionAnimation.instance.playersPosition[currentPlayerIndex], true));
        Card temp = DiscardedDeck.Deal();
        Card temp2 = player.RemoveCard(touchedCard);
        Debug.Log("Card to be swapped " + touchedCard.GetCardImageName());
        Debug.Log("Discarded Deck touched card" + temp.GetCardImageName());
        DiscardedDeck.Add(temp2);
        player.AddToHand(temp);
        this.touchedCard = null;    // Global variable
        UnityMainThreadDispatcher.Schedule(() => GameView.Instance.LoadClearMethod(), Constants.clearMethodDelay);

        UnityMainThreadDispatcher.Instance().Enqueue(SwitchTurnToNextPlayer(false, Constants.turnPlayerDelay)); 
       
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
        yield return new WaitForSeconds(delayTime);

    }

    //private void LoadClearMethod()
    //{
    //    Debug.Log("Inside Load Clear Method");
    //    IClearItems(PlayerGameObject[currentPlayerIndex]);
    //    IClearItems(GameView.Instance.dealtDeckObject);
    //    IClearItems(GameView.Instance.discardedDeckObject);
    //}

    //private void IClearItems(GameObject content)
    //{
    //    for (int i = 0; i < content.transform.childCount; i++)
    //    {
    //        Destroy(content.transform.GetChild(i).gameObject);
    //    }
    //    new WaitUntil(() => content.transform.childCount == 0);

    //    Debug.Log("Clear Item Method Executed Completely");

    //}

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

    private void StartNextRound()
    {
        Debug.Log("Starting round " + (++roundCounter));
        
        for (int i=0;i<players.Count;i++)
        {
            players[i].SetShowCard(false);
            if (DealtDeck.CardsCount() == 0)
                RefillDeck();
            GameView.Instance.LoadClearMethod(i);
            players[i].AddToHand(DealtDeck.Deal());
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<6;i++)
        {
            PlayerGameObject[i]= GameObject.Find(PlayerUIMapping.Instance.cardholder[currentPlayerIndex].name);
        }
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

        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1f)
        {
            Debug.Log("Back Key Pressed");
            if (Popup.currentPopup != null && Popup.currentPopup.closeOnEsc)
            {
                Popup.currentPopup.HidePopup();
            }
            else if (Popup.currentPopup == null)
            {
                
                ShowExit();
            }
        }


    }

    public void ShowExit()
    {
        exitPopup.ShowPopup();
    }
    public void HideExit()
    {
        exitPopup.HidePopup();
    }

    public void CloseGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScreen");
    }

    public void RestartGame()
    {
        UnityMainThreadDispatcher.Schedule(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }, 0.5f);
        
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
