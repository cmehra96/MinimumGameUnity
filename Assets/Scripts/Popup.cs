using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace Assets.Scripts
{
   public class Popup: MonoBehaviour
    {
        public bool isOpen;
        public bool closeOnEsc = true;
        [SerializeField] GameObject maskImage;
        [SerializeField] RectTransform popupRect;
       // [SerializeField] float time = .25f;
        public UnityEvent onShow;
        public UnityEvent onHide;
        public static Popup currentPopup;
        void Start()
        {
            if (isOpen)
            {
                ShowPopup();
            }
            else
            {
                HidePopup();
            }
        }

        public void ShowPopup(float time=0.25f)
        {
            Debug.Log("Inside Show Popup");
            if (DOTween.IsTweening(popupRect) || isOpen)
                return;

            isOpen = true;
            maskImage.gameObject.SetActive(isOpen);
            popupRect.DOScale(Vector3.one, time);
            currentPopup = this;
            onShow.Invoke(); 

            Debug.Log("Show Popup Completed");
        }

        public void HidePopup(float time = 0.25f)
        {
            Debug.Log("Inside Hide Popup");
            if (DOTween.IsTweening(popupRect) || !isOpen)
                return;
            
            isOpen = false;
            currentPopup = null;
            maskImage.gameObject.SetActive(isOpen);
            popupRect.DOScale(Vector3.zero, time).OnComplete(() => 
            {
                onHide.Invoke();

            });
        }
    }
}
