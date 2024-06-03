using UnityEngine;


namespace CardMatching.GamePlay
{
    [CreateAssetMenu(menuName = "CardMatching/MatcherElementData")]
    public class MatcherElementData : ElementData
    {
        public string GroupId;
        public string SubgroupId;
        public Sprite SpriteMatchElement;
    }
}
