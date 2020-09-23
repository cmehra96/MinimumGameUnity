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

        if (cardcount == 0)
        {

            int decksize = GameController.Instance.players[0].GetDeck().CardsCount();

            for (int i = 0; i < decksize; i++)
            {
                Card currentCard = GameController.Instance.players[0].GetCardByIndex(i);
                GameObject vector2 = Object.Instantiate<GameObject>(this.cardPrefab, PlayerUIMapping.Instance.cardholder[0].transform);
                vector2.transform.rotation = Quaternion.Euler(Vector3.zero);
                string str = "Cards/" + currentCard.GetCardImageName(false);
                Sprite sprite = Resources.Load<Sprite>(str);
                vector2.GetComponent<CardUI>().sprite = sprite;
                vector2.GetComponent<CardUI>().UpdateSpriteRender(sprite);
                vector2.GetComponent<CardUI>().card = currentCard;
                vector2.GetComponent<RectTransform>().sizeDelta = new Vector2(0.08f * Screen.width, 0.2f * Screen.height);
                vector2.GetComponent<RectTransform>().localScale = Vector3.one;
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

    public void DrawDealtDeck()
    {
        int cardcount = dealtDeckObject.transform.childCount;

        if (cardcount == 0)
        {
            Card currentCard = GameController.Instance.DealtDeck.GetTopCard();
            GameObject vector2 = Object.Instantiate<GameObject>(this.cardPrefab, dealtDeckObject.transform);
            vector2.transform.rotation = Quaternion.Euler(Vector3.zero);
            string str = "Cards/" + currentCard.GetCardImageName(false);
            Sprite sprite = Resources.Load<Sprite>(str);
            vector2.GetComponent<CardUI>().sprite = sprite;
            vector2.GetComponent<CardUI>().card = currentCard;
           vector2.GetComponent<RectTransform>().sizeDelta = new Vector2(0.08f * Screen.width, 0.2f * Screen.height);
            vector2.GetComponent<RectTransform>().localScale = Vector3.one;
            CardUI cardUI = vector2.GetComponent<CardUI>();
            vector2.GetComponent<Image>().sprite = vector2.GetComponent<CardUI>().sprite;
            vector2.transform.SetParent(dealtDeckObject.transform);
        }
    }

    public void DrawDiscardDeck()
    {
        int cardcount = discardedDeckObject.transform.childCount;

        if (cardcount == 0)
        {
            Card currentCard = GameController.Instance.DealtDeck.GetTopCard();
            GameObject vector2 = Object.Instantiate<GameObject>(this.cardPrefab, discardedDeckObject.transform);
            vector2.transform.rotation = Quaternion.Euler(Vector3.zero);
            string str = "Cards/" + currentCard.GetCardImageName(false);
            Sprite sprite = Resources.Load<Sprite>(str);
            vector2.GetComponent<CardUI>().sprite = sprite;
            vector2.GetComponent<CardUI>().card = currentCard;
            vector2.GetComponent<RectTransform>().sizeDelta = new Vector2(0.08f * Screen.width, 0.2f * Screen.height);
            vector2.GetComponent<RectTransform>().localScale = Vector3.one;
            CardUI cardUI = vector2.GetComponent<CardUI>();
            vector2.GetComponent<Image>().sprite = vector2.GetComponent<CardUI>().sprite;
            vector2.transform.SetParent(discardedDeckObject.transform);
        }
    }

    private void Awake()
    {
        GameView.Instance = this;
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
