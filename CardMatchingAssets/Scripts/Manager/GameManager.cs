using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CardMatching.GamePlay.Mods;
using CardMatching.GamePlay;

namespace CardMatching.Manager
{
    public class GameManager : Matcher
    {
        public Canvas InGameCanvas;
        [SerializeField] GameObject singleImageModScene;
        [SerializeField] GameObject multipleImageModScene_1;
        [SerializeField] GameObject multipleImageModScene_2;
        [SerializeField] LevelManager levelManager;
        [SerializeField] GameMod[] selectedMod;
        [SerializeField] private CardElement[] singleObjectCards;
        [SerializeField] private CardElement[] multipleObjectCards_1;
        [SerializeField] private CardElement[] multipleObjectCards_2;
        [SerializeField] GameObject[] effects;

        public static GameManager Instance;
        public bool isCardDragging;
        public int stageIndex;
        public int StarGainedInLevel;
        public List<MatcherElementData> stageLevelData1;
        public List<MatcherElementData> stageLevelData2;
        public List<MatcherElementData> stageLevelData3;

        public System.Action OnLevelComplete;

        private string[] groupIds;

        int currentSelectModIndex = 0;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            //DevelopmentUITextVisibility(false);
            stageIndex = 0;
            PlayReferences.Instance.ModSelectionPanelController.SetSelectedModIndex(stageIndex);
        }

        public void SelectedMod(float delay = 0.3f)
        {
            if (stageIndex < 3)
            {
                StartCoroutine(SingleCardLevel(delay));
            }
            else if (stageIndex > 2 && stageIndex < 5)
            {
                currentSelectModIndex = 1;
                StartCoroutine(MultipleCardLevel_1(delay));
            }
            else
            {
                currentSelectModIndex = 2;
                StartCoroutine(MultipleCardLevel_2(delay));
            }
        }

        public IEnumerator SingleCardLevel(float delay=0.3f) //secilen moda gore o modun OnlevelCards fonksiyonu cagiriliyor.
        {
            currentSelectModIndex = 0;
            yield return new WaitForSeconds(delay);

            if (stageIndex==0) selectedMod[0].OnLevelCards(stageLevelData1.Cast<MatcherElementData>().ToList());
            else if(stageIndex==1) selectedMod[0].OnLevelCards(stageLevelData2.Cast<MatcherElementData>().ToList());
            else selectedMod[0].OnLevelCards(stageLevelData3.Cast<MatcherElementData>().ToList());

            singleImageModScene.SetActive(true);
            multipleImageModScene_1.SetActive(false);
            multipleImageModScene_2.SetActive(false);
        }
        public IEnumerator MultipleCardLevel_1(float delay = 0.3f) //secilen moda gore o modun OnlevelCards fonksiyonu cagiriliyor.
        {
            yield return new WaitForSeconds(delay);

            List<MatcherElementData> elements = new List<MatcherElementData>();

            foreach (var item in levelData.Data.Cast<MatcherElementData>().ToList())
            {
                if (item.SubgroupId == "main" || item.SubgroupId == "true") elements.Add(item);
            }

            int removeRndCardId = UnityEngine.Random.Range(0, groupIds.Length);
            string rndGroupId = groupIds[removeRndCardId];

            elements = elements.Where(x => x.GroupId != rndGroupId).ToList();

            selectedMod[currentSelectModIndex].OnLevelCards(elements);

            singleImageModScene.SetActive(false);
            multipleImageModScene_1.SetActive(true);
            multipleImageModScene_2.SetActive(false);
        }

        public IEnumerator MultipleCardLevel_2(float delay = 0.3f) //secilen moda gore o modun OnlevelCards fonksiyonu cagiriliyor.
        {
            yield return new WaitForSeconds(delay);

            List<MatcherElementData> elements = new List<MatcherElementData>();

            foreach (var item in levelData.Data.Cast<MatcherElementData>().ToList())
            {
                if (item.SubgroupId == "main" || item.SubgroupId == "true") elements.Add(item);
            }

            selectedMod[currentSelectModIndex].OnLevelCards(elements);

            singleImageModScene.SetActive(false);
            multipleImageModScene_1.SetActive(false);
            multipleImageModScene_2.SetActive(true);
        }


        public void CheckAnswer(MatcherElementData containableCardData, MatcherElementData containerCardData) //kartlardan gelen bilgilere gore inputa true ya da false donduruyor
        {
            //var dragElement = (MatcherElementData)condition.Containable.transform.GetComponent<CardElement>().data;
            //var containerElement = (MatcherElementData)condition.Container.transform.GetComponent<CardElement>().data;
            //condition.ConditionCallback?.Invoke(selectedMod[currentSelectModIndex].Check(containerCardData, containableCardData));
            //condition.ConditionCallback?.Invoke(selectedMod[currentSelectModIndex].Check((MatcherElementData)ContainerCard.data, (MatcherElementData)DraggableCard.data));
            if (selectedMod[currentSelectModIndex].Check(containerCardData, containableCardData))
                Debug.Log("trueeeeee");
            else Debug.Log("FALSEEEE");
        }

        public IEnumerator SingleCardResetLevel(float delay = 0.3f)
        {
            yield return new WaitForSeconds(delay);

            foreach (var item in singleObjectCards)
            {
                item.ResetCardInputData();
            }//MultipleCardLevel_2Reset
        }

        public IEnumerator MultipleCardResetLevel_1(float delay = 0.3f)
        {
            yield return new WaitForSeconds(delay);

            foreach (var item in multipleObjectCards_1) item.ResetCardInputData();
        }

        public IEnumerator MultipleCardResetLevel_2(float delay = 0.3f)
        {
            yield return new WaitForSeconds(delay);

            foreach (var item in multipleObjectCards_2) item.ResetCardInputData();
        }

        public void SetLevelPool(LevelData levelData) //LevelManager'den gelen datalari groupId'lerine g�re listeleniyor ama listelenme seklinin d�zenlenmesi gerekiyor
        {
            stageLevelData1.Clear();
            stageLevelData2.Clear();
            stageLevelData3.Clear();
            this.levelData = levelData;
            groupIds = levelData.Data.Cast<MatcherElementData>().Select(x => x.GroupId).Distinct().ToArray();

            for (int i = 0; i < levelData.Data.Count; i++)
            {
                if (((MatcherElementData)levelData.Data[i]).GroupId == groupIds[0]) stageLevelData1.Add((MatcherElementData)levelData.Data[i]);
                else if (((MatcherElementData)levelData.Data[i]).GroupId == groupIds[1]) stageLevelData2.Add((MatcherElementData)levelData.Data[i]);
                else if (((MatcherElementData)levelData.Data[i]).GroupId == groupIds[2]) stageLevelData3.Add((MatcherElementData)levelData.Data[i]);
            }
            levelManager.LastLevelTime = Time.realtimeSinceStartup;
        }

        public List<MatcherElementData> FilterByGroupId(string groupId) // //LevelManager'den gelen datalarin groupId'lerini bir listede toplayip level olusturulurken id sirasina g�re gidiliyor
        {
            return levelData.Data.Cast<MatcherElementData>().Where(x => x.GroupId == groupId).ToList();
        }
        
        public void NextStage() // Kartlarin icinde bulunan OnTrueAnswer() fonksiyonundan cagirilarak level ici stage indexine gore islemler yaptiriliyor
        {
            stageIndex++;

            if (stageIndex<=3 || stageIndex==5)
            {
                StartCoroutine(SingleCardResetLevel(1f));
                SelectedMod(1f);
            }
            else if (stageIndex > 7) LevelComplete();
        }

        public void LevelComplete() //Level tamamlaninca calisiyor
        {
            stageIndex = 0;
            OnLevelComplete?.Invoke();
            StartCoroutine(PlayLevelCompleteEffects());
            levelManager.IncreaseCurrentLevel();
            //levelManager.IncreaseNjoyWorldSkin();
        }

        public void LoadLevel()//Bir sonraki level
        {
            StopLevelCompleteEffects();
            StartCoroutine(SingleCardResetLevel(.1f));
            StartCoroutine(MultipleCardResetLevel_1(.1f));
            StartCoroutine(MultipleCardResetLevel_2(.1f));
            SelectedMod(.1f);
            levelManager.LastLevelTime = Time.realtimeSinceStartup;
        }

        public void ResetLevel()
        {
            StopLevelCompleteEffects();
            stageIndex = 0;
            StarGainedInLevel = 0;
            if (currentSelectModIndex==0)
            {
                StartCoroutine(SingleCardResetLevel(.1f));
            }
            else if (currentSelectModIndex==1)
            {
                StartCoroutine(SingleCardResetLevel(.1f));
                StartCoroutine(MultipleCardResetLevel_1(.1f));
            }
            else
            {
                StartCoroutine(SingleCardResetLevel(.1f));
                StartCoroutine(MultipleCardResetLevel_1(.1f));
                StartCoroutine(MultipleCardResetLevel_2(.1f));
            }
            SelectedMod(.1f);
            levelManager.LastLevelTime = Time.realtimeSinceStartup;
        }

        public void SkipMenu()
        {
            levelManager.StartLevel();
            ResetLevel();
        }

        public IEnumerator PlayLevelCompleteEffects()
        {
            foreach (var item in effects)
            {
                yield return new WaitForSeconds(0.2f);
                item.SetActive(true);
            }
        }

        private void StopLevelCompleteEffects()
        {
            foreach (var item in effects) item.SetActive(false);
        }
    }

}
