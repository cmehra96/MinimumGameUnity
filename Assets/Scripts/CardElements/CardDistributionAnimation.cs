using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;

namespace Assets.Scripts.CardElements
{
    public class CardDistributionAnimation : MonoBehaviour
    {
        public GameObject cardsBack;
        public List<GameObject> generatedCards = new List<GameObject>();
        public List<GameObject> playersPosition;
        public GameObject dealCardDistribution;
        public static CardDistributionAnimation instance
        {
            get;
            private set;
        }

        public CardDistributionAnimation()
        {



        }

        public void generateCards(bool isNewGame)
        {
            int size = 0;
            if (generatedCards.Count > 0)
                generatedCards.Clear();
            GameObject distributionobject = GameObject.Find("CardDistributionAnimation");
            if (isNewGame)
                size = playersPosition.Count * 2;
            else
                size = playersPosition.Count;
            for (int i = 0; i < size; i++)
            {
                GameObject vector2 = UnityEngine.Object.Instantiate<GameObject>(cardsBack, distributionobject.transform);
                generatedCards.Add(vector2);
                vector2.SetActive(true);
            }

        }

      
      
        private IEnumerator DistributeCardsToPlayer()
        {
            
            foreach (GameObject newcard in generatedCards)
            {
                var cover = Instantiate(cardsBack, newcard.transform.position, Quaternion.identity, newcard.transform);
                cover.GetComponent<RectTransform>().localScale = Vector3.one;
                var tween = cover.transform.DOMove(playersPosition.First().transform.position, 0.5f);
                tween.OnComplete(() => Destroy(cover));
                yield return new WaitForSeconds(0.6f);

            }
            yield return new WaitForSeconds(1f);
            playersPosition.First().SetActive(true);

            foreach (GameObject gameObject in generatedCards)
                Destroy(gameObject);
        }

        public void PlayCardDistributionAnimation()
        {
            generateCards(true);
            StartCoroutine(DistributeCardsToPlayer());
        }

        void Awake()
        {
           instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
         
        }

       
    }
}
