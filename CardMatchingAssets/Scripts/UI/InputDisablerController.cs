using UnityEngine;
using UnityEngine.UI;

namespace CardMatching.GamePlay.UI
{
    public class InputDisablerController : MonoBehaviour
    {
        public Image image;
        public static InputDisablerController Instance { get; private set; }

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                image.gameObject.SetActive(value);
            }
        }
        private bool _enabled;

        private void Awake()
        {
            Instance = this;
        }
    }
}
