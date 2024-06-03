using System.Collections.Generic;
using UnityEngine;

namespace CardMatching.GamePlay.Mods
{
    public abstract class GameMod: MonoBehaviour
    {
        public List<CardElement> ContainableCards; // o modun sahnede bulunana containable kartlarini tutacak
        public List<CardElement> ContainerCards; // o modun sahnede bulunana container kartlarini tutacak
        public List<MatcherElementData> containableCardDatas; //bir levelde olacak kart datalarini ayirirken containable kart datalarini bu listede tutulacak
        public List<MatcherElementData> containerCardDatas; //bir levelde olacak kart datalarini ayirirken container kart datalarini bu listede tutulacak

        public abstract void OnLevelCards(List<MatcherElementData> elementDatas); //leveli olu�tururken o levelde olacak kartlarin datasini alip kart bilgilerini ayirip GetContainableCardsLevel ve GetContainerCardsLevel fonksiyonlarini cagiricak.
                                             //Bu fonksiyonlarda gelen data bilgisine gore kartlara data atanacak.
        public abstract void GetContainableCardsLevel();
        public abstract void GetContainerCardsLevel();
        public abstract bool Check(MatcherElementData a, MatcherElementData b=null); // kartlarin eslesme kontrolunu yapiyor
    }
}
