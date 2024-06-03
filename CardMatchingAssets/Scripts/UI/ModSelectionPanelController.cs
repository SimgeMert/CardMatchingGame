using UnityEngine;
using DG.Tweening;
using CardMatching.GamePlay.UI;
using CardMatching.Manager;

namespace CardMatching.GamePlay
{
    public class ModSelectionPanelController : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private RectTransform rootPanel;
        [SerializeField] private RectTransform bgRect;

        public void SetSelectedModIndex(int index) //Secilen mod butonuna gore level manager'deki Skin listesindeki currentSkinIndex degerli level cagiriliyor
        {
            levelManager.CurrentSkinIndex = index;
            levelManager.StartLevel();
            GameManager.Instance.ResetLevel();
        }

        public void ShowMenuCanvas() // ModSelectionPanel 'in secim ekraninaki harekit sagliyor
        {
            InputDisablerController.Instance.Enabled = true;

            Vector2 targetOffset = new Vector2(rootPanel.position.x, rootPanel.position.y + 15);
            Vector2 targetOffset2 = rootPanel.position;
            rootPanel.position = targetOffset;
            rootPanel.DOMove(targetOffset2, 0.4f).onComplete += () =>
            {
                InputDisablerController.Instance.Enabled = false;
            };
        }
    }
}
