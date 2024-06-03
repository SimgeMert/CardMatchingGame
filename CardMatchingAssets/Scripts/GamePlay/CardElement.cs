using UnityEngine;
using UnityEngine.UI;

namespace CardMatching.GamePlay
{
    public abstract class CardElement : Element
    {
        public Camera mainCamera;
        public Image cardImg;
        public BoxCollider2D col;
        public Vector3 firstPosition;
        public MatcherElementData cardMatcherData;

        public abstract void InsertData(MatcherElementData elementData); // cardlarin icine gelen data bilgisi ataniyor
        public abstract void ResetCardInputData(); //drag and drop islemi tamamlamis kartlarin tekrar drag and drop yapabilmesi icin kartin input resetleme fonksiyonlari cagiriliyor
        public abstract void OnTrueAnswer(); //  dogru eslesme olunca yapilacak islemleri yapiyor
        public abstract void OnFalseAnswer(); // yanlis eslesme olunca yapilacak islemleri yapiyor

    }
}
