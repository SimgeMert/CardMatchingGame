using CardMatching.Manager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardMatching.GamePlay.Mods
{
    public class FirstMechanicMod : GameMod
    {
        public override void OnLevelCards(List<MatcherElementData> elementDatas)
        {
            List<MatcherElementData> cloneList = elementDatas.ToList();
            containableCardDatas.Clear();
            containerCardDatas.Clear();
            elementDatas.Clear();
            int listCount = cloneList.Count;
            for (int i = 0; i < listCount; i++)
            {
                int rnd = Random.Range(0, cloneList.Count);
                elementDatas.Add(cloneList[rnd]);
                cloneList.Remove(cloneList[rnd]);
            }

            foreach (var item in elementDatas)
            {
                if (item.SubgroupId == "main")
                {
                    Debug.Log("-----");
                    containableCardDatas.Add(item); 
                }
                else containerCardDatas.Add(item);
            }

            foreach (var item in containableCardDatas) Debug.Log(item.GroupId + "1111111111");
            GetContainableCardsLevel();
            GetContainerCardsLevel();
            //bir levelde kullanilacak butun kartlarin MatcherElementData gelecek 
            //MatcherElementData[i].SubgroupId main ise containableCardDatas eklenecek, degilse containerCardDatas eklenecek
        }

        public override void GetContainerCardsLevel()
        {
            for (int i = 0; i < ContainerCards.Count; i++)
            {
                ContainerCards[i].InsertData(containerCardDatas[i]);
            }
        }

        public override void GetContainableCardsLevel()
        {
            for (int i = 0; i < ContainableCards.Count; i++)
            {
                Debug.Log("containableCardDatas" + containableCardDatas[i].GroupId);
                ContainableCards[i].InsertData(containableCardDatas[i]);
            }
        }

        public override bool Check(MatcherElementData a, MatcherElementData b = null)
        {
            return GameManager.Instance.CheckSubgroupId(a, "true");
        }
    }
}
