using Assets.Scripts.CardElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Assets.Scripts.CardElements
{
    public class CardUI : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Card card;
        public Sprite sprite;
        private float intitalX;
        private float initialY;
        private Vector2 initPos;

        [SerializeField]
        [Tooltip("How long must pointer be down on this object to trigger a long press")]
        private float holdTime = 1f;
        public UnityEvent onLongPress = new UnityEvent();

        // Start is called before the first frame update
        void Start()
        {
            
            this.initialY = base.transform.position.y;
            //base.GetComponent<Button>().interactable = true;
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Drag Begin");
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("Dragging");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("Drag Ended");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
            Invoke("OnLongPress", holdTime);
        }

        
        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Mouse Up");
            CancelInvoke("OnLongPress");
        }

        void OnLongPress()
        {
            Debug.Log("Long Press");
            onLongPress.Invoke();
        }
    }
}
