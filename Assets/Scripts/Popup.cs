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
        [SerializeField] float time = .25f;
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

        public void ShowPopup(bool invokeOnComplete = true)
        {
            if (DOTween.IsTweening(popupRect) || isOpen)
                return;

            isOpen = true;
            maskImage.gameObject.SetActive(isOpen);
            popupRect.DOScale(Vector3.one, time).OnComplete(() =>
            {
                currentPopup = this;
                onShow.Invoke();

            });
        }

        public void HidePopup(bool invokeOnComplete = true)
        {
            
            if (DOTween.IsTweening(popupRect) || !isOpen)
                return;
            Debug.Log("Inside Hide Popup");
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
