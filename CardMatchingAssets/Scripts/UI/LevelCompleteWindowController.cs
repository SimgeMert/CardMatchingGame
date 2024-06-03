using CardMatching.Manager;
using UnityEngine;

namespace Games.ShadowFinder.UI
{
    public class LevelCompleteWindowController : MonoBehaviour
    {

        public int stars;

        private void Start()
        {
            stars = 0;
            PlayReferences.Instance.LevelManager.OnLevelStarCalculated += () => OpenLevelCompletePanel();
        }

        private void OpenLevelCompletePanel() //Level tamamlaninca levelcomplate panelin acilmasi saglaniyor
        {
        }

        public void ShowStar()
        {
        }

        public void NextButton() // LevelCompletePanel'de bulunan butona ataniyor
        {
            PlayReferences.Instance.LevelManager.StartLevel();
            GameManager.Instance.LoadLevel();
        }

        public void RestartButton()
        {
            PlayReferences.Instance.LevelManager.DecreaseCurrentLevel();
            PlayReferences.Instance.LevelManager.ResetAnswerData();
            GameManager.Instance.ResetLevel();
        }

        public int StarCalculator()
        {
            int wroungCount = PlayReferences.Instance.LevelManager.wrongAnswerCount;

            if (wroungCount < 4) stars = 3;
            else if (wroungCount > 3 && wroungCount < 7) stars = 2;
            else stars = 1;
            return stars;
        }
    }
}
