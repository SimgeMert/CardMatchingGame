using CardMatching.GamePlay;
using Games.ShadowFinder.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatching.Manager
{
    public class PlayReferences : MonoBehaviour
    {
        public static PlayReferences Instance { get; private set; }

        [field: SerializeField] public LevelManager LevelManager { get; set; }
        [field: SerializeField] public ModSelectionPanelController ModSelectionPanelController { get; set; }
        [field: SerializeField] public LevelCompleteWindowController LevelCompleteWindowController { get; set; }

        private void Awake()
        {
            Instance = this;
        }

    }
}
