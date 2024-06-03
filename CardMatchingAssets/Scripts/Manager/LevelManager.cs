using UnityEngine;
using System;

namespace CardMatching.Manager
{
    public class LevelManager : BaseLevelManager
    {
        public int correctAnswerCount;
        public int wrongAnswerCount;
        public string DataString;
        public Action OnLevelStarCalculated;
        public int starsCount;

        int successPoint;
        int minThreeStarValue = 84;
        int minTwoStarValue = 60;

        public float LastLevelTime
        {
            get
            {
                return Time.realtimeSinceStartup - lastLevelTime;
            }
            set
            {
                lastLevelTime = value;
            }
        }
        float lastLevelTime;

        private void Start()
        {
            GameManager.Instance.OnLevelComplete += () => LevelStarCalculate();


            if (PlayerPrefs.GetInt(CurrentSkin.ToString()+ "CardMatchingLevelIndex") < 0)
            {
                CurrentSkin.SetCurrentLevelIndex(0);
                PlayerPrefs.SetInt(CurrentSkin.ToString() + "CardMatchingLevelIndex", 0);
            }
            else CurrentSkin.SetCurrentLevelIndex(PlayerPrefs.GetInt(CurrentSkin.ToString() + "CardMatchingLevelIndex"));
        }

        public void StartLevel()
        {
            ResetAnswerData();
            LevelOpenProcess(CurrentSkin.CurrentLevel);
        }

        public void ResetAnswerData() //Her level yuklenirken cevap sayilarin sifirlanmasi
        {
            correctAnswerCount = 0;
            wrongAnswerCount = 0;
        }

        public void IncreaseCurrentLevel() // currentLevel degerini arttiracak fonksiyonu cagirmak icin
        {
            CurrentSkin.SetCurrentLevelIndex(CurrentSkin.CurrentLevel+1);

            if (CurrentSkin.CurrentLevel >= CurrentSkin.LevelDatas.Count) CurrentSkin.SetCurrentLevelIndex(0);
            PlayerPrefs.SetInt(CurrentSkin.ToString() + "CardMatchingLevelIndex", CurrentSkin.CurrentLevel);

        }
        public void DecreaseCurrentLevel()// currentLevel degerini azalticak fonksiyonu cagirmak icin
        {
            CurrentSkin.SetCurrentLevelIndex(CurrentSkin.CurrentLevel-1);
            if (CurrentSkin.CurrentLevel < 0) CurrentSkin.SetCurrentLevelIndex(0);
            PlayerPrefs.SetInt(CurrentSkin.ToString() + "CardMatchingLevelIndex", CurrentSkin.CurrentLevel);
        }

        protected override void LevelOpenProcess(int index)
        {
            GameManager.Instance.SetLevelPool(CurrentSkin.LevelDatas[index]);
        }

        protected override void TerminationProcess()
        {
        }

        public void LevelStarCalculate()
        {
            int starCount = StarCalculator();
            GameManager.Instance.StarGainedInLevel = starCount;
            PlayReferences.Instance.LevelCompleteWindowController.stars = starCount;
            OnLevelStarCalculated?.Invoke();
        }

        public int StarCalculator()
        {
            float totalAnswerCount = correctAnswerCount + wrongAnswerCount;
            int calculation = Mathf.FloorToInt((correctAnswerCount / totalAnswerCount) * 100);
            successPoint = calculation;

            if (minThreeStarValue < calculation) starsCount = 3;
            else if (minThreeStarValue >= calculation && minTwoStarValue < calculation) starsCount = 2;
            else starsCount = 1;

            return starsCount;
        }
    }
}
