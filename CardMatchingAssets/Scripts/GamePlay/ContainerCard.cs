using CardMatching.Manager;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardMatching.GamePlay
{
    public class ContainerCard : CardElement, IDropHandler
    {
        [Foldout("Settings")][SerializeField] Image cardImage;

        private void Awake()
        {
            firstPosition = transform.position;
        }

        public override void InsertData(MatcherElementData elementData)
        {
            cardImg.sprite = elementData.SpriteMatchElement;
            cardMatcherData = elementData;
            data = elementData;
        }

        public override void ResetCardInputData()
        {
            //col.enabled = true;
            enabled = true;
        }

        public override void OnTrueAnswer()
        {
            col.enabled = false;
            GameManager.Instance.NextStage();
        }

        public override void OnFalseAnswer()
        {
            PlayReferences.Instance.LevelManager.wrongAnswerCount++;
            cardImage.color = Color.red;
            col.enabled = false;
            transform.DOShakeRotation(0.4f).OnComplete(() =>
            {
                col.enabled = true;
                cardImage.color = Color.white;
            });
        }

        public void CheckContainCondition(MatcherElementData containableCardData, MatcherElementData containerCardData) // GameManager' e kart datasi atlildiktan sonra GameManager fonksiyonuba karttan gelen ContainCondition bilgisi gonderiliyor
        {
            Debug.Log("containableCardData " + containableCardData.GroupId);
            Debug.Log("containerCardData " + containerCardData.GroupId);
            GameManager.Instance.CheckAnswer(containableCardData, containerCardData);
        }

        public void OnDrop(PointerEventData eventData)
        {
            GameObject dropped = eventData.pointerDrag;
            if (dropped.transform.GetComponent<ContainableCard>())
                CheckContainCondition(dropped.transform.GetComponent<ContainableCard>().cardMatcherData, cardMatcherData);
        }
    }
}
