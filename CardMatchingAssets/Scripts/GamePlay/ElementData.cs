using System;
using NaughtyAttributes;
using UnityEngine;

namespace CardMatching.GamePlay
{
    [CreateAssetMenu(menuName = "CardMatching/ElementData")]
    public class ElementData : ScriptableObject
    {
        [ReadOnly] public string Id = Guid.NewGuid().ToString();
        public ElementStatus Status = ElementStatus.Active;

        public Action OnStatusChanged;

        public void SetStatus(ElementStatus status)
        {
            Status = status;
            OnStatusChanged?.Invoke();
        }
    }

    public enum ElementStatus
    {
        Inactive,
        Active
    }
}
