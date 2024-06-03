using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardMatching.GamePlay
{
    public class Matcher : MonoBehaviour
    {
        [SerializeField] protected LevelData levelData;

        private IEnumerable<MatcherElementData> _pattern;

        //todo: Generating pattern.

        public bool CheckGroupMatch(MatcherElementData a, MatcherElementData b)
        {
            return a.GroupId == b.GroupId;
        }

        public bool CheckSubgroupMatch(MatcherElementData a, MatcherElementData b)
        {
            return CheckGroupMatch(a, b) && a.SubgroupId == b.SubgroupId;
        }

        public bool CheckGroupId(MatcherElementData data, string Id)
        {
            return data.GroupId == Id;
        }

        public bool CheckSubgroupId(MatcherElementData data, string Id)
        {
            return data.SubgroupId == Id;
        }

        public bool CheckEnumerableGroupMatch(IEnumerable<MatcherElementData> data)
        {
            return data.Select(x => x.GroupId).Distinct().ToList().Count == 1;
        }

        public bool CheckPatternMatch(MatcherElementData data, int index)
        {
            return _pattern.ToList()[index] == data;

        }
    }
}
