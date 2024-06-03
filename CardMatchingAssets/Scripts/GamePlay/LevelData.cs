using System.Collections.Generic;
using UnityEngine;

namespace CardMatching.GamePlay
{
    [CreateAssetMenu(menuName = "CardMatching/LevelData")]
    public class LevelData : ScriptableObject
    {
        public List<ElementData> Data;
    }
}
