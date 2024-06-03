using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace CardMatching.Manager
{
    public abstract class BaseLevelManager : MonoBehaviour
    {
        [SerializeField]
        private List<Skin> _skins;

        [ReadOnly]
        public int CurrentSkinIndex;

        public Skin CurrentSkin => _skins[CurrentSkinIndex];

        public Action<int> OnLevelOpened;
        public Action OnLevelTerminated;

        public void SelectSkin(int selectionIndex)
        {
            CurrentSkinIndex = selectionIndex;
        }

        public Score GetScoreByLevelIndex(Skin skin, int index)
        {
            return skin.Scores[index];
        }

        public void OpenLevel(int index)
        {
            LevelOpenProcess(index);
            OnLevelOpened?.Invoke(index);
        }

        protected abstract void LevelOpenProcess(int index);

        protected abstract void TerminationProcess();
    }
}
