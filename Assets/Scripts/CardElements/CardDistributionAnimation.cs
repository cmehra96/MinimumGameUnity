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
        private bool isCardDistributionCompleted = false;
        
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
            GameObject distributionobject = GameObject.Find("CardDistributionObject");
            if (isNewGame)
                size = playersPosition.Count * 2;
            else
                size = playersPosition.Count;
            for (int i = 0; i < size; i++)
            {
                GameObject vector2 = Instantiate(cardsBack, distributionobject.transform);
                generatedCards.Add(vector2);
                vector2.SetActive(true);
            }

        }

      
      
        private IEnumerator DistributeCardsToPlayer()
        {
            Debug.Log("Inside Distribute Cards To Player");
            for(int i=0;i< generatedCards.Count();i++)
            {
                var cover = Instantiate(cardsBack, generatedCards[i].transform.position, Quaternion.identity, generatedCards[i].transform);
                cover.GetComponent<RectTransform>().localScale = Vector3.one;
                var tween = cover.transform.DOMove(playersPosition[i%(playersPosition.Count)].transform.position, 0.5f);
                tween.OnComplete(() => Destroy(cover));
                yield return new WaitForSeconds(0.6f);

            }
            yield return new WaitForSeconds(1f);
            //playersPosition.First().SetActive(true);

            foreach (GameObject gameObject in generatedCards)
                Destroy(gameObject);
            isCardDistributionCompleted = true;
            Debug.Log("Card Distribution method executed succussfully");
        }

        //public void PlayCardDistributionAnimation(bool isNewGame)
        //{
            
        //    generateCards(isNewGame);
        //    StartCoroutine(DistributeCardsToPlayer());
        //}

        public void PlayCardDistributionAnimation(bool isNewGame)
        {
            
            StartCoroutine(PlayCardDistributionAnimationRoutine(isNewGame));
        }

        public IEnumerator PlayCardDistributionAnimationRoutine(bool isNewGame)
        {
           
            generateCards(isNewGame);
            yield return DistributeCardsToPlayer();
        }

        public bool getIsCardDistributionCompleted()
        { return isCardDistributionCompleted; }

        public void SetIsCardDistributionCompleted(bool isCardDistributionCompleted)
        {
            this.isCardDistributionCompleted = isCardDistributionCompleted;
        }

        void Awake()
        {
           instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
         
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

    }
}
