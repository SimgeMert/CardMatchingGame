using CardMatching.Manager;
using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardMatching.GamePlay
{
    public class ContainableCard : CardElement, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [Foldout("Settings")][SerializeField] Image cardImage;
        [Foldout("Settings")][SerializeField] RectTransform rect;

        public bool isDragging;

        protected Rect freeSpaceRect;

        private void Start()
        {
            firstPosition=transform.position;
            StartCoroutine(StartPosition());
        }

        public override void InsertData(MatcherElementData elementData)
        {
            cardImg.sprite = elementData.SpriteMatchElement;
            cardMatcherData = elementData;
            data = elementData;
        }

        public override void ResetCardInputData()
        {
            transform.position = firstPosition;
            //col.enabled = true;
        }

        public override void OnTrueAnswer()
        {
            PlayReferences.Instance.LevelManager.correctAnswerCount++;
            col.enabled = false;
            cardImage.color = Color.green;
            transform.DOScale(1.5f, 0.2f).OnComplete(() =>
            {
                transform.DOScale(1.3f, 0.2f);
                cardImage.color = Color.white;
            });
        }

        public override void OnFalseAnswer()
        {
            cardImg.color = Color.red;
            col.enabled = false;
            transform.DOShakeRotation(0.4f).OnComplete(() =>
            {
                col.enabled = true;
                cardImg.color = Color.white;
            });
        }

        public IEnumerator StartPosition()
        {
            yield return new WaitForSeconds(.2f);
            firstPosition = transform.position;
            transform.position = firstPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (GameManager.Instance.isCardDragging) return;
            GameManager.Instance.isCardDragging = true;
            isDragging = true;
            cardImage.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDragging) return;
            rect.anchoredPosition += eventData.delta / GameManager.Instance.InGameCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!isDragging) return;
            isDragging=false;
            transform.DOMove(firstPosition, .8f).OnComplete(() => cardImage.raycastTarget = true);
            StartCoroutine(ImageRaycast());
        }
        IEnumerator ImageRaycast()
        {
            yield return new WaitForSeconds(.8f);

            GameManager.Instance.isCardDragging = false;
            cardImage.raycastTarget = true;
        }
    }
}
