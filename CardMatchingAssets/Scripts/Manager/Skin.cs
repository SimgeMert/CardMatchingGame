using CardMatching.GamePlay;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms.Impl;

namespace CardMatching.Manager
{
    /// <summary>
    /// Görseller ve çevre öğeleri değiştirilerek oluşturulan, aynı mekaniğe sahip oyun modları.
    /// </summary>
    [System.Serializable]
    public class Skin
    {
        public List<LevelData> LevelDatas;
        public List<Score> Scores;

        private int _currentLevel;

        public int CurrentLevel
        {
            get
            {
                return _currentLevel == null ? 0 : _currentLevel;
            }
        }

        public void SetCurrentLevelIndex(int index)
        {
            _currentLevel = index;
        }
    }
}
