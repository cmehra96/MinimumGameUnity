using Assets.Scripts.CardElements;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public GameObject cardPrefab;
    
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

              //  int decksize = GameController.Instance.players[0].GetDeck().CardsCount();
                // if(PlayerUIMapping.Instance.cardholder[0].transform.GetEnumerator().)
                for (int i = 0; i < 2; i++)
                {
                    Card currentCard = GameController.Instance.players[0].GetCardByIndex(i);
                    GameObject vector2 = Object.Instantiate<GameObject>(this.cardPrefab, PlayerUIMapping.Instance.cardholder[0].transform.parent.parent);
                    vector2.transform.rotation = Quaternion.Euler(Vector3.zero);
                    string str = "Cards/"+currentCard.GetCardImageName();
                    Sprite sprite = Resources.Load<Sprite>(str);
                    vector2.GetComponent<CardUI>().sprite = sprite;
                    vector2.GetComponent<RectTransform>().sizeDelta = new Vector2(0.08f*Screen.width, 0.2f*Screen.height);
                    vector2.GetComponent<RectTransform>().localScale = Vector3.one;
                    CardUI cardUI = vector2.GetComponent<CardUI>();
                    vector2.GetComponent<Image>().sprite = vector2.GetComponent<CardUI>().sprite;
                    vector2.transform.SetParent(PlayerUIMapping.Instance.cardholder[0].transform);
                }
            }
            PlayerUIMapping.Instance.cardholder[0].GetComponent<GridLayoutGroup>().enabled = true;


        }
       
        private void Awake()
    {
        GameView.Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GridLayoutGroup component = PlayerUIMapping.Instance.cardholder[0].GetComponent<GridLayoutGroup>();
        if (Camera.main.aspect < 1.6f)
        {
            component.cellSize = new Vector2(130f, 200f);
        }
        else
        {
            component.cellSize = new Vector2(150f, 200f);
        }
       
    }

  
}
