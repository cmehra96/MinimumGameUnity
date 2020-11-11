using Assets.Scripts.CardElements;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject dealtDeckObject;
    public GameObject discardedDeckObject;
    public bool isClearMethodCompleted = true;
    public static GameView Instance
    {
        get;
        private set;
    }

    public GameView()
    {

    }

    public void DrawMainPlayerDeck()
    {
        int cardcount = PlayerUIMapping.Instance.cardholder[0].transform.childCount;
        if (GameController.Instance.GetCurrentPlayer() == GameController.Instance.players[0])
            PlayerUIMapping.Instance.turnIndicators[0].SetActive(true);
        else
            PlayerUIMapping.Instance.turnIndicators[0].SetActive(false);


        if (cardcount == 0)
        {

            int decksize = GameController.Instance.players[0].GetDeck().CardsCount();

            for (int i = 0; i < decksize; i++)
            {
                Card currentCard = GameController.Instance.players[0].GetCardByIndex(i);
                GameObject vector2 = Object.Instantiate<GameObject>(this.cardPrefab, PlayerUIMapping.Instance.cardholder[0].transform);
                vector2.transform.rotation = Quaternion.Euler(Vector3.zero);
                string str = "Cards/" + currentCard.GetCardImageName(true);
                Sprite sprite = Resources.Load<Sprite>(str);
                vector2.GetComponent<CardUI>().sprite = sprite;
               // vector2.GetComponent<CardUI>().UpdateSpriteRender(sprite);
                vector2.GetComponent<CardUI>().card = currentCard;
               // vector2.GetComponent<RectTransform>().sizeDelta = new Vector2(0.08f * Screen.width, 0.2f * Screen.height);
               // vector2.GetComponent<RectTransform>().localScale = Vector3.one;
                CardUI cardUI = vector2.GetComponent<CardUI>();
                vector2.GetComponent<Image>().sprite = vector2.GetComponent<CardUI>().sprite;

                vector2.transform.SetParent(PlayerUIMapping.Instance.cardholder[0].transform);
            }
        }
        
        // If Required 
        //else
        //{
        //    //foreach (Transform child in PlayerUIMapping.Instance.cardholder[0].transform)
        //    {
                
        //    }
        //}
        PlayerUIMapping.Instance.cardholder[0].GetComponent<GridLayoutGroup>().enabled = true;


    }

    public void DrawPlayerAtLeft()
    {
        int cardcount = PlayerUIMapping.Instance.cardholder[1].transform.childCount;
        if (GameController.Instance.GetCurrentPlayer() == GameController.Instance.players[1])
            PlayerUIMapping.Instance.turnIndicators[1].SetActive(true);
        else
            PlayerUIMapping.Instance.turnIndicators[1].SetActive(false);
        if (cardcount == 0)
        {

            int decksize = GameController.Instance.players[1].GetDeck().CardsCount();

            for (int i = 0; i < decksize; i++)
            {
                Card currentCard = GameController.Instance.players[1].GetCardByIndex(i);
                GameObject vector2 = Object.Instantiate<GameObject>(this.cardPrefab, PlayerUIMapping.Instance.cardholder[1].transform);
                vector2.transform.localRotation = Quaternion.Euler(Vector3.zero);
                float totalWidht = PlayerUIMapping.Instance.cardholder[1].GetComponent<RectTransform>().sizeDelta.x;
                string str = "Cards/" + currentCard.GetCardImageName(true);
                Sprite sprite = Resources.Load<Sprite>(str);
                vector2.GetComponent<CardUI>().sprite = sprite;
                // vector2.GetComponent<CardUI>().UpdateSpriteRender(sprite);
                vector2.GetComponent<CardUI>().card = currentCard;
                // vector2.GetComponent<RectTransform>().sizeDelta = new Vector2(0.08f * Screen.width, 0.2f * Screen.height);
                // vector2.GetComponent<RectTransform>().localScale = Vector3.one;
                CardUI cardUI = vector2.GetComponent<CardUI>();
                vector2.GetComponent<Image>().sprite = vector2.GetComponent<CardUI>().sprite;
                 vector2.transform.SetParent(PlayerUIMapping.Instance.cardholder[1].transform);
            }
        }
    }


    public void DrawPlayerAtTopLeft()
    {
        int cardcount = PlayerUIMapping.Instance.cardholder[2].transform.childCount;
        if (cardcount == 0)
        {

            int decksize = GameController.Instance.players[2].GetDeck().CardsCount();

            for (int i = 0; i < decksize; i++)
            {
                Card currentCard = GameController.Instance.players[2].GetCardByIndex(i);
                GameObject vector2 = Object.Instantiate<GameObject>(this.cardPrefab, PlayerUIMapping.Instance.cardholder[2].transform);
                vector2.transform.localRotation = Quaternion.Euler(Vector3.zero);
                float totalWidht = PlayerUIMapping.Instance.cardholder[2].GetComponent<RectTransform>().sizeDelta.x;
                string str = "Cards/" + currentCard.GetCardImageName(true);
                Sprite sprite = Resources.Load<Sprite>(str);
                vector2.GetComponent<CardUI>().sprite = sprite;
                vector2.GetComponent<CardUI>().card = currentCard;
                CardUI cardUI = vector2.GetComponent<CardUI>();
                vector2.GetComponent<Image>().sprite = vector2.GetComponent<CardUI>().sprite;
                vector2.transform.SetParent(PlayerUIMapping.Instance.cardholder[2].transform);
            }
        }
    }


    public void DrawPlayerAtTopCenter()
    {
        int cardcount = PlayerUIMapping.Instance.cardholder[3].transform.childCount;
        if (cardcount == 0)
        {

            int decksize = GameController.Instance.players[3].GetDeck().CardsCount();

            for (int i = 0; i < decksize; i++)
            {
                Card currentCard = GameController.Instance.players[3].GetCardByIndex(i);
                GameObject vector2 = Object.Instantiate<GameObject>(this.cardPrefab, PlayerUIMapping.Instance.cardholder[3].transform);
                vector2.transform.localRotation = Quaternion.Euler(Vector3.zero);
                float totalWidht = PlayerUIMapping.Instance.cardholder[1].GetComponent<RectTransform>().sizeDelta.x;
                string str = "Cards/" + currentCard.GetCardImageName(true);
                Sprite sprite = Resources.Load<Sprite>(str);
                vector2.GetComponent<CardUI>().sprite = sprite;
                vector2.GetComponent<CardUI>().card = currentCard;
                CardUI cardUI = vector2.GetComponent<CardUI>();
                vector2.GetComponent<Image>().sprite = vector2.GetComponent<CardUI>().sprite;

                vector2.transform.SetParent(PlayerUIMapping.Instance.cardholder[3].transform);
            }
        }
    }

    public void DrawPlayerAtTopRight()
    {
        int cardcount = PlayerUIMapping.Instance.cardholder[4].transform.childCount;
        if (cardcount == 0)
        {

            int decksize = GameController.Instance.players[4].GetDeck().CardsCount();

            for (int i = 0; i < decksize; i++)
            {
                Card currentCard = GameController.Instance.players[4].GetCardByIndex(i);
                GameObject vector2 = Object.Instantiate<GameObject>(this.cardPrefab, PlayerUIMapping.Instance.cardholder[4].transform);
                vector2.transform.localRotation = Quaternion.Euler(Vector3.zero);
                float totalWidht = PlayerUIMapping.Instance.cardholder[4].GetComponent<RectTransform>().sizeDelta.x;
                string str = "Cards/" + currentCard.GetCardImageName(true);
                Sprite sprite = Resources.Load<Sprite>(str);
                vector2.GetComponent<CardUI>().sprite = sprite;
                vector2.GetComponent<CardUI>().card = currentCard;
                CardUI cardUI = vector2.GetComponent<CardUI>();
                vector2.GetComponent<Image>().sprite = vector2.GetComponent<CardUI>().sprite;

                vector2.transform.SetParent(PlayerUIMapping.Instance.cardholder[4].transform);
            }
        }
    }

    public void DrawPlayerAtRight()
    {
        int cardcount = PlayerUIMapping.Instance.cardholder[5].transform.childCount;
        if (cardcount == 0)
        {

            int decksize = GameController.Instance.players[5].GetDeck().CardsCount();

            for (int i = 0; i < decksize; i++)
            {
                Card currentCard = GameController.Instance.players[5].GetCardByIndex(i);
                GameObject vector2 = Object.Instantiate<GameObject>(this.cardPrefab, PlayerUIMapping.Instance.cardholder[5].transform);
                vector2.transform.localRotation = Quaternion.Euler(Vector3.zero);
                float totalWidht = PlayerUIMapping.Instance.cardholder[5].GetComponent<RectTransform>().sizeDelta.x;
                string str = "Cards/" + currentCard.GetCardImageName(true);
                Sprite sprite = Resources.Load<Sprite>(str);
                vector2.GetComponent<CardUI>().sprite = sprite;
                vector2.GetComponent<CardUI>().card = currentCard;
                CardUI cardUI = vector2.GetComponent<CardUI>();
                vector2.GetComponent<Image>().sprite = vector2.GetComponent<CardUI>().sprite;

                vector2.transform.SetParent(PlayerUIMapping.Instance.cardholder[5].transform);
            }
        }
    }


    public void DrawDealtDeck()
    {
        int cardcount = dealtDeckObject.transform.childCount;

        if (cardcount == 0)
        {
            Card currentCard = GameController.Instance.DealtDeck.GetTopCard();
            GameObject vector2 = Object.Instantiate<GameObject>(this.cardPrefab, dealtDeckObject.transform);
            vector2.transform.rotation = Quaternion.Euler(Vector3.zero);
            string str = "Cards/" + currentCard.GetCardImageName(true);
            Sprite sprite = Resources.Load<Sprite>(str);
            RectTransform rt = vector2.GetComponent<RectTransform>();
            vector2.GetComponent<CardUI>().sprite = sprite;
            vector2.GetComponent<CardUI>().card = currentCard;
            vector2.GetComponent<RectTransform>().sizeDelta = new Vector2(0.08f * Screen.width, 0.2f * Screen.height);
            vector2.GetComponent<RectTransform>().localScale = Vector3.one;
            //vector2.transform.position = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width * 0.5f - rt.rect.width, Screen.height * 0.5f, 0));
             vector2.GetComponent<Image>().sprite = vector2.GetComponent<CardUI>().sprite;
            vector2.transform.SetParent(dealtDeckObject.transform);
        }
    }

    public void DrawDiscardDeck()
    {
        int cardcount = discardedDeckObject.transform.childCount;

        if (cardcount == 0)
        {
            
            Card currentCard = GameController.Instance.DiscardedDeck.GetTopCard();
            GameObject vector2 = Object.Instantiate<GameObject>(this.cardPrefab, discardedDeckObject.transform);
            vector2.transform.rotation = Quaternion.Euler(Vector3.zero);
            string str = "Cards/" + currentCard.GetCardImageName(true);
            RectTransform rt = vector2.GetComponent<RectTransform>();
            Sprite sprite = Resources.Load<Sprite>(str);
            vector2.GetComponent<CardUI>().sprite = sprite;
            vector2.GetComponent<CardUI>().card = currentCard;
            vector2.GetComponent<RectTransform>().sizeDelta = new Vector2(0.08f * Screen.width, 0.2f * Screen.height);
            vector2.GetComponent<RectTransform>().localScale = Vector3.one;
          //  vector2.transform.position = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width * 0.5f + rt.rect.width, Screen.height * 0.5f, 0));
            CardUI cardUI = vector2.GetComponent<CardUI>();
            vector2.GetComponent<Image>().sprite = vector2.GetComponent<CardUI>().sprite;
            vector2.transform.SetParent(discardedDeckObject.transform);
        }
    }

    public void LoadClearMethod()
    {
        Debug.Log("Inside Load Clear Method");
        IClearItems(PlayerUIMapping.Instance.cardholder[GameController.Instance.currentPlayerIndex]);
        IClearItems(GameView.Instance.dealtDeckObject);
        IClearItems(GameView.Instance.discardedDeckObject);
        isClearMethodCompleted = true;
    }

    private void IClearItems(GameObject content)
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        new WaitUntil(() => content.transform.childCount == 0);

        Debug.Log("Clear Item Method Executed Completely");

    }

    private void Awake()
    {
        GameView.Instance = this;
    }

    private void Update()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //GridLayoutGroup component = PlayerUIMapping.Instance.cardholder[0].GetComponent<GridLayoutGroup>();
        //if (Camera.main.aspect < 1.6f)
        //{
        //    component.cellSize = new Vector2(130f, 200f);
        //}
        //else
        //{
        //    component.cellSize = new Vector2(150f, 200f);
        //}

      
    }


}
